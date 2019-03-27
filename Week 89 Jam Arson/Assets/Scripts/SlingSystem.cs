using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingSystem : MonoBehaviour
{
    /*
     * Introduction, the sling is a reverse aiming system.
     * It consist of a anchor and a dragable element, and it 
     * show a point and direction where you are aiming at
     * 
     */
    #region Slingshot Variables
    public GameObject m_dragCircle;
    public GameObject m_projectilePrefab;
    public CameraManager mainCamera;
    public float m_chargeSpeed;
    public float m_chargeMax;
    private bool m_pIsAiming;
    public static bool pauseShooting = false;
    private Vector3 m_pCenterAim;
    private GameObject m_pProjectile;
    private GameObject m_pDragCircle;
    private float m_pChargeValue;
    #endregion

    #region Projectile line Variables
    public float timeResolution = 0.02f;
    public float maxTime = 10.0f;
    public float VelocityOffset = 4.0f;
    private LineRenderer lineRenderer;
    #endregion

    #region In-built Functions
    // Start is called before the first frame update
    void Start()
    {
        m_pIsAiming = false;
        m_pChargeValue = 0;
        m_pCenterAim = new Vector3(transform.position.x, transform.position.y, 0);
        lineRenderer = GetComponent<LineRenderer>();
        if(lineRenderer == null)
        {
            Debug.LogError("Line Renderer is not attached to this gameobject.");
        }
            
        if(m_projectilePrefab.GetComponent<Rigidbody2D>() == null)
        {
            Debug.LogError("Projectile has no rigidbody.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseShooting)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1") && !m_pIsAiming)
        {
            if(m_pDragCircle != null)
            {
                Destroy(m_pDragCircle);
                m_pChargeValue = 0;
            }
            m_pIsAiming = true;
            m_pDragCircle = Instantiate(m_dragCircle); 
            m_pDragCircle.transform.position = CalculatePositionOfCrosshair();
            
        } else if (Input.GetButtonUp("Fire1") && m_pIsAiming)
        {
            m_pIsAiming = false;
            m_pProjectile = Instantiate(m_projectilePrefab);
            m_pProjectile.transform.position = transform.position;
            m_pProjectile.GetComponent<Rigidbody2D>().AddForce((CalculatePositionOfCrosshair() - m_pCenterAim) * VelocityOffset);
            mainCamera.followTarget = m_pProjectile;
            mainCamera.shouldFollowTarget = true;
        }

        if (m_pIsAiming)
        {
            m_pDragCircle.transform.position = CalculatePositionOfCrosshair();
            SetProjectileLine();
            
        } else if (m_pDragCircle != null && !m_pIsAiming)
        {
            // Here I want the dragcircle to fly towards centre
            m_pDragCircle.transform.position = Vector3.MoveTowards(m_pDragCircle.transform.position, m_pCenterAim, Mathf.Pow(m_pChargeValue, 3) * Time.deltaTime);
            if (Vector3.Distance(m_pDragCircle.transform.position, m_pCenterAim) < 0.5)
            {
                Destroy(m_pDragCircle);
                m_pChargeValue = 0;
            }
        }
        
    }
    #endregion

    #region SlingShot Functions
    private Vector3 CalculatePositionOfCrosshair()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        newPosition.z = 0;
        Vector3 directionalVector = newPosition - m_pCenterAim;
        Vector3 reversedUnitVector = -1 * directionalVector / directionalVector.magnitude;
        float angleWithXAxis = Mathf.Acos(Vector3.Dot(Vector3.right, reversedUnitVector));
        
        m_pChargeValue = Mathf.Min(m_chargeMax, Vector3.Distance(m_pCenterAim, newPosition));

        return reversedUnitVector * m_pChargeValue + m_pCenterAim; //m_pCenterAim is added to shif the origin of vector.
    }
    #endregion

    #region Line Renderer Functions

    void SetProjectileLine()
    {
        Vector3 velocityVector = (CalculatePositionOfCrosshair() - m_pCenterAim) * VelocityOffset; // since this is the direction * charge value * Offset
        lineRenderer.positionCount = ((int)(maxTime / timeResolution));

        int index = 0;
        Vector3 currentPosition = transform.position;
        for(float t = 0.01f; t <= maxTime; t += timeResolution)
        {
            lineRenderer.SetPosition(index, currentPosition);
            currentPosition += velocityVector * timeResolution;
            velocityVector += Physics.gravity * timeResolution;
            index++;
        }
    }

    #endregion
}

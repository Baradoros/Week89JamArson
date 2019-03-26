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

    public GameObject m_dragCircle;
    public float m_chargeSpeed;
    public float m_chargeMax;
    private bool m_pIsAiming;
    private Vector3 m_pCenterAim;
    private GameObject m_pDragCircle;
    private float m_pChargeValue;
    // Start is called before the first frame update
    void Start()
    {
        m_pIsAiming = false;
        m_pChargeValue = 0;
        m_pCenterAim = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !m_pIsAiming)
        {
            m_pIsAiming = true;
            m_pDragCircle = Instantiate(m_dragCircle); 
            m_pDragCircle.transform.position = CalculatePositionOfCrosshair();
            
        } else if (Input.GetButtonUp("Fire1"))
        {
            m_pIsAiming = false;
        }
        
        if (m_pIsAiming)
        {
            m_pDragCircle.transform.position = CalculatePositionOfCrosshair();
            
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

    private Vector3 CalculatePositionOfCrosshair()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        newPosition.z = 0;
        float angle = Mathf.Atan2((newPosition.y - m_pCenterAim.y), (newPosition.x - m_pCenterAim.y)) + Mathf.PI; // to reverse the direction.
        Vector3 reversedUnitVector = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
        Debug.Log(m_chargeMax.ToString() + " | " + Vector3.Distance(m_pCenterAim, newPosition).ToString());
        m_pChargeValue = Mathf.Min(m_chargeMax, Vector3.Distance(m_pCenterAim, newPosition));

        return reversedUnitVector * m_pChargeValue;
    }
}

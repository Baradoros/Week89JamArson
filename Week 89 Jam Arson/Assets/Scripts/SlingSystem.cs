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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !m_pIsAiming)
        {
            m_pIsAiming = true;
            m_pCenterAim = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            m_pCenterAim.z = 0;
            m_pDragCircle = Instantiate(m_dragCircle);
            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            position.z = 0;
            m_pDragCircle.transform.position = position;
            
        } else if (Input.GetMouseButtonUp(0))
        {
            m_pIsAiming = false;
        }
        
        if (m_pIsAiming)
        {
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            float angle = Vector2.SignedAngle(m_pCenterAim, newPosition) * Mathf.Deg2Rad;
   
            if (m_pChargeValue < m_chargeMax)
            {
                m_pChargeValue += m_chargeSpeed;
                m_pDragCircle.transform.position = new Vector3(m_pCenterAim.x + m_pChargeValue * Mathf.Cos(angle), m_pCenterAim.y + m_pChargeValue * Mathf.Sin(angle));
            } else
            {
                m_pChargeValue = m_chargeMax;
                m_pDragCircle.transform.position = new Vector3(m_pCenterAim.x + m_pChargeValue * Mathf.Cos(angle ), m_pCenterAim.y + m_pChargeValue * Mathf.Sin(angle));
            }
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
}

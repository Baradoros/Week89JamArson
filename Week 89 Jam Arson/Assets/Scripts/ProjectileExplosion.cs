using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D[] collisionsForForce;
    Rigidbody2D rbForForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       collisionsForForce = Physics2D.OverlapCircleAll(gameObject.transform.position, 2.0f);   

        foreach(Collider2D col in collisionsForForce)
        {
            FlammableItem flames = col.gameObject.GetComponent<FlammableItem>();
            if (flames != null)
            {
                flames.onFire = true;
                flames.StartFire();
                flames.StartCoroutine("ActivateSpreadFire");
            }
            rbForForce = col.GetComponent<Rigidbody2D>();
            if (rbForForce != null)
            {
                float rngForce = 3.0f;
                Vector2 force = new Vector2(rngForce, rngForce);
                rbForForce.AddForce(force, ForceMode2D.Impulse);
            }
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gameObject.transform.position, 2.0f);
    }
}

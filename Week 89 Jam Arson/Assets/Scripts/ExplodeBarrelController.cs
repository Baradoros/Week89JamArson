using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ExplodeBarrelController : MonoBehaviour
{
    //Particle effect explode
    public GameObject particleExplode;

    //Radius of explosion
    public float explodeRadius;

    //Min velocity to explode
    public float minVelocity;

    //Power of explosion
    [Range(-2.5f, 2.5f)]
    public float power;

    //barrel Rigidbody2D
    Rigidbody2D rb2Dbarrel;

    void Awake()
    {
        rb2Dbarrel = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if player hit barrel
        if (col.gameObject.CompareTag("Player"))
        {
            Explode();
        }
        else
        {
            if (Mathf.Abs(rb2Dbarrel.velocity.x * 1000000f) > minVelocity || Mathf.Abs(rb2Dbarrel.velocity.y * 1000000f) > minVelocity)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        //Creating explosion effect and destroying it
        GameObject effect = Instantiate(particleExplode, transform.position, transform.rotation);
        Destroy(effect, 3);

        //Taking a object near by barrel
        Collider2D[] damage = Physics2D.OverlapCircleAll(transform.position, explodeRadius); 

        foreach (Collider2D c in damage)
        {
            Rigidbody2D rb2d = c.GetComponent<Rigidbody2D>();
            //Checking that object can be move
            if (rb2d != null)
            {
                if (!c.gameObject.CompareTag("World"))
                {
                    //Making variables to force
                    // X
                    float x = Random.Range(-25f, 25f);
                    if (x < 0 && x > -15f)
                        x = -15f;
                    else if (x > 0 && x < 15f)
                        x = 15f;
                    x *= power;
                    
                    // Y
                    float y = Random.Range(-25f, 25f);
                    if (y < 0 && y > -15f)
                        y = -15f;
                    else if (y > 0 && y < 15f)
                        y = 15f;
                    y *= power;

                    //Add force
                    Vector2 dir = new Vector2(x, y);
                    rb2d.AddForce(dir, ForceMode2D.Impulse);
                }
            }
        }

        //Destroy barrel
        Destroy(gameObject);
    }
}

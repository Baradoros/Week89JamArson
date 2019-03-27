using UnityEngine;
using System.Collections;
using System;

public class FlammableItem : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField]
    public bool onFire = false;
    public bool isSpreadable = false;
    public bool destroyOnCollision = false;
    public float spreadDelay = 2.0f;
    public float deathDelay = 5.0f;
    private ParticleSystem fireParticleSystem;
    public bool OnFire
    {   get
        {
            return onFire;
        }
        set
        {
            onFire = value;
            if (onFire)
            {
                StartFire();
                StartCoroutine(ActivateSpreadFire());
            }
            else
            {
                StopFire();
            }
        }
    }
    #endregion

    #region In-built functions
    // Use this for initialization
    void Start()
    {
        fireParticleSystem = gameObject.GetComponent<ParticleSystem>();
        if(fireParticleSystem == null)
        {
            Debug.LogError("fireParticleSystem is null in " + gameObject);
        } else
        {
            if (onFire)
            {
                StartFire();
                StartCoroutine(ActivateSpreadFire());
            }
            else
            {
                StopFire();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isSpreadable)
        {
            FlammableItem otherItem = collision.gameObject.GetComponent<FlammableItem>();
            if (otherItem != null)
            {
                if (otherItem.OnFire || OnFire)
                {
                    if (otherItem.OnFire && !OnFire)
                    {
                        OnFire = true;
                    }
                    else if (!otherItem.OnFire && OnFire)
                    {
                        otherItem.OnFire = true;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSpreadable)
        {
            FlammableItem otherItem = collision.gameObject.GetComponent<FlammableItem>();
            if (otherItem != null)
            {
                if (otherItem.OnFire || OnFire)
                {
                    if (otherItem.OnFire && !OnFire)
                    {
                        OnFire = true;
                    }
                    else if (!otherItem.OnFire && OnFire)
                    {
                        otherItem.OnFire = true;
                    }
                }
            }
        }
        if (destroyOnCollision)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isSpreadable)
        {
            FlammableItem otherItem = collision.gameObject.GetComponent<FlammableItem>();
            if (otherItem != null)
            {
                if (otherItem.OnFire || OnFire)
                {
                    if (otherItem.OnFire && !OnFire)
                    {
                        OnFire = true;
                    }
                    else if (!otherItem.OnFire && OnFire)
                    {
                        otherItem.OnFire = true;
                    }
                }
            }
        }
    }
    #endregion

    #region Custom Functions

    /// <summary>
    /// Stops Fire on the FlammableItem
    /// </summary>
    private void StartFire()
    {
        fireParticleSystem.Play();
    }

    /// <summary>
    /// Stops Fire on the FlammableItem
    /// </summary>
    private void StopFire()
    {
        fireParticleSystem.Stop();
    }

    // Used by LoadScene() to delay scene loading
    private IEnumerator ActivateSpreadFire()
    {
        yield return new WaitForSeconds(spreadDelay);
        isSpreadable = true;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
    #endregion
}

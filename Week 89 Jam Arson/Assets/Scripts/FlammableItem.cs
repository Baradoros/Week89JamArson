using UnityEngine;
using System.Collections;
using System;

public class FlammableItem : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField]
    public bool onFire = false;
    public bool isSpreadable = false;
    public bool destroyable = true;
    public float spreadDelay = 1.0f;
    public float deathDelay = 3.0f;
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
                if (destroyable)
                {
                    StartCoroutine(ActivateSpreadFire());
                }
                else
                {
                    isSpreadable = true;
                }
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
                if (destroyable)
                {
                    StartCoroutine(ActivateSpreadFire());
                }
                else
                {
                    isSpreadable = true;
                }
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
                        Debug.Log(this + " Not on fire | " + otherItem.OnFire + " " + OnFire);
                    }
                    else if (!otherItem.OnFire && OnFire)
                    {
                        otherItem.OnFire = true;
                        Debug.Log(otherItem + " Not on fire | " + otherItem.OnFire + " " + OnFire);
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
                        Debug.Log(this + " Not on fire | " + otherItem.OnFire + " " + OnFire);
                    }
                    else if (!otherItem.OnFire && OnFire)
                    {
                        otherItem.OnFire = true;
                        Debug.Log(otherItem + " Not on fire | " + otherItem.OnFire + " " + OnFire);
                    }
                }
            }
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
                        Debug.Log(this + " Not on fire | " + otherItem.OnFire + " " + OnFire);
                    }
                    else if (!otherItem.OnFire && OnFire)
                    {
                        otherItem.OnFire = true;
                        Debug.Log(otherItem + " Not on fire | " + otherItem.OnFire + " " + OnFire);
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
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
    #endregion
}

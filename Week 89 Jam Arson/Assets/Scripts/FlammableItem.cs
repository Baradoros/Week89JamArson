using UnityEngine;
using System.Collections;
using System;

public class FlammableItem : MonoBehaviour
{
    #region Flammable Item Variables
    [SerializeField]
    private bool onFire; //Remove after testing
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
                Debug.Log("Started");
                StartFire();
            }
            else
            {
                Debug.Log("Stopped");
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
                Debug.Log("Started");
                StartFire();
            }
            else
            {
                Debug.Log("Stopped");
                StopFire();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FlammableItem otherItem = collision.gameObject.GetComponent<FlammableItem>();
        if (otherItem != null)
        {
            if (otherItem.OnFire && !OnFire)
            {
                OnFire = true;
                Debug.Log(this + " Not on fire | " + otherItem.OnFire + " " + OnFire);
            } else if(!otherItem.OnFire && OnFire)
            {
                otherItem.OnFire = true;
                Debug.Log(otherItem + " Not on fire | " + otherItem.OnFire + " " + OnFire);
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
    #endregion
}

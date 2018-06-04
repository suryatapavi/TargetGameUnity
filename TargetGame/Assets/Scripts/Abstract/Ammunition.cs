using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Base Class for All Ammunitions
public abstract class Ammunition : MonoBehaviour, IAmmunition
{
    public AmmunitionTypes AmmunitionType = AmmunitionTypes.NULL;
    public float Strength = 0.0f;
    public ArtifactTypes ownerType = ArtifactTypes.NULL;
    protected float lifetime = 10f;
    protected float destroytime = 1f;
    public virtual void setup()
    { }

    private void disable()
    {
        gameObject.SetActive(false);
    }
    //OnFire sets the lifetime/expiration time on the bullet
    public virtual void OnFire()
    {
        Invoke("disable",lifetime);
    }

    public abstract void OnHitTarget(GameObject target);

    // after hitting expiration sequence
    public virtual void SelfDestroy()
    {
        Invoke("disable", destroytime);
    }

}
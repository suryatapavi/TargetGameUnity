using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A bullet is a special type of an ammunition.
// In the current context this bullet is an ammuntion for the Player.
// This script is to be attached to a bullet that the Player uses as ammunition.
//A bullet is controlled by a shooting device.

public class Bullet : Ammunition, IPlayerAmmunition
{
    public Bullet() { }
    public float LifeTime = 1f;
    public float DestroyTime = 1f;
    [NonSerialized]public bool targetHit=false;

//On hitting a target this function validates if the target is of type PlayerCanKill
//If so it messages itself to the target - lodges in!!

    public override void OnHitTarget(GameObject target)
    {
        if (target.GetComponent<ITarget>() != null)
        {
            if (target.GetComponent<IPlayerCanKill>() != null)
            {
                targetHit = true;
                target.GetComponent<ITarget>().OnHit<Ammunition>(this);
            }

        }
    }



    //initial ammunition aetup
    public override void setup()
    {
        base.lifetime = LifeTime;
        base.destroytime = DestroyTime;
    }

    //takes players type and strength for communicating with IPlayerCanKill type objects
    //Shooting device calls this method. 
    public void SetPlayerInfo(PlayerTypes owner, float stregnth)
    {
        base.ownerType = owner.CastToArtifact<PlayerTypes>();
        base.Strength = stregnth;
    }

    public void Start()
    {
       this.setup();
    }

    public void OnEnable()
    {
        base.OnFire();
    }

    public void OnTriggerEnter(Collider other)
    {
        OnHitTarget(other.gameObject);
        base.SelfDestroy();
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnHitTarget(collision.gameObject);
        base.SelfDestroy();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//A Gun class is a special type of shooting device that is controlled by the user in the current context.
//It overrides the load, shoot and control handler as er deisgn.

[System.Serializable]
public class Gun : ShootDevice, IUserControlled
{
    [SerializeField] private float GunShootForce = 500;
    [SerializeField]
    public float ShootForce
    { get { return GunShootForce; }
      set {GunShootForce = value ;}
    }
    [SerializeField] private float GunPitchSpeed = 10;
    [SerializeField] private Transform GunShootLocation;

    [NonSerialized] public int ShotsFired = 0;

    public Transform ShootLocation
    {
        get { return GunShootLocation;}
    }

    public override void Load(GameObject _shootObjectPrefab, int _shootCount, bool _hasAmmunitionReserve, int _maxshootCount)
    {
        base.SetupReference(GunShootLocation);
        base.Load(_shootObjectPrefab, _shootCount, _hasAmmunitionReserve,_maxshootCount);
    }

    public override void Shoot()
    {
        GameObject shootObject = base.FetchfromPool();
        if (shootObject != null)
        {
            shootObject.transform.position = GunShootLocation.transform.position;
            shootObject.transform.rotation = GunShootLocation.transform.rotation;
            shootObject.GetComponent<Rigidbody>().AddForce(GunShootLocation.transform.forward * ShootForce);
            ShotsFired += 1;
        }
    }



    public void UserControlHandler()
    {
        if (DesktopContollerA.Controls.PitchDownControl())
        {
            MovePitchDown(GunPitchSpeed);
        }
        if (DesktopContollerA.Controls.PitchUpControl())
        {
            MovePitchUp(GunPitchSpeed);
        }
        if (DesktopContollerA.Controls.PrimaryTrigger())
        {
            Shoot();
        }

    }

    void Update ()
    {
        UserControlHandler();
    }

}

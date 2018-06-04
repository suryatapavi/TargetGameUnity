using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is an abstract implementation of a Shoot Device class of objects.
//Shoot Devices should pool ammunition they shoot with.
//It has been given a degree of movement for aiming.
//Loading. setting up and accessing ammunition pool is mostly generic and has a virtual implementation to be overriden as required.
//How a shooting device performs the action is specific to the device and hence has an abstract implementation.

public abstract class ShootDevice : MonoBehaviour, IisShootDevice, IhasObjectPool, IPitchUpDown
{
    protected ObjectPooler AmunitionPool;
    protected GameObject shootObjectPrefab;
    protected int shootCount;
    protected int maxshootCount;
    protected bool hasAmmunitionReserve;
    protected Transform shootLocation { get; set; }
    private bool ReferenceSet = false;

    public int GetPoolSize()
    {
        return AmunitionPool.GetPoolSize;
    }

    //Reference Point has to be set by particular shooting devices
    protected void SetupReference(Transform _shootLocation)
    {
        shootLocation = _shootLocation;
        ReferenceSet = true;
    }

    //Loads a Shoot Device with ammunition
    public virtual void Load(GameObject _shootObjectPrefab, int _shootCount,  bool _hasAmmunitionReserve, int _maxshootCount)
    {
        shootObjectPrefab = _shootObjectPrefab;
        shootCount = _shootCount;
        hasAmmunitionReserve = _hasAmmunitionReserve;
        maxshootCount = _maxshootCount; ;
        if (ReferenceSet)
        {
            CreateObjectPool();
        }
        else
        {
            Debug.Log("Shoot Location Missing");
        }
    }

    public void CreateObjectPool()
    {
        AmunitionPool = new ObjectPooler(shootObjectPrefab, shootCount, shootLocation, hasAmmunitionReserve, maxshootCount);
        AmunitionPool.InitializePool();
    }


    public GameObject FetchfromPool()
    {
       return AmunitionPool.FetchfromPool();
    }
    public virtual void MovePitchUp(float pitchSpeed)
    {
        this.transform.Rotate(-this.transform.right, pitchSpeed * Time.deltaTime);
    }
    public virtual void MovePitchDown(float pitchSpeed)
    {
        this.transform.Rotate(this.transform.right, pitchSpeed * Time.deltaTime);
    }

    public abstract void Shoot();
}

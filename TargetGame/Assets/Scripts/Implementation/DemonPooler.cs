using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemonPooler : ArtifactPooler, INeedsPlaneReference
{ 
    public GameObject DemonPrefab;
    public int DemonPoolSize = 10;
    public bool DemonsHiding = false;
    public int MaxNumDemons = 20;
    public Transform DemonThreat;
    public bool RandomlySpawnDemons = true;
    public Vector3 DemonThreatRange;
    public float RegenerationDelay = 20f;

    //Simple Pooling Logic - waits for the method to be invoked by a dying object after a certain delay
    //Pooling Logic can be different for different types of rewards
    public override void PoolingLogic()
    {
        Invoke("FetchfromPool", RegenerationDelay);
    }


    [PlaneReferenceProvider(PlaneType = PlaneTypes.Terrain)]
    public GameObject AddPlaneReference(GameObject objectonPlane)
    {
        objectonPlane.AdjustTerrainHeight();
        return objectonPlane;
    }

    public override GameObject FetchfromPool()
    {
        GameObject obj = base.FetchfromPool();
        AddPlaneReference(obj);
        return obj;
    }

    private void Awake()
    {
        base.SetupReference(DemonThreat, DemonThreatRange, RandomlySpawnDemons);
        base.SetupPool(DemonPrefab, DemonPoolSize, DemonsHiding, MaxNumDemons);
        Debug.Log("Pool Setup Complete:" + PoolerType.ToString());
        Debug.Log("Pool Size: " + base.maxpoolSize.ToString());
    }

    void Start ()
    {
        for (int i = 0; i < DemonPoolSize; i++)
        {
            base.FetchfromPool();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The pooling logic of the different pooled artifacts is mplmented in this class.
//This script has logic specific to PowerUpReward.

public class PowerUpRewardPooler : ArtifactPooler, INeedsPlaneReference
{ 
    public GameObject PowerUpPrefab;
    public int RewardPoolSize = 10;
    public bool RewardsinReserve = false;
    public int MaxNumRewards = 20;
    public Transform RewardSpawnReference;
    public bool RandomlySpawnRewards = true;
    public Vector3 RewardSpawnRange;
    public float RegenerationDelay = 5f;


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
        base.SetupReference(RewardSpawnReference, RewardSpawnRange, RandomlySpawnRewards);
        base.SetupPool(PowerUpPrefab, RewardPoolSize, RewardsinReserve, MaxNumRewards);
        Debug.Log("Pool Setup Complete:" + PoolerType.ToString());
        Debug.Log("Pool Size: " + base.maxpoolSize.ToString());
    }

    void Start()
    {
        for (int i = 0; i < RewardPoolSize; i++)
        {
            base.FetchfromPool();
        }
    }
}

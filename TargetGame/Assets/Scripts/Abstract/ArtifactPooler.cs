using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An abstract implementation of a class that pools objects that are a type of a Target in the game
// for different types of targets the pooling setup is similar and hence has a virtual implementation here.
//The pooling logic or how and when targets are created and destroyed is specific to targets - hence it is an abstract method.

public abstract class ArtifactPooler : MonoBehaviour, IArtifactPooler, IhasObjectPool
{
    public ArtifactTypes PoolerType;
    protected ObjectPooler ArtifactPool;
    protected GameObject ArtifactToPool;
    protected int poolSize;
    protected bool canGrow;
    protected int maxpoolSize;
    protected Transform spawnRefernce { get; set; }
    protected Vector3 spawnRange { get; set; }
    protected bool randomspawn { get; set; }

    private bool ReferenceSet = false;
    //For Testing 
    string TestingCode = "PoolTest";
    public ObjectPooler GetPoolToTest(string testingCode)
    {
        if (testingCode == TestingCode)
        {
            return ArtifactPool;
        }
        else { return null; }
    }

    public virtual void SetupReference(Transform _spawnRefernce, Vector3 _spawnRange, bool _randomspawn)
    {
        spawnRefernce = _spawnRefernce;
        spawnRange = _spawnRange;
        randomspawn = _randomspawn;
        ReferenceSet = true;
    }

    //Sets Up a Pool of Rewards
    public virtual void SetupPool(GameObject _targettoPool, int _poolSize, bool _canGrow, int _maxpoolSize)
    {
        ArtifactToPool = _targettoPool;
        poolSize = _poolSize;
        canGrow = _canGrow;
        maxpoolSize = _maxpoolSize;
        if (ReferenceSet)
        {
            CreateObjectPool();
        }
        else
        {
            Debug.Log("Enemy Threat Referece Not Set ");
        }
    }

    public virtual void CreateObjectPool()
    {
        ArtifactPool = new ObjectPooler(ArtifactToPool, poolSize, spawnRefernce, spawnRange, randomspawn, canGrow, maxpoolSize);
        ArtifactPool.InitializePool();
    }

    public virtual GameObject FetchfromPool()
    {   
        return ArtifactPool.FetchfromPool();
    }

    public abstract void PoolingLogic();
}

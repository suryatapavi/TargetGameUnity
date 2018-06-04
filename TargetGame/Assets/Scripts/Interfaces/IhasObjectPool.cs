using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* any class or game object that creates an object pool should implement this interface */
public interface IhasObjectPool
{
    void CreateObjectPool();                  // how the pool is created
    GameObject FetchfromPool();               // how objects are accessed from the pool
}

/* any class that access a object pool*/
public interface ICallsObjectPool
{
    void CallPool();
}

/*Any Game Object that pools (Object Pooling) game objects should implement this interface*/
public interface IArtifactPooler
{
    void SetupPool(GameObject _enemytoPool, int _poolSize, bool _canGrow, int _maxPoolSize);           // how to set up the pool
    void PoolingLogic();                                                                               // how to maintain pool at run time
}
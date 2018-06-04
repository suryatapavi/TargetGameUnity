using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this is a helper class implementation for object pooling.
//It does not inherit from Monobehavior and hence new object creation can be programmatically controlled from classes/objects that implements an object pool

public class ObjectPooler
{ 
    private GameObject topoolObjectPrefab { get; set; }
    private bool randomInstantiate { get; set; }
    private Transform instantiateReference { get; set; }
    private Vector3 instantiateRange { get; set; }
    private int poolSize { get; set; }
    private bool canGrow { get; set; }
    private int maxPoolSize { get; set; }

    private List<GameObject> ObjectPool;

    public int GetPoolSize
    {
        get { return ObjectPool.Count; }
    }

    /*different constructors for object pooling*/
    public ObjectPooler(GameObject _topoplObjectPrefab, int _poolSize, Transform _instantiateReference, bool _canGrow, int _maxPoolSize)
    {
        topoolObjectPrefab = _topoplObjectPrefab;
        poolSize = _poolSize;
        instantiateReference = _instantiateReference;
        canGrow = _canGrow;
        maxPoolSize = _maxPoolSize;
    }

    public ObjectPooler(GameObject _topoplObjectPrefab, int _poolSize, Transform _instantiateReference, Vector3 _instantiateRange, bool _randomInstantiate, bool _canGrow, int _maxPoolSize)
    {
        topoolObjectPrefab = _topoplObjectPrefab;
        poolSize = _poolSize;
        instantiateReference = _instantiateReference;
        instantiateRange = _instantiateRange;
        randomInstantiate = _randomInstantiate;
        canGrow = _canGrow;
        maxPoolSize = _maxPoolSize;
    }

    // method to call to initialize a pool
    public void InitializePool()
    {
        ObjectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = GameObject.Instantiate(topoolObjectPrefab) as GameObject;
            obj.SetActive(false);
            ObjectPool.Add(obj);
        }
        Debug.Log( this.GetType().ToString() + "Object Pool Initialized:" + ObjectPool.Count.ToString());
    }

    //method to call to fetch an object from the pool - this applies required transform before returning the object.
    public GameObject FetchfromPool()
    {
        GameObject obj = GetObject();
        if (obj != null)
        {
            if (!randomInstantiate)
            {

                obj.transform.position = instantiateReference.position;
            }
            else
            {
                Vector3 boundsup = instantiateReference.position + instantiateRange;
                Vector3 boundlow = instantiateReference.position - instantiateRange;
                Vector3 randomPos = new Vector3(Random.Range(boundlow.x, boundsup.x), Random.Range(boundlow.y, boundsup.y), Random.Range(boundlow.z, boundsup.z));
                obj.transform.position = randomPos;
            }
            obj.SetActive(true);
        }
        return obj;

    }

    // method to call to fetch an object from the pool - without applying desired transforms

    public GameObject GetObject()
    {
        GameObject returnobj = null;
        for (int i = 0; i < poolSize; i++)
        {
            if (!ObjectPool[i].activeInHierarchy)
            {
                return ObjectPool[i];
            }
        }
        if (canGrow && (ObjectPool.Count < maxPoolSize))
        {
            returnobj = GameObject.Instantiate(topoolObjectPrefab) as GameObject;
            ObjectPool.Add(returnobj);
        }
        //Debug.Log("Returning object from pool");
        return returnobj;
    }
}

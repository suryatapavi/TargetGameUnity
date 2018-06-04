using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//This class implements a RewardPortal that gives Rewards and is a Target for reward Collection
//A wrapper class calls the different functions of the class.
//The class is used by a pooling logic class that initializes and maintains a pool of such portals.

public class RewardPortals : MonoBehaviour,IReward, ITarget, ICallsObjectPool, IAnimateGeneric<IEnumerator,Vector3, Vector3>, INeedsPlaneReference
{
    protected RewardTypes Type;

    protected float BaseReward = 0.1f;
    protected float reward = 0.10f;
    protected int randomRewardMultiplier = 1;
    protected Text displayReward;
    protected bool randomizeReward = true;

    public float AnimateSwingRange = 10;
    public float AnimationSpeed=0.5f;
    private Vector3 currentposition;
    private Vector3 swingrightposition;
    private Vector3 swingleftposition;

    public float RewardValue
    {
        get { return reward; }
    }

   //Settting Up the Reward Portal
    public virtual void SetupReward(RewardTypes _rewardType, bool _randomizeReward, float _baseReward, Text _displayReward)
    {
        Type = _rewardType;
        displayReward = _displayReward;
        randomizeReward = _randomizeReward;
        BaseReward = _baseReward;
    }

    //This method can be called multiple times to reinitilize the reward value
    public virtual void InitializeReward()
    {
        if (randomizeReward)
        {
            randomRewardMultiplier = UnityEngine.Random.Range(1, 10);
        }
        reward = BaseReward * randomRewardMultiplier;
        displayReward.text = ((int)(reward * 100)).ToString();
    }

    //Resets the reward portal
    public virtual void Reset()
    {
        StopAllCoroutines();
        reward = BaseReward;
        randomRewardMultiplier = 1;
        CallPool();
    }

    //Method called to maintain the rewardportal
    //on reset calls the pool which it is a part of and invoke the pooling logic
    public void CallPool()
    {
        ArtifactPooler[] PoolSearch = (ArtifactPooler[])GameObject.FindObjectsOfType<ArtifactPooler>();
        foreach (ArtifactPooler pool in PoolSearch)
        {
            if (Type.CastToArtifact<RewardTypes>()==pool.PoolerType)                                               //casting all different types of artifacts to a generic artifactstype enum to select right artifacts pool
            {
                pool.PoolingLogic();
            }
        }
    }
    //Method called when the reward portal is hit
    public virtual void OnHit<T>(T hitObject)
    {
        if (typeof(T) == typeof(GameObject))
        {
            GameObject hit = hitObject as GameObject;
            GiveReward(hit);
            foreach (var disabler in Enum.GetValues(typeof(DisablesReward)))
            {
                if (hit.tag == disabler.ToString())
                {
                    gameObject.SetActive(false);
                }
            }

        }
        
    }

    //Method called to determine if 
    public virtual void GiveReward(GameObject candidate)
    {
        if (candidate.GetComponentInParent<IReceivesReward<float>>() != null)
        {
            candidate.GetComponentInParent<IReceivesReward<float>>().ReceiveReward(Type, reward);
        }
    }

    //Entry to the animation loop - initiated at the start or object creation
    public virtual void BeforeHit()
    {
        currentposition = this.transform.position;
        swingleftposition = currentposition - new Vector3(UnityEngine.Random.Range(0,AnimateSwingRange),0,0);
        swingrightposition = currentposition + new Vector3(UnityEngine.Random.Range(0, AnimateSwingRange),0,0);
        StartCoroutine(Animate(swingrightposition,swingleftposition));
    }

    //Coroutine for custom Animation on the reward portals - runs in a loop with the BeforeHit Method, till an event is encountered.
    public virtual IEnumerator Animate(Vector3 targetR, Vector3 targetL)
    {
        while (Mathf.Abs(transform.position.x-targetL.x)>.2)
        {
            transform.position = Vector3.Lerp(transform.position, targetL, 0.5f * Time.deltaTime);
            AddPlaneReference(this.gameObject);
            yield return null;
        }
        while (Mathf.Abs(transform.position.x - targetR.x) > .2)
        {
            transform.position = Vector3.Lerp(transform.position, targetR, 0.5f * Time.deltaTime);
            AddPlaneReference(this.gameObject);
            yield return null;
        }
        BeforeHit();
    }

    [PlaneReferenceProvider(PlaneType = PlaneTypes.Terrain)]
    public GameObject AddPlaneReference(GameObject objectonPlane)
    {
        objectonPlane.AdjustTerrainHeight();
        return null;
    }
}

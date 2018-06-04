using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class RewardPortalTests
{
    [UnityTest]
	public IEnumerator RewardPortal_GiveReward()
    {
        //Actual
        GameObject testreward = new GameObject();
        testreward.AddComponent<RewardPortals>();
        Text testtext = Resources.Load("Tests/TestText", typeof(Text)) as Text;
        testreward.GetComponent<RewardPortals>().SetupReward(RewardTypes.PowerUp, true, 0.4f, testtext);
        GameObject testplayer = new GameObject();
        testplayer.AddComponent<PlayerRelaxedMan>();
        float beforereward = testplayer.GetComponent<Player>().PowerFactor;
        testreward.GetComponent<RewardPortals>().GiveReward(testplayer);
        float setreward = testplayer.GetComponent<Player>().PowerFactor;
        
        yield return null;
        //Expected
        float expectedreward =(1+testreward.GetComponent<RewardPortals>().RewardValue)*beforereward;
        
        //Assert
        Assert.AreEqual(setreward, expectedreward);
        yield return null;
    }
    [UnityTest]        //All other pools in the scene must be disabled for test to work correctly
    public IEnumerator RewardPortal_PoolingCommunication()
    {
        //Actual
        GameObject testPool = new GameObject();
        testPool.AddComponent<PowerUpRewardPooler>();
        testPool.GetComponent<PowerUpRewardPooler>().RegenerationDelay = 0.0f;
        testPool.GetComponent<ArtifactPooler>().PoolerType = ArtifactTypes.PowerUp;
        GameObject testreward = new GameObject();
        testreward.AddComponent<RewardPortals>();
        Text testtext = Resources.Load("Tests/TestText", typeof(Text)) as Text;
        testreward.GetComponent<RewardPortals>().SetupReward(RewardTypes.PowerUp, true, 0.4f, testtext);
        testPool.GetComponent<ArtifactPooler>().SetupReference(testreward.transform, new Vector3(10, 10, 10), true);
        testPool.GetComponent<ArtifactPooler>().SetupPool(testreward, 10, false, 5);
        testreward.GetComponent<RewardPortals>().CallPool();                            //should take one out of the pool
        int Activepoolsize = 0;
        if (testPool.GetComponent<ArtifactPooler>().GetPoolToTest("PoolTest") != null)
        {
            ObjectPooler returnedPool = testPool.GetComponent<ArtifactPooler>().GetPoolToTest("PoolTest");
            for (int i = 0; i < 9; i++)
            {
                if (returnedPool.FetchfromPool() != null)
                {
                    Activepoolsize += 1;
                }
            }
            Debug.Log(Activepoolsize);
        }
        yield return null;
        //Expected
        int expectedActivePoolSize = 9;
        //Assert
        Assert.AreEqual(Activepoolsize, expectedActivePoolSize);
        yield return null;
    }
    [UnityTest]
    public IEnumerator RewardPortal_Hit_Disable()
    {
        //Actual
        GameObject testreward = new GameObject();
        testreward.AddComponent<RewardWrapper>();
        GameObject testplayer = Resources.Load("Tests/Player", typeof(GameObject)) as GameObject;
        testreward.GetComponent<ITarget>().OnHit<GameObject>(testplayer);
        bool RewardState = testreward.gameObject.activeInHierarchy;
        yield return null;
        //Expected
        bool expectedState = false;
        //Assert
        Assert.AreEqual(RewardState, expectedState);
        yield return null;
    }
    [UnityTest]
    public IEnumerator RewardPortal_Hit_NotDisable()
    {
        //Actual
        GameObject testreward = new GameObject();
        testreward.AddComponent<RewardWrapper>();
        GameObject testplayer = Resources.Load("Tests/Demon", typeof(GameObject)) as GameObject;
        testreward.GetComponent<ITarget>().OnHit<GameObject>(testplayer);
        bool RewardState = testreward.gameObject.activeInHierarchy;
        yield return null;
        //Expected
        bool expectedState = true;
        //Assert
        Assert.AreEqual(RewardState, expectedState);
        yield return null;
    }
}

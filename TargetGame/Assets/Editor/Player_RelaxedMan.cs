using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Player_RelaxedMan {

    [UnityTest]
    public IEnumerator PlayerTest_Relaxedman_ReceiveReward_EnemyKill()
    {
        //Actual Behavior
        GameObject testPlayer = new GameObject();
        testPlayer.AddComponent<PlayerRelaxedMan>();
        testPlayer.GetComponent<Player>().SetPlayerType(PlayerTypes.ManRelax);
        testPlayer.GetComponent<IReceivesReward<float>>().ReceiveReward(RewardTypes.EnemyKill, 200);
        yield return null;
        float playerScore = testPlayer.GetComponent<Player>().Score;
        //Expected Behavior
        float expectedScore = 200;
        //Assert
        Assert.AreEqual(playerScore, expectedScore);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerTest_Relaxedman_ReceiveReward_PowerUp()
    {
        //Actual Behavior
        GameObject testPlayer = new GameObject();
        testPlayer.AddComponent<PlayerRelaxedMan>();
        testPlayer.GetComponent<Player>().SetPlayerType(PlayerTypes.ManRelax);
        float beforePower = testPlayer.GetComponent<Player>().PowerFactor;
        testPlayer.GetComponent<IReceivesReward<float>>().ReceiveReward(RewardTypes.PowerUp, .40f);
        yield return null;
        float playerPower = testPlayer.GetComponent<Player>().PowerFactor;
        //Expected Behavior
        float expectedPower = beforePower*(1.4f);
        //Assert
        Assert.AreEqual(playerPower, expectedPower);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerTest_Relaxedman_ReceiveReward_SizeUp()
    {
        //Actual Behavior
        GameObject testPlayer = new GameObject();
        testPlayer.AddComponent<PlayerRelaxedMan>();
        testPlayer.GetComponent<Player>().SetPlayerType(PlayerTypes.ManRelax);
        float beforeSize = testPlayer.GetComponent<Player>().SizeFactor;
        testPlayer.GetComponent<IReceivesReward<float>>().ReceiveReward(RewardTypes.SizeUp, 1.50f);
        yield return null;
        float playerSize = testPlayer.GetComponent<Player>().SizeFactor;
        //Expected Behavior
        float expectedSize = beforeSize * (2.5f);
        //Assert
        Assert.AreEqual(playerSize, expectedSize);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerTest_Relaxedman_ReceiveReward_PowerUp_MaxCondition()
    {
        //Actual Behavior
        GameObject testPlayer = new GameObject();
        testPlayer.AddComponent<PlayerRelaxedMan>();
        testPlayer.GetComponent<Player>().SetPlayerType(PlayerTypes.ManRelax);
        testPlayer.GetComponent<Player>().MaxPowerFactor = 10;
        testPlayer.GetComponent<IReceivesReward<float>>().ReceiveReward(RewardTypes.PowerUp, 12.40f);
        yield return null;
        bool playerPower = testPlayer.GetComponent<Player>().PowerFactor <= 10;
        Debug.Log(testPlayer.GetComponent<Player>().PowerFactor);
        //Expected Behavior
        bool expectedPower = true;
        //Assert
        Assert.AreEqual(playerPower, expectedPower);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerTest_Relaxedman_ReceiveReward_SizeUp_MaxCondition()
    {
        //Actual Behavior
        GameObject testPlayer = new GameObject();
        testPlayer.AddComponent<PlayerRelaxedMan>();
        testPlayer.GetComponent<Player>().SetPlayerType(PlayerTypes.ManRelax);
        testPlayer.GetComponent<Player>().MaxSizeFactor = 4;
        testPlayer.GetComponent<IReceivesReward<float>>().ReceiveReward(RewardTypes.SizeUp, 5.40f);
        yield return null;
        bool playerPower = testPlayer.GetComponent<Player>().SizeFactor <= 4;
        Debug.Log(testPlayer.GetComponent<Player>().SizeFactor);
        //Expected Behavior
        bool expectedPower = true;
        //Assert
        Assert.AreEqual(playerPower, expectedPower);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerTest_Relaxedman_GotKilled()
    {
        //Actual Behavior
        GameObject testPlayer = new GameObject();
        testPlayer.AddComponent<PlayerRelaxedMan>();
        testPlayer.GetComponent<Player>().SetPlayerType(PlayerTypes.ManRelax);
        GameObject testKiller = new GameObject();
        testKiller.AddComponent<TreeDemon>();
        testPlayer.GetComponent<Player>().GetKilled(testKiller);
        yield return null;
        bool PlayerActive = testPlayer.activeInHierarchy;
        //Expected Behavior
        bool ExpectedActive = false;
        //Assert
        Assert.AreEqual(PlayerActive, ExpectedActive);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerTest_Relaxedman_NotKilled()
    {
        //Actual Behavior
        GameObject testPlayer = new GameObject();
        testPlayer.AddComponent<PlayerRelaxedMan>();
        testPlayer.GetComponent<Player>().SetPlayerType(PlayerTypes.ManRelax);
        GameObject testKiller = new GameObject();
        testKiller.AddComponent<RewardPortals>();
        testPlayer.GetComponent<Player>().GetKilled(testKiller);
        yield return null;
        bool PlayerActive = testPlayer.activeInHierarchy;
        //Expected Behavior
        bool ExpectedActive = true;
        //Assert
        Assert.AreEqual(PlayerActive, ExpectedActive);
        yield return null;
    }

}

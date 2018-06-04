using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Demon_TreeDemon
{   
    [UnityTest]
    public IEnumerator DemonTests_TreeDemon_Initialization()
    {
        GameObject testDemon = new GameObject();
        testDemon.AddComponent<TreeDemon>();
        //Actual Behavior
        GameObject playerPrefab = Resources.Load("Tests/Player", typeof(GameObject)) as GameObject;
        testDemon.GetComponent<TreeDemon>().InitializeDemon(DemonTypes.TreeDemon, 5, playerPrefab, 0.0f);
        float killReward = testDemon.GetComponent<TreeDemon>().onKillReward;
        yield return null;
        //Expected Behavior
        float expectedReward = (float)PlayerCanKill.TreeDemon;
        //Assert
        Assert.AreEqual(killReward,expectedReward);
        yield return null;
    }
    [UnityTest]
    public IEnumerator DemonTests_TreeDemon_HitByPlayerAmmunition_CorrectAmmoDetection()
    {
        //Actual Behavior
        GameObject testDemon = new GameObject();
        testDemon.AddComponent<TreeDemon>();
        var playerPrefab = Resources.Load("Tests/Player", typeof(GameObject)) as GameObject;
        testDemon.GetComponent<TreeDemon>().InitializeDemon(DemonTypes.TreeDemon, 5, playerPrefab, 5.0f);
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        testAmmo.GetComponent<IPlayerAmmunition>().SetPlayerInfo(PlayerTypes.ManRelax, 0.0f);
        testDemon.GetComponent<TreeDemon>().OnHit(testAmmo);
        yield return null;
        AmmunitionTypes returnedAmmo = testDemon.GetComponent<TreeDemon>().hitByAmmo;
        //Expected Behavior
         AmmunitionTypes sentAmmoType = AmmunitionTypes.Bullet;
        //Assert
        Assert.AreEqual(returnedAmmo, sentAmmoType);
        yield return null;
    }
    [UnityTest]
    public IEnumerator DemonTests_TreeDemon_HitByPlayerAmmunition_CorrectHitPlayerDetection()
    {
        //Actual Behavior
        GameObject testDemon = new GameObject();
        testDemon.AddComponent<TreeDemon>();
        var playerPrefab = Resources.Load("Tests/Player", typeof(GameObject)) as GameObject;
        testDemon.GetComponent<TreeDemon>().InitializeDemon(DemonTypes.TreeDemon, 5, playerPrefab, 5.0f);
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        testAmmo.GetComponent<IPlayerAmmunition>().SetPlayerInfo(PlayerTypes.ManRelax, 0.0f);
        testDemon.GetComponent<TreeDemon>().OnHit(testAmmo);
        yield return null;
        PlayerTypes returnedPlayer = testDemon.GetComponent<TreeDemon>().hitByPlayer;
        //Expected Behavior
        PlayerTypes usedPlayer = PlayerTypes.ManRelax;
        //Assert
        Assert.AreEqual(returnedPlayer, usedPlayer);
        yield return null;
    }
    [UnityTest]
    public IEnumerator DemonTests_TreeDemon_HitByPlayerAmmunition_CorrectHitPowerDetection()
    {
        //Actual Behavior
        GameObject testDemon = new GameObject();
        testDemon.AddComponent<TreeDemon>();
        GameObject playerPrefab = Resources.Load("Tests/Player", typeof(GameObject)) as GameObject;
        testDemon.GetComponent<IDemon>().InitializeDemon(DemonTypes.TreeDemon, 2f,playerPrefab,2f);
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        
        testAmmo.GetComponent<IPlayerAmmunition>().SetPlayerInfo(PlayerTypes.ManRelax, 20f);
        testDemon.GetComponent<ITarget>().OnHit<Ammunition>(testAmmo.GetComponent<Bullet>());
        yield return null;
        float Ammostrength = testDemon.GetComponent<TreeDemon>().hitPower;
        //Expected Behavior
        //float expectedStength = testAmmo.GetComponent<Ammunition>().Strength;
        float expectedStength = 20f;
        //Assert
        Assert.AreEqual(Ammostrength, expectedStength);
        yield return null;
    }

    [UnityTest]// Player Type object must be active in scene for test to pass
    public IEnumerator DemonTests_TreeDemon_PlayerScoreSet()
    {
        //Actual Behavior
        GameObject TestDemon = new GameObject();
        TestDemon.AddComponent<TreeDemon>();
        Player player = GameObject.FindObjectOfType<Player>();
        player.GetComponent<Player>().Score = 0f;
        TestDemon.GetComponent<IDemon>().InitializeDemon(DemonTypes.TreeDemon, 10f, player.gameObject, 2f);
        TestDemon.GetComponent<IGivesPlayerScore>().SetPlayerScore(player.GetComponent<Player>().Type);
        float playerScore = player.GetComponent<Player>().Score;
        yield return null;
        //Expected Behavior
        float expectedScore = (float)DemonTypes.TreeDemon.CastAnyToAny<PlayerCanKill, DemonTypes>(PlayerCanKill.NULL);
        //Assert
        Assert.AreEqual(playerScore, expectedScore);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DemonTests_TreeDemon_HitByPlayerAmmunition_GotKilled()
    {
        //Actual Behavior
        GameObject TestDemon = new GameObject();
        TestDemon.AddComponent<TreeDemon>();
        GameObject playerPrefab = Resources.Load("Tests/Player") as GameObject;
        TestDemon.GetComponent<IDemon>().InitializeDemon(DemonTypes.TreeDemon, 10, playerPrefab, 2f);
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        testAmmo.GetComponent<IPlayerAmmunition>().SetPlayerInfo(PlayerTypes.ManRelax, 20f);
        TestDemon.GetComponent<ITarget>().OnHit<Ammunition>(testAmmo.GetComponent<Bullet>());
        yield return null;
        bool DemonState = TestDemon.GetComponent<TreeDemon>().gameObject.activeInHierarchy;
        //Expected Behavior
        bool expectedDemonState = false;
        //Assert
        Assert.AreEqual(DemonState, expectedDemonState);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DemonTests_TreeDemon_HitByPlayerAmmunition_NotKilled()
    {
        //Actual Behavior
        GameObject TestDemon = new GameObject();
        TestDemon.AddComponent<TreeDemon>();
        GameObject playerPrefab = Resources.Load("Tests/Player") as GameObject;
        TestDemon.GetComponent<IDemon>().InitializeDemon(DemonTypes.TreeDemon, 10, playerPrefab, 2f);
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        testAmmo.GetComponent<IPlayerAmmunition>().SetPlayerInfo(PlayerTypes.ManRelax, 0f);
        TestDemon.GetComponent<ITarget>().OnHit<Ammunition>(testAmmo.GetComponent<Bullet>());
        yield return null;
        bool DemonState = TestDemon.GetComponent<TreeDemon>().gameObject.activeInHierarchy;
        //Expected Behavior
        bool expectedDemonState = true;
        //Assert
        Assert.AreEqual(DemonState, expectedDemonState);
        yield return null;
    }

    [UnityTest]// Player Type object must be active in scene for test to pass
    public IEnumerator DemonTests_TreeDemon_KilledPlayer()
    {
        //Actual Behavior
        GameObject TestDemon = new GameObject();
        TestDemon.AddComponent<TreeDemon>();
        if (GameObject.FindObjectOfType<Player>() != null)
        {
            Player player = GameObject.FindObjectOfType<Player>();
            TestDemon.GetComponent<ICanKillPlayer>().killPlayer(player.gameObject);
            yield return null;
            bool playerState = player.gameObject.activeInHierarchy;
            //Expected Behavior
            bool expectedState = false;
            //Assert
            Assert.AreEqual(playerState, expectedState);
            yield return null;
        }
        yield return null;
    }

}

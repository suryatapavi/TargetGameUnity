using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Ammunition_Bullet
{

    [UnityTest]
    public IEnumerator Ammunition_Bullet_CorrectPlayerSetting()
    {
        //Actual Behavior
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        testAmmo.GetComponent<IPlayerAmmunition>().SetPlayerInfo(PlayerTypes.ManRelax, 50.0f);
        PlayerTypes playerType = testAmmo.GetComponent<Ammunition>().ownerType.CastFromArtifact<PlayerTypes>(PlayerTypes.NULL);
        yield return null;
        //Expected Behavior
        PlayerTypes ExpectedplayerType = PlayerTypes.ManRelax;
        //Assert
        Assert.AreEqual(playerType, ExpectedplayerType);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Ammunition_Bullet_CorrectStrengthSetting()
    {
        //Actual Behavior
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        testAmmo.GetComponent<IPlayerAmmunition>().SetPlayerInfo(PlayerTypes.ManRelax, 50.0f);
        float ammoStrength = testAmmo.GetComponent<Ammunition>().Strength;
        yield return null;
        //Expected Behavior
        float expectedStrength = 50.0f;
        //Assert
        Assert.AreEqual(ammoStrength, expectedStrength);
        yield return null;

    }

    [UnityTest]
    public IEnumerator Ammunition_Bullet_OnHit_PlayerCanKillType()
    {
        //Actual Behavior
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        GameObject testTarget = new GameObject();
        testTarget.AddComponent<TreeDemon>();
        testAmmo.GetComponent<Ammunition>().OnHitTarget(testTarget.gameObject);
        bool hitTarget = testAmmo.GetComponent<Bullet>().targetHit;
        yield return null;
        //Expected Behavior
         bool expectedhitState = true;
        //Assert
        Assert.AreEqual(hitTarget, expectedhitState);
        yield return null;
    }


    [UnityTest]
    public IEnumerator Ammunition_Bullet_OnHit_NotPlayerCanKillType()
    {
        //Actual Behavior
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        GameObject testTarget = new GameObject();
        testTarget.AddComponent<RewardPortals>();
        testAmmo.GetComponent<Ammunition>().OnHitTarget(testTarget.gameObject);
        bool hitTarget = testAmmo.GetComponent<Bullet>().targetHit;
        yield return null;
        //Expected Behavior
        bool expectedhitState = false;
        //Assert
        Assert.AreEqual(hitTarget, expectedhitState);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Ammunition_Bullet_OnHit_DisableOnFire()
    {
        //Actual Behavior
        GameObject testAmmo = new GameObject();
        testAmmo.AddComponent<Bullet>();
        testAmmo.GetComponent<Bullet>().LifeTime = 0f;
        testAmmo.GetComponent<Bullet>().setup();
        testAmmo.GetComponent<Ammunition>().OnFire();
        yield return null;
        bool AmmoState = testAmmo.GetComponent<Bullet>().gameObject.activeInHierarchy;
        Debug.Log(AmmoState);
        //Expected Behavior
        bool expectedActiveState = false;
        //Assert
        Assert.AreEqual(AmmoState, expectedActiveState);
        yield return null;
    }
}

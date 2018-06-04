using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ShootDevice_Gun:MonoBehaviour
{ 
	[UnityTest]
	public IEnumerator ShootDevice_Gun_ShootLocationPicked()
    {
        //Actual Behavior
        GameObject TestGun = Instantiate(Resources.Load("Tests/Gun", typeof(GameObject))) as GameObject;
        GameObject TestAmmo = Instantiate(Resources.Load("Tests/Ammo", typeof(GameObject))) as GameObject; 
        TestGun.GetComponent<Gun>().Load(TestAmmo, 20, true, 50);
        yield return null;
        bool LocationSet = TestGun.GetComponent<Gun>().ShootLocation != null;
        //Expected Behavior
        bool ExpectedSet = true;
        //Assert
        Assert.AreEqual(LocationSet, ExpectedSet);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShootDevice_Gun_AmmoPoolCreated()
    {
        //Actual Behavior
        GameObject TestGun = Instantiate(Resources.Load("Tests/Gun", typeof(GameObject))) as GameObject;
        GameObject TestAmmo = Instantiate(Resources.Load("Tests/Ammo", typeof(GameObject))) as GameObject;
        TestGun.GetComponent<Gun>().Load(TestAmmo, 20, true, 50);
        yield return null;
        int Ammopool = TestGun.GetComponent<ShootDevice>().GetPoolSize();
        bool poolstate = (Ammopool != 0);
        //Expected Behavior
        bool ExpectedSet = true;
        //Assert
        Assert.AreEqual(poolstate, ExpectedSet);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShootDevice_Gun_AmmoPoolCorrectSize()
    {
        //Actual Behavior
        GameObject TestGun = Instantiate(Resources.Load("Tests/Gun", typeof(GameObject))) as GameObject;
        GameObject TestAmmo = Instantiate(Resources.Load("Tests/Ammo", typeof(GameObject))) as GameObject;
        TestGun.GetComponent<Gun>().Load(TestAmmo, 40, true, 50);
        yield return null;
        int poolSize = TestGun.GetComponent<ShootDevice>().GetPoolSize();
        //Expected Behavior
        int ExpectedSize = 40;
        //Assert
        Assert.AreEqual(poolSize, ExpectedSize);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShootDevice_Gun_AmmoPoolShotfired()
    {
        //Actual Behavior
        GameObject TestGun = Instantiate(Resources.Load("Tests/Gun", typeof(GameObject))) as GameObject;
        GameObject TestAmmo = Instantiate(Resources.Load("Tests/Ammo", typeof(GameObject))) as GameObject;
        TestGun.GetComponent<Gun>().Load(TestAmmo, 10, true, 50);
        yield return null;
        for (int i = 0; i < 10; i++)
        {
            TestGun.GetComponent<Gun>().Shoot();
           
        }
        int shotfiredNos = TestGun.GetComponent<Gun>().ShotsFired;
        //Expected Behavior
        int ExpectedNos = 10;
        //Assert
        Assert.AreEqual(shotfiredNos, ExpectedNos);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShootDevice_Gun_AmmoPoolExpansion()
    {
        //Actual Behavior
        GameObject TestGun = Instantiate(Resources.Load("Tests/Gun", typeof(GameObject))) as GameObject;
        GameObject TestAmmo = Instantiate(Resources.Load("Tests/Ammo", typeof(GameObject))) as GameObject;
        TestGun.GetComponent<Gun>().Load(TestAmmo, 10, true, 50);
        yield return null;
        for (int i = 0; i < 20; i++)
        {
            TestGun.GetComponent<Gun>().Shoot();
        }

        int poolSize = TestGun.GetComponent<ShootDevice>().GetPoolSize();
        //Expected Behavior
        int ExpectedNos = 20;
        //Assert
        Assert.AreEqual(poolSize, ExpectedNos);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ShootDevice_Gun_AmmoPoolExpansion_ToMaxSize()
    {
        //Actual Behavior
        GameObject TestGun = Instantiate(Resources.Load("Tests/Gun", typeof(GameObject))) as GameObject;
        GameObject TestAmmo = Instantiate(Resources.Load("Tests/Ammo", typeof(GameObject))) as GameObject;
        TestGun.GetComponent<Gun>().Load(TestAmmo, 10, true, 50);
        yield return null;
        for (int i = 0; i < 60; i++)
        {
            TestGun.GetComponent<Gun>().Shoot();
        }
        int poolSize = TestGun.GetComponent<ShootDevice>().GetPoolSize();
        //Expected Behavior
        int ExpectedNos = 50;
        //Assert
        Assert.AreEqual(poolSize, ExpectedNos);
        yield return null;
    }
}

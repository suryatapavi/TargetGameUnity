using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Any Game Object that has a shooting device should implement this interface*/

public interface IhasShootDevice
{
    void loadShootDevice();    // how object loads shooting device
}


/*Any Game Object that is a shooting device should implement this interface*/

public interface IisShootDevice
{
    void Load(GameObject shootObjectPrefab, int AnuminationCount, bool hasAmunitionReserve, int _maxshootCount);    // how shooting device is loaded
    void Shoot();                                                                                                   // how shoot action is performed
}

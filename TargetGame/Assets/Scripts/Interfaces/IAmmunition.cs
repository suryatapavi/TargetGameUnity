using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Any Game Object that carries Ammunition should implement this interface*/
public interface IAmmunition
{
    void setup();                             // actions to be performed on setup
    void OnFire();                            // action to be taken on firing ammunition
    void OnHitTarget(GameObject target);      // action to be taken when ammunition hits target
    void SelfDestroy();                       // action to be performed when ammunition is destroyed
}

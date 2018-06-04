using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is an abstract implementation of a Demon type gameobject.
//Demons have been given predefined movements as per design.
//Demons are targets and receive kill.
//Most of Demon behavior during attack, before and after hit and on being killed is specific to a demon type and are abstract methods.
//Movements have a virtual implementation that can be overriden as required.

public abstract class Demon : MonoBehaviour, IDemon, IMoveLookAt, IMoveUpDown
{ 
    public abstract void InitializeDemon(DemonTypes type, float attackSpeed, GameObject threats, float minimumkillStrength);
    public abstract void Die();

    public virtual void Attack() { }
    public virtual void MoveLookAt(Transform target, float translateSpeed)
    {
        transform.LookAt(target);
        transform.Translate(this.transform.forward * translateSpeed *Time.deltaTime);
    }

    public virtual void MoveUp(float bobbingSpeed)
    {
        this.transform.Translate(this.transform.up * bobbingSpeed * Time.deltaTime);
    }

    public virtual void MoveDown(float bobbingSpeed)
    {
        this.transform.Translate(-this.transform.up * bobbingSpeed * Time.deltaTime);
    }
}

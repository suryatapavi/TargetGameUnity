using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is an abstract implementation of a Player is a human type.
// Degree of movement for most human players remains the same and thus has virtual implementation - this can be overriden for specif model orientation if required.
//Initializing, playing and dieing logic is specific to type of human player and hence are abstract methods.

public abstract class Player : MonoBehaviour, IPlayer, IMoveForwardBackward, IMoveRightLeft, IYawForwardBackward
{
    
    public PlayerTypes Type;
    public float Score = 0.0f;
    public float PowerFactor = 1f;
    public float SizeFactor = 1f;
    public float MaxSizeFactor = 4;
    public float MaxPowerFactor = 1000;

    public abstract void InitializePlayer();
    public abstract void Play();
    public abstract void GetKilled(GameObject killer);

    public abstract void SetPlayerType(PlayerTypes type);
    public virtual void MoveForward(float translateSpeed)
    {
        this.transform.Translate(this.transform.forward * translateSpeed * Time.deltaTime, Space.World);
    }
    public virtual void MoveBackward(float translateSpeed)
    {
        this.transform.Translate(-this.transform.forward * translateSpeed * Time.deltaTime, Space.World);
    }
    public virtual void MoveRight(float translateSpeed)
    {
        this.transform.Translate(this.transform.right * translateSpeed * Time.deltaTime,Space.World);
    }
    public virtual void MoveLeft(float translateSpeed)
    {
        this.transform.Translate(-this.transform.right * translateSpeed * Time.deltaTime,Space.World);
    }
    public virtual void MoveYawForward(float yawSpeed)
    {
        this.transform.Rotate(this.transform.up * yawSpeed * Time.deltaTime);
    }
    public virtual void MoveYawBackward(float yawSpeed)
    {
        this.transform.Rotate(-this.transform.up * yawSpeed * Time.deltaTime);
    }
}

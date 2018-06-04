using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In the current implementation the  
public class TreeDemon : Demon, ITarget, ICanKillPlayer, IPlayerCanKill, IGivesPlayerScore ,IAnimate, INeedsPlaneReference
{
    public TreeDemon() { }
    public DemonTypes Type;

    protected float attackSpeed = 2f;
    protected GameObject Threat;
    protected float MinKillStrength;
    private Animator treeDemon;
    private int hashAnimatorHeight;
    private int hashAnimatorDie;

    //Testing
    [NonSerialized] public float hitPower = 0.0f;
    [NonSerialized] public AmmunitionTypes hitByAmmo;
    [NonSerialized] public PlayerTypes hitByPlayer;
    [NonSerialized] public float onKillReward = 0.0f;


    //Not implemented
    public void SetupAnimatorHandle() { }
    public void Animate() { }

    //Demon is intialized to set its properties
    public override void InitializeDemon(DemonTypes type, float speed, GameObject threat, float minKillStrength)
    {
        Type = type;
        attackSpeed = speed;
        Threat = threat;
        MinKillStrength = minKillStrength;
        PlayerCanKill killType = Type.CastAnyToAny<PlayerCanKill, DemonTypes>(PlayerCanKill.NULL);
        if (killType != PlayerCanKill.NULL)
        {
            onKillReward = (float)killType;
        }
    }

    //Everything a demon does before getting hit
    public void BeforeHit()
    {
        MoveLookAt(Threat.transform, attackSpeed);
    }

    //How a Demon attacks - logical as well as animator operations
    public override void Attack()
    {
         BeforeHit();

    }

    // Function to be called by ammunitions on hitting this type
    //this object checks if the ammuntion belongs to the player.
    //If so, since it is a PlayerCanKill type it accepts the hit.
    public void OnHit<T>(T hitMessage)
    {
        if (typeof(T) == typeof(Ammunition))
        {
           Ammunition ammunition = hitMessage as Ammunition;  

           foreach (PlayerAmmunition ammo in Enum.GetValues(typeof(PlayerAmmunition)))
           {
             if (ammo.CastToArtifact<PlayerAmmunition>() == ammunition.AmmunitionType.CastToArtifact<AmmunitionTypes>())
             {
                 hitByAmmo = ammunition.AmmunitionType;
                 PlayerTypes playerType = ammunition.ownerType.CastFromArtifact<PlayerTypes>(PlayerTypes.NULL);
                    if (playerType != PlayerTypes.NULL)
                    {
                        hitByPlayer = playerType;
                        hitPower = ammunition.Strength;
                        ReceivePlayerKill(hitByPlayer, hitPower); ;
                    }
                 break;
             }
           }
        }
    }

    //This function checks the strength of the hit received from the player
    //If greater than the minimum hit strength - the objects Dies.
    public void ReceivePlayerKill(PlayerTypes playerType, float killStrength)
    {
        if (killStrength >= MinKillStrength)
        {
            SetPlayerScore(playerType);
            Die();
        }
    }

    //Sequence of Logical and Animator actions to be performed on getting killed.
    public override void Die()
    {
        Debug.Log("Demon - " + Type.ToString() + "- Dead!");
        this.gameObject.SetActive(false);
    }

    //Function for player to score
    public void SetPlayerScore(PlayerTypes playerType)
    {
        if (GameObject.FindGameObjectWithTag(playerType.ToString()).GetComponent<IReceivesReward<float>>() !=null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerType.ToString()) as GameObject;
            try
            {
                player.GetComponent<IReceivesReward<float>>().ReceiveReward(RewardTypes.EnemyKill, onKillReward);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            Debug.Log(playerType.ToString() + " rewarded " + onKillReward.ToString() + " points");
        }
    }

    //Since this object can kill player - it is triggered by the kill condition and then send the getkilled message to the player
    public void killPlayer(GameObject attackedObject)
    {
        if (attackedObject.GetComponent<IPlayer>() != null)
        {
            attackedObject.GetComponent<IPlayer>().GetKilled(this.gameObject);
        }
    }

    // to get the terrain reference - not implemented
    [PlaneReferenceProvider(PlaneType = PlaneTypes.Terrain)]
    public GameObject AddPlaneReference(GameObject obj)
    {
        return null;
    }
}

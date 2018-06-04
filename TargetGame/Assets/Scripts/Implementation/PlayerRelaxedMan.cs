using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRelaxedMan : Player, IUserControlled, IhasShootDevice, IReceivesReward<float>, IAnimatorHandler<string, float>, IAnimate<float>
{
    //Motion Related
    public float TranslateSpeed = 10;
    public float TranslateAcceleration = 1.0f;
    public float TurnSpeed = 5;
    public float RotateSpeed = 10;

    //Ammunition Related
    public GameObject AmmunitionPrefab;
    public int AmmunitionCount = 20;
    public bool hasAmmunitionReserve = true;
    public int AmmunitionReserveCount = 50;
    ShootDevice shootDevice;

    //Animation Related
    public float AnimatorSpeed = 0;
    private Animator RelaxedMan;
    private int hashAnimatorSpeed;
    float[] rewards;


    //Function that maps this player's internal behavior to the external IPlayer behavior known to other classes through the PlayerTypes Enum
    public override void SetPlayerType(PlayerTypes type)
    {
        Type = type;
    }

    //Setup Animator Component 
    public void SetupAnimatorHandle()
    {
        if (this.GetComponentInChildren<Animator>() != null)
        {
            RelaxedMan = this.GetComponentInChildren<Animator>();
        }
        hashAnimatorSpeed = Animator.StringToHash("Speed");
    }

    //Function to handle Animation changes under different conditions
    public void AnimatorHandle(string conditionParameter, float conditionValue)
    {
        if (RelaxedMan != null)
        {
            if (conditionParameter == "Speed")
            {
                RelaxedMan.SetFloat(hashAnimatorSpeed, conditionValue);
            }
        }
    }

    public void Animate(float sizeFactor)
    {
        this.transform.localScale *= SizeFactor;
    }


    //Reward Portals will Initialize this function on Trigger Enter if the type Signature of the collided object has this IReceivesReward interface
    //Function used to communicate with Reward Portals
    //Also gets Rewards from IPlayerCanKill type objects through this function

    public void ReceiveReward(RewardTypes Rewardtype, float RewardValue)
    {
        switch (Rewardtype)
        { 
            case RewardTypes.SizeUp:
                if (SizeFactor < MaxSizeFactor)
                {
                    SizeFactor = (1 + RewardValue) * SizeFactor;
                    if (SizeFactor >= MaxSizeFactor) { SizeFactor = MaxSizeFactor; }
                    try
                    {
                        ScoreBoard.Board.UpdateScoreboard("SizeFactor", SizeFactor);
                    }
                    catch (System. Exception e)
                    {
                        Debug.Log(e);
                    }
                    Animate(RewardValue);
                }
                break;
            case RewardTypes.PowerUp:
                if (PowerFactor < MaxPowerFactor)
                {
                    Score += Score * RewardValue;
                    PowerFactor = (1 + RewardValue) * PowerFactor;
                    if (PowerFactor >= MaxPowerFactor) { PowerFactor = MaxPowerFactor; }
                    try
                    {
                        ScoreBoard.Board.UpdateScoreboard("Score", Score);
                        ScoreBoard.Board.UpdateScoreboard("PowerFactor", PowerFactor);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e);
                    }
                    
                }
                break;
            case RewardTypes.EnemyKill:
                Score += RewardValue*PowerFactor;
                try
                {
                    ScoreBoard.Board.UpdateScoreboard("Score", Score);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e);
                }
                Debug.Log("SCORE!!");
                //Scoreboard Code Modification
                break;
        }
        
    }

    //Function called to load shoot devices a player has with the ammunition the player has
    public void loadShootDevice()
    {
        shootDevice.Load(AmmunitionPrefab, AmmunitionCount, hasAmmunitionReserve, AmmunitionReserveCount);
    }

    public void UserControlHandler()
    {
        //maps the degree of motion of the player to the Control Actions
        if (DesktopContollerA.Controls.ForwardControl())
        {
            TranslateSpeed = TranslateSpeed + TranslateAcceleration * Time.deltaTime;
            MoveForward(TranslateSpeed);
        }
        if (DesktopContollerA.Controls.BackwardControl())
        {
            TranslateSpeed = TranslateSpeed + TranslateAcceleration * Time.deltaTime;
            MoveBackward(TranslateSpeed);
        }
        if (DesktopContollerA.Controls.RightControl())
        {
            MoveRight(TurnSpeed);
        }
        if (DesktopContollerA.Controls.LeftControl())
        {
            MoveLeft(TurnSpeed);
        }
        if (DesktopContollerA.Controls.YawBackwardControl())
        {
            MoveYawBackward(RotateSpeed);
        }
        if (DesktopContollerA.Controls.YawForwardControl())
        {
            MoveYawForward(RotateSpeed);
        }
    }


    public override void InitializePlayer()
    {
        //Get all shootdevices a player has
        shootDevice = this.GetComponentInChildren<ShootDevice>();

        //Setup Animators on the Player
        SetupAnimatorHandle();

        //load the shoot devicess
        loadShootDevice();

        //initialize the scoreboard
        //Currently it loads default scores - could potentially load from previous game states
        //rewards = new float[] { base.Score, base.PowerFactor, base.SizeFactor };

        ScoreBoard.Board.UpdateScoreboard("Score", Score);
        ScoreBoard.Board.UpdateScoreboard("PowerFactor", PowerFactor);
        ScoreBoard.Board.UpdateScoreboard("SizeFactor", SizeFactor);

    }

    public override void Play()
    {
        //get current Position
        float currentposition = this.transform.position.magnitude;
        //if any control action performed - perform actions and come back
        UserControlHandler();
        //get current position after control action
        float previousposition = this.transform.position.magnitude;
        //calculate the speed change due to control action
        AnimatorSpeed = Mathf.Abs(currentposition - previousposition)/Time.deltaTime;
        //send the speed information to the Speed controlled Animator Handle.
        AnimatorHandle("Speed", AnimatorSpeed);
    }

    //When kill condition is encountered this function is called to check if the envoker has killing priviledge; if so player is killed
    public override void GetKilled(GameObject killer)
    { 
        if (killer.GetComponent<ICanKillPlayer>() != null)
        {
            gameObject.SetActive(false);
            Application.Quit();
        }
    }     
}

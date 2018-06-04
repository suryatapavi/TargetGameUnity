using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayerCanKill
{
    void ReceivePlayerKill(PlayerTypes playerType, float killStrength);
}


public interface IPlayerAmmunition
{
    void SetPlayerInfo(PlayerTypes playerType, float stregnth);
}

public interface ICanKillPlayer
{
    void killPlayer(GameObject attackedObject);
}
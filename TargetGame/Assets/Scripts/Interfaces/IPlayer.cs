using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Any Game Object plays the game for points/ rewards/ ... should implement this interface*/
public interface IPlayer
{
    void SetPlayerType(PlayerTypes type);
    void InitializePlayer();                 // how a player should be initialized
    void Play();                             // how a player should play
    void GetKilled(GameObject killer);                              // how a player dies / resets / loses (may or may not be killed)
}

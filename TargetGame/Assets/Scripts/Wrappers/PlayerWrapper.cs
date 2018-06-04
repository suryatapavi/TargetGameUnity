using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a Player Wrapper class. 
//Irrespective of the Player behavior internal implementation - this script can operate on any IPlayer type of game object.
//This hides the internal Player implementation in a model/prefab/game object and works in a generic way.

public class PlayerWrapper : MonoBehaviour
{
    public PlayerTypes PlayerType = PlayerTypes.ManRelax;
    private IPlayer player;
    private void Awake()
    {
        player = this.gameObject.GetComponent<IPlayer>();
        player.SetPlayerType(PlayerType);
    }

    void Start ()
    {
            player.InitializePlayer();
	}
	
	void Update ()
    {

            player.Play();
	}

    private void OnTriggerEnter(Collider other)
    {
        player.GetKilled(other.gameObject);
    }


}

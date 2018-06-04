using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A demon Wrapper is to be attached to a Demon. 
// Different types of demons can be  controlled  throught this script.
// The implementations are hidden here and method calls are generic for all possible demons for Unity Events and Functions.

public class DemonWrapper : MonoBehaviour
{
    public DemonTypes DemonType = DemonTypes.TreeDemon;
    public float AttackSpeed = 2f;
    public GameObject DemonThreat;
    public float MinimumStrengthToKill=0.0f;
    private Demon thisDemon;
    private ICanKillPlayer playerkiller;

    private void Awake()
    {
        if (this.gameObject.GetComponent<Demon>() != null)
        {
            thisDemon = this.gameObject.GetComponent<Demon>();
           
        }
        if (this.GetComponent<ICanKillPlayer>() !=null)
        {
            playerkiller = this.GetComponent<ICanKillPlayer>();
            
        }        
    }
    void Start ()
    {
        if (thisDemon  != null)
        {
            thisDemon.InitializeDemon(DemonType, AttackSpeed, DemonThreat, MinimumStrengthToKill);
        }
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (playerkiller != null)
        {
            playerkiller.killPlayer(other.gameObject);
        }
    }

    private void Update()
    {
        if (thisDemon != null)
        {
            thisDemon.Attack();
        }
    }
}

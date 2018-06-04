using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Collection of all Game Artifacts*/
public enum ArtifactTypes
{
    SizeUp,
    PowerUp,
    EnemyKill,
    TreeDemon,
    IceDemon,
    FireDemon,
    Monster,
    ManRelax,
    Vampire,
    Bullet,
    Gun,
    NULL
    
}

/*Collection of types of Rewards used in this game*/
public enum RewardTypes
{
    PowerUp,
    SizeUp,
    EnemyKill,
    NULL
}

/*Collection of Player Types*/
public enum PlayerTypes
{
    ManRelax,
    NULL
}

/*Collection of Demon Types*/
public enum DemonTypes
{
    TreeDemon,
    IceDemon,
    FireDemon,
    NULL
}

/*Collection of all Ammunition Types*/
public enum AmmunitionTypes
{
    Bullet,
    NULL

}

/* Types of Artifacts that a player can kill - and the points for killing each type*/
public enum PlayerCanKill
{
    TreeDemon = 100,
    IceDemon = 200,
    FireDemon = 300,
    Monster = 50,
    Zombie = 30,
    Vampire = 20,
    NULL=0
}

/* Artifact types that can kill a player*/
public enum PlayerKillers
{
    TreeDemon,
    IceDemon,
    FireDemon,
    Monster,
    NULL
}

/*Types of Ammunitions a player can carry*/
public enum PlayerAmmunition
{
    Bullet,
    NULL
}


//Collection of types that can disable a reward portal
public enum DisablesReward
{
    ManRelax,
}


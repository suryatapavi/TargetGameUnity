using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// different kinds of interfaces to implement Unity's Animator type game object
// if a gameobject has a Animator controller it should implement one of those to control the animation.

public interface IAnimatorHandler<T1, T2>
{
    void SetupAnimatorHandle();
    void AnimatorHandle(T1 AnimatorParameter, T2 AnimatorState);
}

public interface IAnimatorHandler<T1>
{
    void SetupAnimatorHandle();
    void AnimatorHandle(T1 AnimatorParameter);
}

public interface IAnimatorHandler
{
    void SetupAnimatorHandle();
    void AnimatorHandle<T1>(T1 AnimatorParameter);
}


// a programatic implementation of animations
//any game object that implements such animations should implement such an interface
public interface IAnimate<T1, T2>
{
    void Animate(T1 AnimatorParameter, T2 AnimatorState);
}

public interface IAnimate<T1>
{
    void Animate(T1 AnimatorParameter);
}

public interface IAnimate
{
    void Animate();
}

public interface IAnimateGeneric<T>
{
    T Animate();
}

public interface IAnimateGeneric<T1,T2>
{
    T1 Animate(T2 animationParam1);
}


public interface IAnimateGeneric<T1,T2,T3>
{
    T1 Animate(T2 animationParam1,T3 animationParam2);
}
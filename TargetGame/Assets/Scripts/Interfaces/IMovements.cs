using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// interfaces for different kinds of movements - along or about different directions
// those that have such a degree of freedom should implement respective interfaces
public interface IMoveForwardBackward
{
    void MoveForward(float translateSpeed);
    void MoveBackward(float translateSpeed);
}

public interface IMoveRightLeft
{
    void MoveRight(float translateSpeed);
    void MoveLeft(float translateSpeed);
}

public interface IMoveVectorDirection
{
    void MoveVectorDirection(float translateSpeed);
}

public interface IMoveLookAt
{
   void  MoveLookAt(Transform target, float translateSpeed);
}

public interface IMoveUpDown
{
    void MoveUp(float bobbingSpeed);
    void MoveDown(float bobbingSpeed);
}

public interface IPitchUpDown
{
    void MovePitchUp(float pitchSpeed);
    void MovePitchDown(float pitchSpeed);

}
public interface IRollRightLeft
{
    void MoveRollRight(float rollSpeed);
    void MoveRollLeft(float rollSpeed);
}
public interface IYawForwardBackward
{
    void MoveYawForward(float yawSpeed);
    void MoveYawBackward(float yawSpeed);
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

//interface related to a controller
//any game object that maps user input to the game world should implement the interfaces as per available functionality

// if the game object is a controller
public interface IController
{
    StringBuilder ControlActions();
}

// if a controller has the following controls - expandable file as per controller hardware
public interface IControllerPrimaryTrigger
{
    bool PrimaryTrigger();
}

public interface IControllerSeconndaryTrigger
{
    bool SecondaryTrigger();
}

public interface IControllerForwardBackward
{
    bool ForwardControl();
    bool BackwardControl();
}

public interface IControllerRightLeft
{
    bool RightControl();
    bool LeftControl();
}

public interface IControllerUpDown
{
    bool UpControl();
    bool DownControl();
}

public interface IControllerPitch
{
    bool PitchUpControl();
    bool PitchDownControl();

}
public interface IControllerRoll
{
    bool RollRightControl();
    bool RollLeftControl();
}
public interface IControllerYaw
{
    bool YawForwardControl();
    bool YawBackwardControl();
}

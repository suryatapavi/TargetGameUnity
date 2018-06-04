using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

//This is a Special Type of a controller.
//This class returns a Singleton reference to the Controller to be used from anywhere in the game.
//This class maps different hardware inputs to generic control actions.

public class DesktopContollerA : MonoBehaviour, IController, IControllerPrimaryTrigger, IControllerSeconndaryTrigger, IControllerForwardBackward, IControllerRightLeft, IControllerPitch, IControllerYaw
{
    public static DesktopContollerA Controls;

    void Awake()
    {
        Controls = this;
        Debug.Log("Controller Initialized");
    }

    public StringBuilder ControlActions()
    {
        StringBuilder actions = new StringBuilder();
        actions.Append("Primary Trigger: Mouse 0 - ");
        actions.Append("Secondary Trigger: RightShift - ");
        actions.Append("Forward Key: W - ");
        actions.Append("Backward Key: S - ");
        actions.Append("Rotate Right Key: A - ");
        actions.Append("Rotate Left Key: D - ");
        actions.Append("Rotate Up Key: UpArrow - ");
        actions.Append("Rotate Down Key: DownArrow - ");
        actions.Append("Righ Key: E - ");
        actions.Append("Left Key: Q - ");
        return actions;
    }

    public bool PrimaryTrigger()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }
    public bool SecondaryTrigger()
    {
        return Input.GetKey(KeyCode.RightShift);
    }
    public bool ForwardControl()
    {
        return Input.GetKey(KeyCode.W);
    }
    public bool BackwardControl()
    {
        return Input.GetKey(KeyCode.S);
    }
    public bool RightControl()
    {
        return Input.GetKey(KeyCode.E);
    }
    public bool LeftControl()
    {
        return Input.GetKey(KeyCode.Q);
    }
    public bool PitchUpControl()
    {
        return Input.GetKey(KeyCode.UpArrow);
    }
    public bool PitchDownControl()
    {
        return Input.GetKey(KeyCode.DownArrow);
    }
    public bool YawForwardControl()
    {
        return Input.GetKey(KeyCode.D);
    }
    public bool YawBackwardControl()
    {
        return Input.GetKey(KeyCode.A);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This is a Extension class to convert custom enums to ArtifactTypes enum for comparison and fetches during run time.
public static class CastArtifacts
{
    public static ArtifactTypes CastToArtifact<T>(this T input)
    {
        string inputstring = "";
        try
        {
            inputstring = input.ToString();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Input is not a defined Enum");
        }
        if (inputstring.Length != 0)
        {
            try
            {
                ArtifactTypes returnvalue;
                ArtifactTypes artifact = (ArtifactTypes)Enum.Parse(typeof(ArtifactTypes), inputstring);
                if (Enum.IsDefined(typeof(ArtifactTypes), artifact))
                {
                    returnvalue = artifact;
                    return returnvalue;
                }

            }
            catch (ArgumentException)
            {
                Debug.Log(input + "is not an Artifact Type");
            }
        }
        return ArtifactTypes.NULL;
    }
    public static T1 CastFromArtifact<T1>(this ArtifactTypes input, T1 defaultvalue)
    {
        string inputstring = "";
        try
        {
            inputstring = input.ToString();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Input is not a defined Enum");
        }
        if (inputstring.Length != 0)
        {
            try
            {
                T1 returnvalue;
                T1 artifact = (T1)Enum.Parse(typeof(T1), inputstring);
                if (Enum.IsDefined(typeof(T1), artifact))
                {
                    returnvalue = artifact;
                    return returnvalue;
                }

            }
            catch (ArgumentException)
            {
                Debug.Log(input + "is not of proper Type");
            }
        }
        return defaultvalue;
    }

    public static T1 CastAnyToAny<T1, T2>(this T2 input, T1 defaultvalue)
    {
        string inputstring = "";
        try
        {
            inputstring = input.ToString();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Input is not a defined Enum");
        }
        if (inputstring.Length != 0)
        {
            try
            {
                T1 returnvalue;
                T1 artifact = (T1)Enum.Parse(typeof(T1), inputstring);
                if (Enum.IsDefined(typeof(T1), artifact))
                {
                    returnvalue = artifact;
                    return returnvalue;
                }

            }
            catch (ArgumentException)
            {
                Debug.Log(input + "is not of proper Type");
            }
        }
        return defaultvalue;
    }
}
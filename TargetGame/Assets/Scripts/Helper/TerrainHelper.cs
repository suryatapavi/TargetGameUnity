using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Extension Method to add Terrain height reference to game objects.

public static class TerrainHelper
{ 
    public static GameObject AdjustTerrainHeight(this GameObject objectToAdjust)
    {
        if (objectToAdjust != null)
        {
            Vector3 pos = new Vector3(objectToAdjust.transform.position.x, 0, objectToAdjust.transform.position.z);
            float height = Terrain.activeTerrain.SampleHeight(pos);
            pos.y = height + objectToAdjust.transform.lossyScale.y / 2.0f;
            objectToAdjust.transform.position = pos;
        }
        return objectToAdjust;
    }
}

public enum PlaneTypes
{
    Terrain,
    Plane
}


public class PlaneReferenceProvider:Attribute
{
    public PlaneTypes PlaneType { get; set; }
}

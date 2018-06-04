using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface to be implemented by objects that need reference to Scene Plane
public interface INeedsPlaneReference
{
    GameObject AddPlaneReference(GameObject objectonPlane);
}

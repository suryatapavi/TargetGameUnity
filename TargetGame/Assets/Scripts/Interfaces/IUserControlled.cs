using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Any Game Object that is controlled by user should implement this interface*/

public interface IUserControlled
{
    void UserControlHandler();              // map for controller actions
}

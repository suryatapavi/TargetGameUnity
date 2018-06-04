using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Any Game Object that shows Players Score/Life/... should implement this interface*/
public interface IScoreBoard
{
    void UpdateScoreboard(string field, float value);
}

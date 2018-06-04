using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

// this is a script to be attached to a scoreboard 
// this allows different events and gameobjects to update scores on the scoreboard

public class ScoreBoard : MonoBehaviour, IScoreBoard
{
    public static ScoreBoard Board;

    public Text ControlActions;
    public Text Scoreboard;
    private string ScoreboardText;
    private string ControlText;
    private Regex ScoreRgx;

    void Awake()
    {
        Board = this;
        ControlActions.text = GameObject.Find("Controller").GetComponent<IController>().ControlActions().ToString();
        ScoreboardText = "Score: 0 SizeFactor: 1 PowerFactor: 1";
        Scoreboard.text = ScoreboardText;
    }

    public void UpdateScoreboard(string field, float value)
    {
        print(value);
        string input =  field + ": ";
        string pattern = input + @"[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)";
        ScoreRgx = new Regex(pattern);
        ScoreboardText = ScoreRgx.Replace(ScoreboardText, input + value.ToString());
        Scoreboard.text = ScoreboardText;
    }
}

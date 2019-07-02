using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] Text[] bowlScore;
    [SerializeField] Text[] frameScore;


    public void FillRollCard (List <int> rolls)
    {
        string scoresString = FormatRolls(rolls);
        for (int i = 0; i < scoresString.Length; i++)
        {
            bowlScore[i].text = scoresString[i].ToString();
        }
    }

    public void FillFrames (List <int> frames)
    {
        for (int i=0; i<frames.Count; i++)
        {
            frameScore[i].text = frames[i].ToString(); 
        }
    }

    public static string FormatRolls (List<int> rolls)
    {
        string output = "";
        int roll = 0;

        for (int i=0; i<rolls.Count; i++)
        {
            roll++;
            if (rolls[i] == 0)
            { output += "-"; }
            else if ((roll % 2 == 0 || roll == 21) && (rolls[i - 1] + rolls[i]) == 10)
            {
                if (roll == 21 && rolls[i]==10)
                { output += "X"; }
                else { output += "/"; }
            }
            else if (rolls[i] == 10)
            {
                if (roll >= 19)
                { output += "X"; }
                else { output += "X "; roll++; }
            }
            else { output += rolls[i].ToString(); }
        }
            return output;
    }
}
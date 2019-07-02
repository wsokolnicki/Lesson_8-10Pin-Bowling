using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public static class ActionMaster
{
    public enum Action { Tidy, Reset, EndTurn, EndGame, Undefined };

    public static Action NextAction(List<int> rolls)
    {

        Action nextAction = Action.Undefined;

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < rolls.Count; i++)
        {
            builder.Append(rolls[i].ToString()).Append(" ");
        }

        List<int> temp = new List<int>();
        foreach (var numb in rolls)
        {
            temp.Add(numb);
        }

        for (int i = 0; i < temp.Count; i++)
        { // Step through rolls

            if (i == 20)
            {
                nextAction = Action.EndGame;
            }
            else if (i >= 18 && temp[i] == 10)
            { // Handle last-frame special cases
                nextAction = Action.Reset;
            }
            else if (i == 19)
            {
                if (temp[18] == 10 && temp[19] == 0)
                {
                    nextAction = Action.Tidy;
                }
                else if (temp[18] + temp[19] == 10)
                {
                    nextAction = Action.Reset;
                }
                else if (temp[18] + temp[19] >= 10)
                {  // Roll 21 awarded
                    nextAction = Action.Tidy;
                }
                else
                {
                    nextAction = Action.EndGame;
                }
            }
            else if (i % 2 == 0)
            { // First bowl of frame
                if (temp[i] == 10)
                {
                    temp.Insert(i + 1, 0); // Insert virtual 0 after strike
                    i++;
                    nextAction = Action.EndTurn;
                }
                else
                {
                    nextAction = Action.Tidy;
                }
            }
            else
            { // Second bowl of frame
                nextAction = Action.EndTurn;
            }
        }

        return nextAction;
    }
}
// MY ACTION MASTER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster2
{
    public enum Action
    {
        Tidy,
        Reset,
        EndTurn,
        EndGame
    }
    int[] bowls = new int[21];  //Array with number of throw
    private int bowl = 1; // one, becouse it is first ball in this round


    public static Action NextAction(List<int> pinFalls)
    {
        ActionMaster2 am = new ActionMaster2();
        Action currentAction = new Action();
        foreach (int pinFall in pinFalls)
        {
            currentAction = am.Bowl(pinFall);
        }

        return currentAction;
    }

    private Action Bowl(int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new UnityException("Invalid pin number");
        }

        bowls[bowl - 1] = pins;

        if (bowl == 21)
        {
            return Action.EndGame;
        }

        if (bowl == 19 && pins == 10)
        {
            bowl += 1;
            return Action.Reset;
        }

        if (bowl == 20)
        {
            if (AllPinsDown())
            {
                if (bowls[20 - 1] != 0)
                {
                    bowl += 1;
                    return Action.Reset;
                }
                else { return Action.Tidy; }
            }
            else if (Bowl21Awarded())
            {
                bowl += 1;
                return Action.Tidy;
            }
            else
            {
                return Action.EndGame;
            }
        }

        if (bowl % 2 != 0)
        {
            if (pins == 10)
            {
                bowl += 2;
                return Action.EndTurn;
            }
            else
            {
                bowl += 1;
                return Action.Tidy;
            }
        }

        else if (bowl % 2 == 0)
        {
            if (bowl == 20 && !Bowl21Awarded())
            {
                return Action.EndGame;
            }
            else
            {
                bowl += 1;
                return Action.EndTurn;
            }
        }
        throw new UnityException("Not sure what action to return!");
    }

    private bool AllPinsDown()
    {
        return ((bowls[19 - 1] + bowls[20 - 1]) % 10 == 0);
    }

    private bool Bowl21Awarded()
    {
        return (bowls[19 - 1] + bowls[20 - 1] > 10);
    }
}

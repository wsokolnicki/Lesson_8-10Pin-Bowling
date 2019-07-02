using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private List<int> pinFalls;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    [SetUp]
    public void Setup()
    {
        pinFalls = new List<int>();
    }

    [Test]
    public void PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_OneStrikeReturnsEndTurn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T02_Bowl8ReturnsTidy()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T03_BowlSpareReturnsEndTurn()
    {
        pinFalls = new List<int> { 8, 2 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T04_CheckResetAtStrikeLastFrame()
    {
        pinFalls = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; //18
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T05_CheckResetAtSpareLastFrame()
    {
        pinFalls = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };//18
        pinFalls.Add(5); //bowl 19
        pinFalls.Add(5); //bowl 20
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T06_CheckEndGameAtLastFrame_Bowl20()
    {
        pinFalls = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; //18
        pinFalls.Add(1); //bowl 19
        pinFalls.Add(7); //bowl 20
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T07_YouTubeCheck()
    {
        pinFalls = new List<int> { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2 };//20
        pinFalls.Add(9); //bowl 21
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T08_StrikeAt19thFrameAnd21Bowl()
    {
        pinFalls = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 }; //19
        pinFalls.Add(8); //bowl 20
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T09_StrikeAt19thFrameAnd0At20th()
    {
        pinFalls = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 }; //19
        pinFalls.Add(0); //bowl 20
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T10_1To9FirstBowlIs0ThanSpare()
    {
        pinFalls = new List<int> { 0, 10 };
        pinFalls.Add(5); //bowl 3
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T11_TripleStrikeLastFrame()
    {
        pinFalls = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; //18
        pinFalls.Add(10); //bowl 19
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10); //bowl 20
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10); //bowl 21
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));

    }

    [Test]
    public void T12()
    {
        pinFalls = new List<int> { 10 };
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }
}
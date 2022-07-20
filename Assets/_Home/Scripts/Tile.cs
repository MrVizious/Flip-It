using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Keeps the information of the tile
/// </summary>
public class Tile
{

    public int owner { get; }
    public int score { get; }

    public Tile(int owner = 0, int score = 0)
    {
        this.owner = owner;
        this.score = score;
    }

    public async Task Flip(int newOwner = 0)
    {
        if (newOwner == 0)
        {
            owner = newOwner;
        }
        else
        {
            ToggleOwner();
        }
        DoubleScore();
        await FlipModel();
    }

    public void ToggleOwner()
    {
        if (owner == 1)
        {
            owner = 2;
        }
        else
        {
            owner = 1;
        }
    }

    public void AddScore(int score = 0)
    {
        this.score += score;
    }

    public void MultiplyScoreBy(int multiplier = 2)
    {
        this.score *= multiplier;
    }

    public void DoubleScore()
    {
        MultiplyScoreBy(2);
    }

    public async Task FlipModel()
    {
        //TODO Flip tile model
    }



}
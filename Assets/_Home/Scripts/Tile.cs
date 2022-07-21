using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


/// <summary>
/// Keeps the information of the tile
/// </summary>
public class Tile
{
    public int owner { get; private set; }
    public int score { get; private set; }
    public TileModel model { get; private set; }

    public Tile(int score = 0, TileModel model = null, int owner = 0)
    {
        this.owner = owner;
        this.score = score;
        this.model = model;
    }

    public async Task FirstAssignment(int owner)
    {
        this.owner = owner;
        await model.InitializeModel(owner);
    }

    public async Task Flip()
    {
        ToggleOwner();
        DoubleScore();
        await model.FlipModel();
    }

    public void ToggleOwner()
    {
        owner = owner == 1 ? 2 : 1;
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



}
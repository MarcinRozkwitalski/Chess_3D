using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : PieceInfo
{
    public void Movement()
    {
        gameObject.GetComponent<Bishop>().Movement();
        gameObject.GetComponent<Rook>().Movement();
    }

    public void BeatableTiles()
    {
        gameObject.GetComponent<Bishop>().BeatableTiles();
        gameObject.GetComponent<Rook>().BeatableTiles();
    }

    public void CheckIfCanDoMoves()
    {
        gameObject.GetComponent<Bishop>().CheckIfCanDoMoves();
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        gameObject.GetComponent<Rook>().CheckIfCanDoMoves();
    }
}
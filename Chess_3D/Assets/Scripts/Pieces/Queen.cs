using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : PieceInfo
{
    public void Movement()
    {
        if(gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            //która strona
        }
        else if(!gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            gameObject.GetComponent<Bishop>().Movement();
            gameObject.GetComponent<Rook>().Movement();
        }
    }

    public void BeatableTiles()
    {
        gameObject.GetComponent<Bishop>().BeatableTiles();
        gameObject.GetComponent<Rook>().BeatableTiles();
    }

    public void CheckIfCanDoMoves()
    {
        gameObject.GetComponent<Bishop>().CheckIfCanDoMoves();
        if(!gameObject.GetComponent<PieceInfo>()._canDoMoves)
        gameObject.GetComponent<Rook>().CheckIfCanDoMoves();
    }

    public void IterateForDefendingPieces()
    {
        gameObject.GetComponent<Bishop>().IterateForDefendingPieces();
        if(!gameObject.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece)
        gameObject.GetComponent<Rook>().IterateForDefendingPieces();
    }
}
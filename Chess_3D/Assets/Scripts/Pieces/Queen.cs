using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : PieceInfo
{
    public void Movement()
    {
        if(_whichSide == 0 && GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked)
        {
            //check if this piece can beat checking ONE piece
        }
        else if(_whichSide == 1 && GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked)
        {
            //check if this piece can beat checking ONE piece
        }
        else if(gameObject.GetComponent<PieceInfo>()._isDefendingKing)
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

    public void CheckForCheckIfIsCheckingEnemyKing()
    {
        gameObject.GetComponent<Bishop>().CheckForCheckIfIsCheckingEnemyKing();
        if(!gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing)
        gameObject.GetComponent<Rook>().CheckForCheckIfIsCheckingEnemyKing();
    }

    public void IterateForDefendingPieces()
    {
        gameObject.GetComponent<Bishop>().IterateForDefendingPieces();
        if(!gameObject.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece)
        gameObject.GetComponent<Rook>().IterateForDefendingPieces();
    }
}
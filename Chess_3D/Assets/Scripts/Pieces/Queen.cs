using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : PieceInfo
{
    new public void SetPosition()
    {
        x = gameObject.GetComponent<PieceInfo>().new_x;
        z = gameObject.GetComponent<PieceInfo>().new_z;
    }
    
    public void Movement()
    {
        if(_whichSide == 0 && GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked)
        {
            gameObject.GetComponent<Bishop>().Movement();
            gameObject.GetComponent<Rook>().Movement();
        }
        else if(_whichSide == 1 && GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked)
        {
            gameObject.GetComponent<Bishop>().Movement();
            gameObject.GetComponent<Rook>().Movement();
        }
        else if(gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            gameObject.GetComponent<Bishop>().Movement();
            gameObject.GetComponent<Rook>().Movement();
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
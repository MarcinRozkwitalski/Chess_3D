using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PieceInfo
{
    public void Movement()
    {
        SetPosition();

        if(_whichSide == 0 && GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked)
        {
            z++; z++; x++;
            CheckMovementForBlockingAndBeating(x, z);

            z++; z++; x--;
            CheckMovementForBlockingAndBeating(x, z);

            z--; z--; x++;
            CheckMovementForBlockingAndBeating(x, z);

            z--; z--; x--;
            CheckMovementForBlockingAndBeating(x, z);

            x++; x++; z--;
            CheckMovementForBlockingAndBeating(x, z);

            x++; x++; z++;
            CheckMovementForBlockingAndBeating(x, z);

            x--; x--; z--;
            CheckMovementForBlockingAndBeating(x, z);

            x--; x--; z++;
            CheckMovementForBlockingAndBeating(x, z);
        }
        else if(_whichSide == 1 && GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked)
        {
            z++; z++; x++;
            CheckMovementForBlockingAndBeating(x, z);

            z++; z++; x--;
            CheckMovementForBlockingAndBeating(x, z);

            z--; z--; x++;
            CheckMovementForBlockingAndBeating(x, z);

            z--; z--; x--;
            CheckMovementForBlockingAndBeating(x, z);

            x++; x++; z--;
            CheckMovementForBlockingAndBeating(x, z);

            x++; x++; z++;
            CheckMovementForBlockingAndBeating(x, z);

            x--; x--; z--;
            CheckMovementForBlockingAndBeating(x, z);

            x--; x--; z++;
            CheckMovementForBlockingAndBeating(x, z);
        }
        else if(gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            //która strona itd
        }
        else if(!gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            z++; z++; x++;
            CheckMovement(x, z);

            z++; z++; x--;
            CheckMovement(x, z);

            z--; z--; x++;
            CheckMovement(x, z);

            z--; z--; x--;
            CheckMovement(x, z);

            x++; x++; z--;
            CheckMovement(x, z);

            x++; x++; z++;
            CheckMovement(x, z);

            x--; x--; z--;
            CheckMovement(x, z);

            x--; x--; z++;
            CheckMovement(x, z);
        }
    }

    public void BeatableTiles()
    {
        SetPosition();

        z++; z++; x++;
        CheckBeatableTiles(x, z);

        z++; z++; x--;
        CheckBeatableTiles(x, z);

        z--; z--; x++;
        CheckBeatableTiles(x, z);

        z--; z--; x--;
        CheckBeatableTiles(x, z);

        x++; x++; z--;
        CheckBeatableTiles(x, z);

        x++; x++; z++;
        CheckBeatableTiles(x, z);

        x--; x--; z--;
        CheckBeatableTiles(x, z);

        x--; x--; z++;
        CheckBeatableTiles(x, z);
    }

    public void CheckMovement(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
            {
                gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
            {
                gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
            {
                gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
            }
        }
        
        SetPosition();
    }

    public void CheckMovementForBlockingAndBeating(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] == null && _whichSide == 0 && gridCreator.chessBoardGrid[x, z].GetComponent<TileInfo>()._canBeBlockedByWhite)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] == null && _whichSide == 1 && gridCreator.chessBoardGrid[x, z].GetComponent<TileInfo>()._canBeBlockedByBlack)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 0 && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && 
                gameHandler._amountOfBlackPiecesCheckingWhiteKing == 1 &&
                chessPiecesGrid.chessPiecesGrid[x, z].GetComponent<PieceInfo>()._isCheckingEnemyKing)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 1 && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && 
                gameHandler._amountOfWhitePiecesCheckingBlackKing == 1 &&
                chessPiecesGrid.chessPiecesGrid[x, z].GetComponent<PieceInfo>()._isCheckingEnemyKing)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
                }
        }
        
        SetPosition();
    }

    public void CheckBeatableTiles(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(_whichSide == 0) gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnWhite();
            if(_whichSide == 1) gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnBlack();
        }

        SetPosition();
    }

    public void CheckIfIsCheckingEnemyKing(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].name == "BlackKing(Clone)")
            {
                gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing = true;
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].name == "WhiteKing(Clone)")
            {
                gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing = true;
            }
        }
        
        SetPosition();
    }

    public void CheckIfCanDoMovement(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] == null && 
                _whichSide == 0 &&
                GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == false && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    gameHandler._possibleWhitePiecesMoves++;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] == null && 
                _whichSide == 1 &&
                GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == false && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    gameHandler._possibleBlackPiecesMoves++;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 0 &&
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && 
                GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == false && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {

                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 1 &&
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && 
                GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == false && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {

                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 0 &&
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && 
                GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == true && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {

                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 1 &&
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && 
                GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == true && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {

                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 0 && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && 
                GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == false && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    gameHandler._possibleWhitePiecesMoves++;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 1 && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && 
                GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == false && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    gameHandler._possibleBlackPiecesMoves++;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 0 && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && 
                GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == true &&
                chessPiecesGrid.chessPiecesGrid[x, z].GetComponent<PieceInfo>()._isCheckingEnemyKing && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    gameHandler._possibleWhitePiecesMoves++;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                _whichSide == 1 && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && 
                GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == true &&
                chessPiecesGrid.chessPiecesGrid[x, z].GetComponent<PieceInfo>()._isCheckingEnemyKing && 
                gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    gameHandler._possibleBlackPiecesMoves++;
                }
        }
        
        SetPosition();
    }

    public void CheckForCheckIfIsCheckingEnemyKing()
    {
        gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing = false;

        SetPosition();

        z++; z++; x++;
        if(gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        z++; z++; x--;
        if(gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        z--; z--; x++;
        if(gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        z--; z--; x--;
        if(gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        x++; x++; z--;
        if(gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        x++; x++; z++;
        if(gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        x--; x--; z--;
        if(gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        x--; x--; z++;
        if(gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);
    }

    public void CheckIfCanDoMoves()
    {
        gameObject.GetComponent<PieceInfo>()._canDoMoves = false;

        SetPosition();

        z++; z++; x++;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        z++; z++; x--;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        z--; z--; x++;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        z--; z--; x--;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        x++; x++; z--;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        x++; x++; z++;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        x--; x--; z--;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        x--; x--; z++;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : PieceInfo
{
    int initX, initZ;

    void Start() {
        initX = (int)gameObject.transform.position.x;
        initZ = (int)gameObject.transform.position.z;
    }

    public void Movement(int _whichSide)
    {
        SetPosition();

        if(_whichSide == 0 && GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked)
        {
            z++;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null && gridCreator.chessBoardGrid[x, z].GetComponent<TileInfo>()._canBeBlockedByWhite)
            {
                gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
            }

            if(chessPiecesGrid.chessPiecesGrid[x, z] != null)
            {
                
            }
            else if(gameObject.transform.position.z == initZ)
            {
                z++;

                if(chessPiecesGrid.chessPiecesGrid[x, z] == null && gridCreator.chessBoardGrid[x, z].GetComponent<TileInfo>()._canBeBlockedByWhite)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
                }
            }

            SetPosition();

            x--; z++;

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && 
                gameHandler._amountOfBlackPiecesCheckingWhiteKing == 1 &&
                chessPiecesGrid.chessPiecesGrid[x, z].GetComponent<PieceInfo>()._isCheckingEnemyKing)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
                }
            }

            SetPosition();

            x++; z++;

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && 
                gameHandler._amountOfBlackPiecesCheckingWhiteKing == 1 &&
                chessPiecesGrid.chessPiecesGrid[x, z].GetComponent<PieceInfo>()._isCheckingEnemyKing)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
                }
            }
        }
        else if(_whichSide == 1 && GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked)
        {
            z--;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null && gridCreator.chessBoardGrid[x, z].GetComponent<TileInfo>()._canBeBlockedByBlack)
            {
                gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
            }

            if(chessPiecesGrid.chessPiecesGrid[x, z] != null)
            {

            }
            else if(gameObject.transform.position.z == initZ)
            {
                z--;

                if(chessPiecesGrid.chessPiecesGrid[x, z] == null && gridCreator.chessBoardGrid[x, z].GetComponent<TileInfo>()._canBeBlockedByBlack)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
                }
            }

            SetPosition();

            x--; z--;

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && 
                gameHandler._amountOfWhitePiecesCheckingBlackKing == 1 &&
                chessPiecesGrid.chessPiecesGrid[x, z].GetComponent<PieceInfo>()._isCheckingEnemyKing)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
                }
            }

            SetPosition();

            x++; z--;

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
                chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && 
                gameHandler._amountOfWhitePiecesCheckingBlackKing == 1 &&
                chessPiecesGrid.chessPiecesGrid[x, z].GetComponent<PieceInfo>()._isCheckingEnemyKing)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
                }
            }
        }
        else if(gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            //która strona itd
        }
        else if(!gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            if(_whichSide == 0)
            {
                z++;
            }
            else if (_whichSide == 1)
            {
                z--;
            }

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
            {
                gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);

                if(gameObject.transform.position.z == initZ)
                {
                    if(_whichSide == 0)
                    {
                        z++;
                    }
                    else if (_whichSide == 1)
                    {
                        z--;
                    }

                    if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                    {
                        gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
                    }
                }
            }

            SetPosition();

            x--; z++;
            if(_whichSide == 0) CheckMovement(x, z);

            SetPosition();

            x++; z++;
            if(_whichSide == 0) CheckMovement(x, z);

            SetPosition();

            x--; z--;
            if(_whichSide == 1) CheckMovement(x, z);

            SetPosition();

            x++; z--;
            if(_whichSide == 1) CheckMovement(x, z);
        }
    }   

    public void BeatableTiles()
    {
        SetPosition();

        if(_whichSide == 0)
        {
            x--; z++;
            if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
            {
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnWhite();
            }

            SetPosition();
            x++; z++;

            if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
            {
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnWhite();
            }
        }
        else if(_whichSide == 1)
        {
            x--; z--;

            if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
            {
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnBlack();
            }

            SetPosition();
            x++; z--;

            if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
            {
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnBlack();
            }
        }
    }

    public void CheckMovement(int x, int z)
    {
        if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
            {
                gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
            {
                gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
            }
        }
    }

    public void CheckIfCanDoMovement(int x, int z)
    {
        if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] != null && 
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
            gameHandler._amountOfBlackPiecesCheckingWhiteKing == 1 &&
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
            gameHandler._amountOfWhitePiecesCheckingBlackKing == 1 &&
            gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                gameHandler._possibleBlackPiecesMoves++;
            }
        }
    }

    public void CheckIfIsCheckingEnemyKing(int x, int z)
    {
        if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
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
    }

    public void CheckIfCanDoMoves()
    {
        gameObject.GetComponent<PieceInfo>()._canDoMoves = false;

        SetPosition();

        if(_whichSide == 0 && 
        GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == false && 
        gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        {
            z++;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                gameHandler._possibleWhitePiecesMoves++;
            }
        }
        else if(_whichSide == 1 && 
        GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == false &&
        gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        {
            z--;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                gameHandler._possibleBlackPiecesMoves++;
            }
        }
        else if(_whichSide == 0 && 
        GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == true &&
        gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        {
            z++;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null &&
            gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._canBeBlockedByWhite == true)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                gameHandler._possibleWhitePiecesMoves++;
            }

            z++;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null &&
            gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._canBeBlockedByWhite == true)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                gameHandler._possibleWhitePiecesMoves++;
            }
        }
        else if(_whichSide == 1 && 
        GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == true && 
        gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        {
            z--;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null &&
            gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._canBeBlockedByBlack == true)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                gameHandler._possibleBlackPiecesMoves++;
            }

            z--;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null &&
            gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._canBeBlockedByBlack == true)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                gameHandler._possibleBlackPiecesMoves++;
            }
        }

        SetPosition();
        x--; z++;
        if(_whichSide == 0 && gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        SetPosition();
        x++; z++;
        if(_whichSide == 0 && gameObject.GetComponent<PieceInfo>()._canDoMoves == false) 
        CheckIfCanDoMovement(x, z);

        SetPosition();
        x--; z--;
        if(_whichSide == 1 && gameObject.GetComponent<PieceInfo>()._canDoMoves == false) 
        CheckIfCanDoMovement(x, z);

        SetPosition();
        x++; z--;
        if(_whichSide == 1 && gameObject.GetComponent<PieceInfo>()._canDoMoves == false) 
        CheckIfCanDoMovement(x, z);
    }

    public void CheckForCheckIfIsCheckingEnemyKing()
    {
        gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing = false;

        SetPosition();

        if(_whichSide == 0)
        {
            z++;
        }
        else if (_whichSide == 1)
        {
            z--;
        }

        SetPosition();
        x--; z++;
        if(_whichSide == 0 && gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        SetPosition();
        x++; z++;
        if(_whichSide == 0 && gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false)
        CheckIfIsCheckingEnemyKing(x, z);

        SetPosition();
        x--; z--;
        if(_whichSide == 1 && gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false) 
        CheckIfIsCheckingEnemyKing(x, z);

        SetPosition();
        x++; z--;
        if(_whichSide == 1 && gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing == false) 
        CheckIfIsCheckingEnemyKing(x, z);
    }
}
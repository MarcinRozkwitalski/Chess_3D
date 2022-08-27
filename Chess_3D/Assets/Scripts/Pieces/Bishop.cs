using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : PieceInfo
{
    public void Movement()
    {
        SetPosition();

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
            //która strona itd
        }
        else if(!gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            CheckMovement("+", "+"); //x, z

            CheckMovement("+", "-"); //x, z

            CheckMovement("-", "-"); //x, z

            CheckMovement("-", "+"); //x, z
        }
    }

    public void BeatableTiles()
    {
        SetPosition();

        CheckBeatableTiles("+", "+"); //x, z

        CheckBeatableTiles("+", "-"); //x, z

        CheckBeatableTiles("-", "-"); //x, z

        CheckBeatableTiles("-", "+"); //x, z
    }

    public void CheckMovement(string ops1, string ops2)
    {
        while(true)
        {
            if(ops1 == "+" && ops2 == "+")
            {
                x++;
                z++;
            }
            else if(ops1 == "+" && ops2 == "-")
            {
                x++;
                z--;
            }
            else if(ops1 == "-" && ops2 == "-")
            {
                x--;
                z--;
            }
            else if(ops1 == "-" && ops2 == "+")
            {
                x--;
                z++;
            }

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
                    break;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
                    break;
                }
                else break;
            }
            else break;
        }

        SetPosition();
    }

    public void CheckBeatableTiles(string ops1, string ops2)
    {
        while(true)
        {
            if(ops1 == "+" && ops2 == "+")
            {
                x++;
                z++;
            }
            else if(ops1 == "+" && ops2 == "-")
            {
                x++;
                z--;
            }
            else if(ops1 == "-" && ops2 == "-")
            {
                x--;
                z--;
            }
            else if(ops1 == "-" && ops2 == "+")
            {
                x--;
                z++;
            }

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(_whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnWhite();
                }
                else if(_whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnBlack();
                }
                else if(_whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].name == "BlackKing(Clone)")
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnWhite();
                }
                else if(_whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].name == "WhiteKing(Clone)")
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnBlack();
                }
                else if(_whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z] != null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnWhite();
                    break;
                }
                else if(_whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z] != null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnBlack();
                    break;
                }
                else break;
            }
            else break;
        }

        SetPosition();
    }

    public void CheckIfCanDoMovement(string ops1, string ops2)
    {
        while(true)
        {
            if(ops1 == "+" && ops2 == "+")
            {
                x++;
                z++;
            }
            else if(ops1 == "+" && ops2 == "-")
            {
                x++;
                z--;
            }
            else if(ops1 == "-" && ops2 == "-")
            {
                x--;
                z--;
            }
            else if(ops1 == "-" && ops2 == "+")
            {
                x--;
                z++;
            }

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    break;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    break;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
                    break;
                }
                else break;
            }
            else break;
        }

        SetPosition();
    }

    public void CheckForPieceDefendingKing(string ops1, string ops2)
    {
        int howManyPieces = 0;
        bool defendingPiece = false;
        int xDefPiecePos = 0;
        int zDefPiecePos = 0;

        while(true)
        {
            if(ops1 == "+" && ops2 == "+")
            {
                x++;
                z++;
            }
            else if(ops1 == "+" && ops2 == "-")
            {
                x++;
                z--;
            }
            else if(ops1 == "-" && ops2 == "-")
            {
                x--;
                z--;
            }
            else if(ops1 == "-" && ops2 == "+")
            {
                x--;
                z++;
            }

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(_whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    break;
                }
                else if(_whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    break;
                }

                if(_whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    if(!defendingPiece)
                    {
                        defendingPiece = true;
                        xDefPiecePos = x;
                        zDefPiecePos = z;
                    }
                    howManyPieces++;
                }
                else if(_whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    if(!defendingPiece)
                    {
                        defendingPiece = true;
                        xDefPiecePos = x;
                        zDefPiecePos = z;
                    }
                    howManyPieces++;
                }

                if(howManyPieces > 2)
                {
                    break;
                }

                if(howManyPieces > 1)
                {
                    if(_whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].name == "BlackKing(Clone)")
                    {
                        chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._isDefendingKing = true;

                        if(ops1 == "+" && ops2 == "+")
                        {
                            chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._attackingPieceDirection = "x+z+";
                        }
                        else if(ops1 == "+" && ops2 == "-")
                        {
                            chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._attackingPieceDirection = "x+z-";
                        }
                        else if(ops1 == "-" && ops2 == "-")
                        {
                            chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._attackingPieceDirection = "x-z-";
                        }
                        else if(ops1 == "-" && ops2 == "+")
                        {                       
                            chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._attackingPieceDirection = "x-z+";
                        }
                        gameObject.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece = true;
                        break;
                    }
                    else if(_whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].name == "WhiteKing(Clone)")
                    {
                        chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._isDefendingKing = true;

                        if(ops1 == "+" && ops2 == "+")
                        {
                            chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._attackingPieceDirection = "x+z+";
                        }
                        else if(ops1 == "+" && ops2 == "-")
                        {
                            chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._attackingPieceDirection = "x+z-";
                        }
                        else if(ops1 == "-" && ops2 == "-")
                        {
                            chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._attackingPieceDirection = "x-z-";
                        }
                        else if(ops1 == "-" && ops2 == "+")
                        {                       
                            chessPiecesGrid.chessPiecesGrid[xDefPiecePos, zDefPiecePos].GetComponent<PieceInfo>()._attackingPieceDirection = "x-z+";
                        }
                        gameObject.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece = true;
                        break;
                    }
                    break;
                }
            }
            else break;
        }

        SetPosition();
    }

    public void CheckIfIsCheckingEnemyKing(string ops1, string ops2)
    {
        while(true)
        {
            if(ops1 == "+" && ops2 == "+")
            {
                x++;
                z++;
            }
            else if(ops1 == "+" && ops2 == "-")
            {
                x++;
                z--;
            }
            else if(ops1 == "-" && ops2 == "-")
            {
                x--;
                z--;
            }
            else if(ops1 == "-" && ops2 == "+")
            {
                x--;
                z++;
            }

            if(-1 < x && x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].name == "BlackKing(Clone)")
                {
                    gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing = true;
                    break;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].name == "WhiteKing(Clone)")
                {
                    gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing = true;
                    break;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null)
                {
                    break;
                }
            }
            else break;
        }

        SetPosition();
    }

    public void CheckIfCanDoMoves()
    {
        gameObject.GetComponent<PieceInfo>()._canDoMoves = false;

        SetPosition();
        
        if(!gameObject.GetComponent<PieceInfo>()._canDoMoves)
        CheckIfCanDoMovement("+", "+"); //x, z

        if(!gameObject.GetComponent<PieceInfo>()._canDoMoves)
        CheckIfCanDoMovement("+", "-"); //x, z

        if(!gameObject.GetComponent<PieceInfo>()._canDoMoves)
        CheckIfCanDoMovement("-", "-"); //x, z

        if(!gameObject.GetComponent<PieceInfo>()._canDoMoves)
        CheckIfCanDoMovement("-", "+"); //x, z
    }

    public void CheckForCheckIfIsCheckingEnemyKing()
    {
        gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing = false;

        SetPosition();
        
        if(!gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing)
        CheckIfIsCheckingEnemyKing("+", "+"); //x, z

        if(!gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing)
        CheckIfIsCheckingEnemyKing("+", "-"); //x, z

        if(!gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing)
        CheckIfIsCheckingEnemyKing("-", "-"); //x, z

        if(!gameObject.GetComponent<PieceInfo>()._isCheckingEnemyKing)
        CheckIfIsCheckingEnemyKing("-", "+"); //x, z
    }

    public void IterateForDefendingPieces()
    {
        if(!gameObject.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece)
        {
            CheckForPieceDefendingKing("+", "+"); //x, z
        }
        if(!gameObject.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece)
        {
            CheckForPieceDefendingKing("+", "-"); //x, z
        }
        if(!gameObject.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece)
        {
            CheckForPieceDefendingKing("-", "+"); //x, z
        }
        if(!gameObject.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece)
        {
            CheckForPieceDefendingKing("-", "-"); //x, z
        }
    }
}
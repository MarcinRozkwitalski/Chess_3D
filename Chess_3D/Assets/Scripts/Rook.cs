using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : PieceInfo
{
    int x, z;

    public void Movement(int _whichSide)
    {
        SetPosition();

        CheckMovement("z", "+");

        CheckMovement("z", "-");

        CheckMovement("x", "+");

        CheckMovement("x", "-");
    }

    public void BeatableTiles(int _whichSide)
    {
        SetPosition();

        CheckBeatableTiles("z", "+");

        CheckBeatableTiles("z", "-");

        CheckBeatableTiles("x", "+");

        CheckBeatableTiles("x", "-");
    }

    public void CheckMovement(string var, string ops)
    {
        while(true)
        {
            if(var == "z" && ops == "+")
            {
                z++;
            }
            else if(var == "z" && ops == "-")
            {
                z--;
            }
            else if(var == "x" && ops == "+")
            {
                x++;
            }
            else if(var == "x" && ops == "-")
            {
                x--;
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

    public void CheckBeatableTiles(string var, string ops)
    {
        while(true)
        {
            if(var == "z" && ops == "+")
            {
                z++;
            }
            else if(var == "z" && ops == "-")
            {
                z--;
            }
            else if(var == "x" && ops == "+")
            {
                x++;
            }
            else if(var == "x" && ops == "-")
            {
                x--;
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

    public void SetPosition()
    {
        x = (int)gameObject.transform.position.x;
        z = (int)gameObject.transform.position.z;
    }
}
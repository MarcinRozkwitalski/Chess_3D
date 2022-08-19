using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : PieceInfo
{
    int x, z;

    int initX, initZ;

    void Start() {
        initX = (int)gameObject.transform.position.x;
        initZ = (int)gameObject.transform.position.z;
    }

    public void Movement(int _whichSide)
    {
        SetPosition();

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

        x++; z++;
        if(_whichSide == 0) CheckMovement(x, z);

        x--; z--;
        if(_whichSide == 1) CheckMovement(x, z);

        x++; z--;
        if(_whichSide == 1) CheckMovement(x, z);
    }   

    public void BeatableTiles(int _whichSide)
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

        SetPosition();
    }

    public void SetPosition()
    {
        x = (int)gameObject.transform.position.x;
        z = (int)gameObject.transform.position.z;
    }
}
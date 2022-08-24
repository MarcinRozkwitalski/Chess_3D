using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : PieceInfo
{
    public bool _isChecked = false;

    public void Movement()
    {
        SetPosition();

        z++; x++;
        CheckMovement(x, z);

        z++; x--;
        CheckMovement(x, z);

        z--; x--;
        CheckMovement(x, z);

        z--; x++;
        CheckMovement(x, z);

        z++;
        CheckMovement(x, z);

        z--;
        CheckMovement(x, z);

        x++;
        CheckMovement(x, z);

        x--;
        CheckMovement(x, z);
    }

    public void BeatableTiles()
    {
        SetPosition();

        z++; x++;
        CheckBeatableTiles(x, z);

        z++; x--;
        CheckBeatableTiles(x, z);

        z--; x--;
        CheckBeatableTiles(x, z);

        z--; x++;
        CheckBeatableTiles(x, z);

        z++;
        CheckBeatableTiles(x, z);

        z--;
        CheckBeatableTiles(x, z);

        x++;
        CheckBeatableTiles(x, z);

        x--;
        CheckBeatableTiles(x, z);
    }

    public void CheckMovement(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] == null && _whichSide == 0 && !gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByBlack)
            {
                gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] == null && _whichSide == 1 && !gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByWhite)
            {
                gameObject.GetComponent<PieceInfo>().SetTileGreen(x, z);
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && !gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByBlack)
            {
                gameObject.GetComponent<PieceInfo>().SetTileRed(x, z);
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && !gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByWhite)
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
            if(_whichSide == 0)
            {
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnWhite();
            }
            else if(_whichSide == 1)
            {
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnBlack();
            }
        }

        SetPosition();
    }

    public void CheckIfCanDoMovement(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] == null && _whichSide == 0 && !gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByBlack)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] == null && _whichSide == 1 && !gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByWhite)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black") && !gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByBlack)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White") && !gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByWhite)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
            }
        }
    }

    public void CheckIfCanDoMoves()
    {
        gameObject.GetComponent<PieceInfo>()._canDoMoves = false;

        SetPosition();
        z++; x++;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        SetPosition();
        z++; x--;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        SetPosition();
        z--; x--;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        SetPosition();
        z--; x++;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        SetPosition();
        z++;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        SetPosition();
        z--;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        SetPosition();
        x++;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);

        SetPosition();
        x--;
        if(gameObject.GetComponent<PieceInfo>()._canDoMoves == false)
        CheckIfCanDoMovement(x, z);
    }

    public void CheckIfChecked()
    {
        _isChecked = false;

        SetPosition();

        if(_whichSide == 0)
        {
            if(gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByBlack == true)
            {
                _isChecked = true;
            }
        }
        else if(_whichSide == 1)
        {
            if(gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>()._isBeatableByWhite == true)
            {
                _isChecked = true;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PieceInfo
{
    public void Movement()
    {
        SetPosition();

        if(gameObject.GetComponent<PieceInfo>()._isDefendingKing)
        {
            //która strona
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

    public void CheckBeatableTiles(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(_whichSide == 0) gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnWhite();
            if(_whichSide == 1) gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOnBlack();
        }

        SetPosition();
    }

    public void CheckIfCanDoMovement(int x, int z)
    {
        if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
        {
            if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 0 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
            }
            else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && _whichSide == 1 && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
            {
                gameObject.GetComponent<PieceInfo>()._canDoMoves = true;
            }
        }
        
        SetPosition();
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
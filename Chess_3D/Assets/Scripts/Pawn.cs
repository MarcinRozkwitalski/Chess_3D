using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : PieceInfo
{
    public void Movement(int _whichSide)
    {
        if(_whichSide == 0)
        {
            int z = (int)gameObject.transform.position.z; int x = (int)gameObject.transform.position.x;
            z++;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
            {
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;

                if(gameObject.transform.position.z == 1)
                {
                    z++;

                    if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                    {
                        gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;
            x--; z++;

            if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;
            x++; z++;

            if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
        else if(_whichSide == 1)
        {
            int z = (int)gameObject.transform.position.z; int x = (int)gameObject.transform.position.x;
            z--;

            if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
            {
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;

                if(gameObject.transform.position.z == gridCreator._zWidth - 2)
                {
                    z--;
                    if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                    {
                        gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
            }
            
            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;
            x--; z--;
            
            if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;

                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;
            x++; z--;

            if(-1 < z && z < gridCreator._zWidth && -1 < x && x < gridCreator._xWidth)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }   
}
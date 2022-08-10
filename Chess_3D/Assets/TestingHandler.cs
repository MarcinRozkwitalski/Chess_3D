using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingHandler : MonoBehaviour
{
    GridCreator gridCreator;
    ChessPiecesGrid chessPiecesGrid;

    void Start()
    {
        gridCreator = GameObject.Find("TileGrid").GetComponent<GridCreator>();
        chessPiecesGrid = GameObject.Find("ChessPiecesGrid").GetComponent<ChessPiecesGrid>();
    }

    public void CheckGameStatusButton()
    {
        for(int i = 0; i < gridCreator._xWidth; i++)
        {
            for (int j = 0; j < gridCreator._zWidth; j++)
            {
                Debug.Log("x = " + i + " | z = " + j + " | " + chessPiecesGrid.chessPiecesGrid[i, j]);
            }
        }
    }

    public void CheckGameStatusButton01()
    {
        for(int i = 0; i < gridCreator._xWidth; i++)
        {
            for (int j = 0; j < gridCreator._zWidth; j++)
            {
                if(gridCreator.chessBoardGrid[i, j].name == "WhiteTile(Clone)" || gridCreator.chessBoardGrid[i, j].name == "BlackTile(Clone)")
                {
                    Debug.Log("x = " + i + " | z = " + j + " | " + gridCreator.chessBoardGrid[i, j]);
                }
            }
        }
    }
}

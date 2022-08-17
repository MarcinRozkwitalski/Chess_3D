using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : PieceLogic
{
    public void Movement(int _whichSide)
    {
        if(_whichSide == 0)
        {
            int z = (int)gameObject.transform.position.z; int x = (int)gameObject.transform.position.x;

            z++; x++;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z++; x--;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z--; x--;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z--; x++;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z++;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z--;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            x++;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            x--;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("Black"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
        else if(_whichSide == 1)
        {
            int z = (int)gameObject.transform.position.z; int x = (int)gameObject.transform.position.x;

            z++; x++;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z++; x--;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z--; x--;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z--; x++;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z++;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            z--;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            x++;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            z = (int)gameObject.transform.position.z; x = (int)gameObject.transform.position.x;

            x--;

            if(-1 < z && z < 8 && -1 < x && x < 8)
            {
                if(chessPiecesGrid.chessPiecesGrid[x, z] == null)
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else if(chessPiecesGrid.chessPiecesGrid[x, z] != null && chessPiecesGrid.chessPiecesGrid[x, z].CompareTag("White"))
                {
                    gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }
}

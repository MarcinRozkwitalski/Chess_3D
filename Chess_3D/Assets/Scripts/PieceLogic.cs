using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceLogic : MonoBehaviour
{
    GridCreator gridCreator;
    GameHandler gameHandler;

    [SerializeField] private string _nameOfChessPiece;

    [SerializeField] private int _typeOfChessPiece; // 0 - pawn, 1 - knight, 2 - bishop, 3 - rook, 4 - queen, 5 - king

    [SerializeField] public int _whichSide; // 0 - white, 1 - black

    [SerializeField] public bool _isSelected = false;

    [SerializeField] public bool _flagForTileGeneration = false;

    MeshRenderer _meshRenderer;
    Color _initialColor;

    void Start()
    {
        gridCreator = GameObject.Find("Grid").GetComponent<GridCreator>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        _meshRenderer = this.GetComponent<MeshRenderer>();

        switch(gameObject.tag){
            case "White":
                _initialColor = Color.white;
                _whichSide = 0;
                break;
            case "Black":
                _initialColor = Color.black;
                _whichSide = 1;
                break;
            default:
                break;
        }

        _nameOfChessPiece = gameObject.name;
        CheckTypeOfChessPiece(_nameOfChessPiece);
    }

    void FixedUpdate() {
        if(_isSelected && !_flagForTileGeneration)
        {
            _flagForTileGeneration = true;

            if(_isSelected && _flagForTileGeneration)
            {
                switch(_typeOfChessPiece)
                {
                    case 0:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");
                        PawnMovement(_whichSide);
                        break;

                    case 1:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");

                        break;

                    case 2:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");

                        break;

                    case 3:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");

                        break;

                    case 4:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");

                        break;

                    case 5:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");

                        break;
                }
            }
        }
    }

    void CheckTypeOfChessPiece(string nameOfChessPiece)
    {
        if(nameOfChessPiece == "WhitePawn(Clone)" || nameOfChessPiece == "BlackPawn(Clone)")            _typeOfChessPiece = 0;
        else if(nameOfChessPiece == "WhiteKnight(Clone)" || nameOfChessPiece == "BlackKnight(Clone)")   _typeOfChessPiece = 1;
        else if(nameOfChessPiece == "WhiteBishop(Clone)" || nameOfChessPiece == "BlackBishop(Clone)")   _typeOfChessPiece = 2;
        else if(nameOfChessPiece == "WhiteRook(Clone)" || nameOfChessPiece == "BlackRook(Clone)")       _typeOfChessPiece = 3;
        else if(nameOfChessPiece == "WhiteQueen(Clone)" || nameOfChessPiece == "BlackQueen(Clone)")     _typeOfChessPiece = 4;
        else if(nameOfChessPiece == "WhiteKing(Clone)" || nameOfChessPiece == "BlackKing(Clone)")       _typeOfChessPiece = 5;
    }

    void PawnMovement(int _whichSide)
    {
        if(_whichSide == 0)
        {
            int z = (int)gameObject.transform.position.z;
            int x = (int)gameObject.transform.position.x;

            if(gridCreator.chessPiecesGrid[x, z + 1] == null)
            {
                gridCreator.chessBoardGrid[x, z + 1].gameObject.GetComponent<Renderer>().material.color = Color.green;

                if(gameObject.transform.position.z == 1)
                {
                    if(gridCreator.chessPiecesGrid[x, z + 2] == null)
                    {
                        gridCreator.chessBoardGrid[x, z + 2].gameObject.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
            }

            if(gridCreator.chessPiecesGrid[x - 1, z + 1] != null && gridCreator.chessPiecesGrid[x - 1, z + 1].CompareTag("Black"))
            {
                gridCreator.chessBoardGrid[x - 1, z + 1].gameObject.GetComponent<Renderer>().material.color = Color.red;
            }

            if(gridCreator.chessPiecesGrid[x + 1, z + 1] != null && gridCreator.chessPiecesGrid[x + 1, z + 1].CompareTag("Black"))
            {
                gridCreator.chessBoardGrid[x + 1, z + 1].gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else if(_whichSide == 1)
        {
            int z = (int)gameObject.transform.position.z;
            int x = (int)gameObject.transform.position.x;

            if(gridCreator.chessPiecesGrid[x, z - 1] == null)
            {
                gridCreator.chessBoardGrid[x, z - 1].gameObject.GetComponent<Renderer>().material.color = Color.green;

                if(gameObject.transform.position.z == 6)
                {
                    if(gridCreator.chessPiecesGrid[x, z - 2] == null)
                    {
                        gridCreator.chessBoardGrid[x, z - 2].gameObject.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
            }

            if(gridCreator.chessPiecesGrid[x - 1, z - 1] != null && gridCreator.chessPiecesGrid[x - 1, z - 1].CompareTag("White"))
            {
                gridCreator.chessBoardGrid[x - 1, z - 1].gameObject.GetComponent<Renderer>().material.color = Color.red;
            }

            if(gridCreator.chessPiecesGrid[x + 1, z - 1] != null && gridCreator.chessPiecesGrid[x + 1, z - 1].CompareTag("White"))
            {
                gridCreator.chessBoardGrid[x + 1, z - 1].gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    void KnightMovement()
    {
        for(int i = 0; i < 8; i++)
        {
            //1 - z++, z++, x++
            //2 - z++, z++, x--
            //3 - z--, z--, x++
            //4 - z--, z--, x--
            //5 - x++, x++, z--
            //6 - x++, x++, z++
            //7 - x--, x--, z--
            //8 - x--, x--, z++
        }
    }

    void BishopMovement()
    {
        for(int i = 0; i < 4; i++)
        {
            //detect for how long it can go diagonally without getting interrupted by any piece or by grid width
            //per tile z++ x++ or z++ x-- or z-- x++ or z-- x--
        }
    }

    void RookMovement()
    {
        for(int i = 0; i < 4; i++)
        {
            //detect for how long it can go diagonally without getting interrupted by any piece or by grid width 
            //per tile z++ or z-- or x++ or x--
        }
    }

    void QueenMovement()
    {
        for(int i = 0; i < 8; i++)
        {
            //bishop + rook
        }
    }

    void KingMovement()
    {
        for(int i = 0; i < 8; i++)
        {

        }
    }
}
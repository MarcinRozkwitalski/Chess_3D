using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceLogic : MonoBehaviour
{
    public GridCreator gridCreator;
    public ChessPiecesGrid chessPiecesGrid;

    [SerializeField] private string _nameOfChessPiece;

    [SerializeField] public int _typeOfChessPiece; // 0 - pawn, 1 - knight, 2 - bishop, 3 - rook, 4 - queen, 5 - king

    [SerializeField] public int _whichSide; // 0 - white, 1 - black

    [SerializeField] public bool _isSelected = false;

    [SerializeField] public bool _flagForTileGeneration = false;

    MeshRenderer _meshRenderer;
    Color _initialColor;

    void Start()
    {
        gridCreator = GameObject.Find("TileGrid").GetComponent<GridCreator>();
        chessPiecesGrid = GameObject.Find("ChessPiecesGrid").GetComponent<ChessPiecesGrid>();
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();

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
                        gameObject.GetComponent<Pawn>().Movement(_whichSide);
                        break;

                    case 1:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");
                        gameObject.GetComponent<Knight>().Movement(_whichSide);
                        break;

                    case 2:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");
                        gameObject.GetComponent<Bishop>().Movement(_whichSide);
                        break;

                    case 3:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");
                        gameObject.GetComponent<Rook>().Movement(_whichSide);
                        break;

                    case 4:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");
                        gameObject.GetComponent<Queen>().Movement(_whichSide);
                        break;

                    case 5:
                        Debug.Log(_nameOfChessPiece + " selected");
                        Debug.Log("Current position: [" + gameObject.transform.position.z + ", " + gameObject.transform.position.x + "].");
                        gameObject.GetComponent<King>().Movement(_whichSide);
                        break;
                }
            }
        }
    }

    void CheckTypeOfChessPiece(string nameOfChessPiece)
    {
        if(nameOfChessPiece == "WhitePawn(Clone)" || nameOfChessPiece == "BlackPawn(Clone)")
        {
            _typeOfChessPiece = 0;
            if(!gameObject.GetComponent<Pawn>()) gameObject.AddComponent<Pawn>();
        }
        else if(nameOfChessPiece == "WhiteKnight(Clone)" || nameOfChessPiece == "BlackKnight(Clone)")
        {
            _typeOfChessPiece = 1;
            if(!gameObject.GetComponent<Knight>()) gameObject.AddComponent<Knight>();
        }
        else if(nameOfChessPiece == "WhiteBishop(Clone)" || nameOfChessPiece == "BlackBishop(Clone)")
        {
            _typeOfChessPiece = 2;
            if(!gameObject.GetComponent<Bishop>()) gameObject.AddComponent<Bishop>();
        }
        else if(nameOfChessPiece == "WhiteRook(Clone)" || nameOfChessPiece == "BlackRook(Clone)")
        {
            _typeOfChessPiece = 3;
            if(!gameObject.GetComponent<Rook>()) gameObject.AddComponent<Rook>();
        }
        else if(nameOfChessPiece == "WhiteQueen(Clone)" || nameOfChessPiece == "BlackQueen(Clone)")
        {
            _typeOfChessPiece = 4;
            if(!gameObject.GetComponent<Queen>()){
                gameObject.AddComponent<Queen>();
                gameObject.AddComponent<Bishop>();
                gameObject.AddComponent<Rook>();
            }
        }
        else if(nameOfChessPiece == "WhiteKing(Clone)" || nameOfChessPiece == "BlackKing(Clone)")
        {
            _typeOfChessPiece = 5;
            if(!gameObject.GetComponent<King>()) gameObject.AddComponent<King>();
        }
    }
}
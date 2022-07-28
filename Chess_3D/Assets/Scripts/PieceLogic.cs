using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceLogic : MonoBehaviour
{
    GameHandler gameHandler;

    [SerializeField] private string _nameOfChessPiece;

    [SerializeField] private int _typeOfChessPiece; // 0 - pawn, 1 - knight, 2 - bishop, 3 - rook, 4 - queen, 5 - king

    [SerializeField] public int _whichSide; // 0 - white, 1 - black

    MeshRenderer _meshRenderer;
    Color _initialColor;

    void Start()
    {
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

    void CheckTypeOfChessPiece(string nameOfChessPiece)
    {
        if(nameOfChessPiece == "WhitePawn(Clone)" || nameOfChessPiece == "BlackPawn(Clone)")            _typeOfChessPiece = 0;
        else if(nameOfChessPiece == "WhiteKnight(Clone)" || nameOfChessPiece == "BlackKnight(Clone)")   _typeOfChessPiece = 1;
        else if(nameOfChessPiece == "WhiteBishop(Clone)" || nameOfChessPiece == "BlackBishop(Clone)")   _typeOfChessPiece = 2;
        else if(nameOfChessPiece == "WhiteRook(Clone)" || nameOfChessPiece == "BlackRook(Clone)")       _typeOfChessPiece = 3;
        else if(nameOfChessPiece == "WhiteQueen(Clone)" || nameOfChessPiece == "BlackQueen(Clone)")     _typeOfChessPiece = 4;
        else if(nameOfChessPiece == "WhiteKing(Clone)" || nameOfChessPiece == "BlackKing(Clone)")       _typeOfChessPiece = 5;
    }
}
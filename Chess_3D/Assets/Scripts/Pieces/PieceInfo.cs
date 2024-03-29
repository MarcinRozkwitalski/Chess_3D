using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceInfo : MonoBehaviour
{
    public GameHandler gameHandler;
    public GridCreator gridCreator;
    public ChessPiecesGrid chessPiecesGrid;

    [SerializeField] private string _nameOfChessPiece;

    [SerializeField] public int _typeOfChessPiece; // 0 - pawn, 1 - knight, 2 - bishop, 3 - rook, 4 - queen, 5 - king

    [SerializeField] public int _whichSide; // 0 - white, 1 - black

    [SerializeField] public bool _canDoMoves;

    [SerializeField] public bool _isCheckingEnemyKing;

    [SerializeField] public bool _isDefendingKing;

    [SerializeField] public string _attackingPieceDirection;

    [SerializeField] public bool _flagForFoundedDefendingPiece;

    [SerializeField] public bool _isSelected;

    [SerializeField] public bool _flagForTileGeneration;

    protected int x, z;
    public int new_x, new_z;

    MeshRenderer _meshRenderer;
    Color _initialColor;

    void Awake()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
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

        SetInitialPosition();
        SetPosition();
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
                        gameObject.GetComponent<Pawn>().Movement(_whichSide);
                        break;

                    case 1:
                        gameObject.GetComponent<Knight>().Movement();
                        break;

                    case 2:
                        gameObject.GetComponent<Bishop>().Movement();
                        break;

                    case 3:
                        gameObject.GetComponent<Rook>().Movement();
                        break;

                    case 4:
                        gameObject.GetComponent<Queen>().Movement();
                        break;

                    case 5:
                        gameObject.GetComponent<King>().Movement();
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

    public void HandleBeatableTiles()
    {
        if(this._typeOfChessPiece == 0)
        {
            gameObject.GetComponent<Pawn>().BeatableTiles();
        }
        else if(this._typeOfChessPiece == 1)
        {
            gameObject.GetComponent<Knight>().BeatableTiles();
        }
        else if(this._typeOfChessPiece == 2)
        {
            gameObject.GetComponent<Bishop>().BeatableTiles();
        }
        else if(this._typeOfChessPiece == 3)
        {
            gameObject.GetComponent<Rook>().BeatableTiles();
        }
        else if(this._typeOfChessPiece == 4)
        {
            gameObject.GetComponent<Queen>().BeatableTiles();
        }
        else if(this._typeOfChessPiece == 5)
        {
            gameObject.GetComponent<King>().BeatableTiles();
        }
    }

    public void HandleIfCanDoMoves()
    {
        if(this._typeOfChessPiece == 0)
        {
            gameObject.GetComponent<Pawn>().CheckIfCanDoMoves();
        }
        else if(this._typeOfChessPiece == 1)
        {
            gameObject.GetComponent<Knight>().CheckIfCanDoMoves();
        }
        else if(this._typeOfChessPiece == 2)
        {
            gameObject.GetComponent<Bishop>().CheckIfCanDoMoves();
        }
        else if(this._typeOfChessPiece == 3)
        {
            gameObject.GetComponent<Rook>().CheckIfCanDoMoves();
        }
        else if(this._typeOfChessPiece == 4)
        {
            gameObject.GetComponent<Queen>().CheckIfCanDoMoves();
        }
        else if(this._typeOfChessPiece == 5)
        {
            gameObject.GetComponent<King>().CheckIfCanDoMoves();
        }
    }

    public void SetTileGreen(int x, int z)
    {
        gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void SetTileRed(int x, int z)
    {
        gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
        chessPiecesGrid.chessPiecesGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void SetTileRedForEnPassant(int x, int z)
    {
        gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void SetInitialPosition()
    {
        x = (int)gameObject.transform.position.x;
        z = (int)gameObject.transform.position.z;

        new_x = x;
        new_z = z;
    }

    public void SetPosition()
    {
        x = new_x;
        z = new_z;
    }
}
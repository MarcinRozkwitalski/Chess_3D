using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    GridCreator gridCreator;
    ChessPiecesGrid chessPiecesGrid;
    AdjustPlaneToGrid adjustPlaneToGrid;
    MoveCameraAroundObject moveCameraAroundObject;

    public bool _gameHasBeenStarted = false;
    public bool _startingCamera = true;
    public bool _letPlayersChoosePieces = false;
    public bool _whiteIsChecked = false;
    public bool _blackIsChecked = false;

    public int _amountOfWhitePiecesCheckingBlackKing = 0;
    public int _amountOfBlackPiecesCheckingWhiteKing = 0;

    public int _possibleWhitePiecesMoves;
    public int _possibleBlackPiecesMoves;

    public List<GameObject> _whitePiecesCheckingBlackKing = new List<GameObject>();
    public List<GameObject> _blackPiecesCheckingWhiteKing = new List<GameObject>();

    public List<string> _blockableTilesByWhite = new List<string>();
    public List<string> _blockableTilesByBlack = new List<string>();

    public GameObject _lastDestroyedGameObject;

    [SerializeField] public int _whosTurnNow = 0; //0 - white, 1 - black

    Transform _selection;
    public GameObject _currentGOSelection = null;
    public PieceInfo _currentGOSelectionPieceInfo = null;
    public int _lastWhichSide;

    Text GameInfoText;

    private void Start() 
    {
        gridCreator = GameObject.Find("TileGrid").GetComponent<GridCreator>();
        chessPiecesGrid = GameObject.Find("ChessPiecesGrid").GetComponent<ChessPiecesGrid>();
        adjustPlaneToGrid = GameObject.Find("Plane").GetComponent<AdjustPlaneToGrid>();
        GameInfoText = GameObject.Find("GameInfoText").GetComponent<Text>();
        moveCameraAroundObject = GameObject.Find("Main Camera").GetComponent<MoveCameraAroundObject>();

        adjustPlaneToGrid.StartAnimation();
    }

    private void Update() 
    {
        if(_gameHasBeenStarted && _letPlayersChoosePieces)
        {
            if(_currentGOSelection) _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;

            if(_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                var pieceLogicScript = _selection.GetComponent<PieceInfo>();

                if(pieceLogicScript != null && pieceLogicScript._whichSide == _whosTurnNow)
                {
                    if(pieceLogicScript._whichSide == 0) selectionRenderer.material.color = Color.white;
                    else if(pieceLogicScript._whichSide == 1) selectionRenderer.material.color = Color.black;
                    else if(pieceLogicScript._isSelected) selectionRenderer.material.color = Color.green;
                    else _selection = null;
                }
                _selection = null;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;

                if(selection) HandlePlayer(selection);
                else if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
            }
            else if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection(); //RayCast hits nothing (false)
        }
    }

    void HandlePlayer(Transform selection)
    {
        if(_currentGOSelection != null && selection.gameObject != _currentGOSelection)
        {
            if(selection.CompareTag("White") || selection.CompareTag("Black"))
            {
                if(selection.GetComponent<Renderer>().material.color == Color.red)
                {
                    if(Input.GetMouseButtonDown(0)) HandlePieceMovement(selection);
                }
                else if(_whosTurnNow == 0 && selection.GetComponent<Renderer>().material.color == Color.black)
                {
                    if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
                }
                else if(_whosTurnNow == 1 && selection.GetComponent<Renderer>().material.color == Color.white)
                {
                    if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
                }
                else if(selection.gameObject.GetComponent<PieceInfo>()._canDoMoves)
                {
                    HandleYellowHover(selection);
                } 
            }
            else if(selection.CompareTag("WhiteTile") || selection.CompareTag("BlackTile"))
            {
                if(selection.GetComponent<Renderer>().material.color == Color.green)
                {
                    if(Input.GetMouseButtonDown(0)) HandlePieceMovement(selection);
                }
                else if(selection.GetComponent<Renderer>().material.color == Color.red)
                {
                    if(Input.GetMouseButtonDown(0)) HandlePieceMovement(selection);
                }
                else if(selection.GetComponent<Renderer>().material.color == Color.white || selection.GetComponent<Renderer>().material.color == Color.black)
                {
                    if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
                }
            }
            else if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection(); 
        }
        else if(selection.name != "BlackTile(Clone)" && selection.name != "WhiteTile(Clone)" && selection.name != "Plane" && selection.gameObject != _currentGOSelection && selection.gameObject.GetComponent<PieceInfo>()._canDoMoves)
        {
            HandleYellowHover(selection);
        }
        else if(selection.gameObject == _currentGOSelection) //wcisniecie wczesniej wybranej bierki
        {
            if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
        }
    }

    public void CurrentGOSelection(Transform selection)
    {
        if(_currentGOSelection == null)
        {
            _currentGOSelection = selection.gameObject;
            _currentGOSelectionPieceInfo = _currentGOSelection.GetComponent<PieceInfo>();
            _currentGOSelectionPieceInfo._isSelected = true;
            _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;
        }
        else if(_currentGOSelection != null)
        {
            CleanTiles();
            CleanChessPieces();

            if(_currentGOSelectionPieceInfo._whichSide == 0)
            {
                _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;
                _lastWhichSide = 0;
            }
            else if(_currentGOSelectionPieceInfo._whichSide == 1)
            {
                _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;
                _lastWhichSide = 1;
            }
            
            _currentGOSelectionPieceInfo._isSelected = false;
            _currentGOSelectionPieceInfo._flagForTileGeneration = false;

            _currentGOSelection = selection.gameObject;
            _currentGOSelectionPieceInfo = _currentGOSelection.GetComponent<PieceInfo>();
            _currentGOSelectionPieceInfo._isSelected = true;
            _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public void ResetCurrentGOSelection()
    {
        if(_currentGOSelection && _currentGOSelectionPieceInfo)
        {
            if(_currentGOSelectionPieceInfo._whichSide == 0)       _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;
            else if(_currentGOSelectionPieceInfo._whichSide == 1)  _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;

            _currentGOSelectionPieceInfo._isSelected = false;
            _currentGOSelectionPieceInfo._flagForTileGeneration = false;

            _currentGOSelection = null;
            _currentGOSelectionPieceInfo = null;

            CleanTiles();
            CleanChessPieces();
        }
    }

    public void HandleYellowHover(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        var pieceInfoScript = selection.GetComponent<PieceInfo>();

        if(pieceInfoScript != null && selectionRenderer != null)
        {
            if(_whosTurnNow == 0 && pieceInfoScript._whichSide == 0)
            {
                selectionRenderer.material.color = Color.yellow;
                if(Input.GetMouseButtonDown(0)) CurrentGOSelection(selection);
            }

            if(_whosTurnNow == 1 && pieceInfoScript._whichSide == 1)
            {
                selectionRenderer.material.color = Color.yellow;
                if(Input.GetMouseButtonDown(0)) CurrentGOSelection(selection);
            }
        }

        _selection = selection;
    }

    public void HandlePieceMovement(Transform selection)
    {
        StopCoroutine("MovePieceAnimation");

        int new_x = 0;
        int new_z = 0;

        GameObject tempCurrentGOSelection = _currentGOSelection;
        PieceInfo tempCurrentGoSelectionPieceInfo = _currentGOSelectionPieceInfo;
        int z = (int)_currentGOSelection.transform.position.z;
        int x = (int)_currentGOSelection.transform.position.x;
        _currentGOSelection = null;
        _currentGOSelectionPieceInfo = null;

        if(selection.GetComponent<PieceInfo>())
        {
            new_x = selection.GetComponent<PieceInfo>().new_x;
            new_z = selection.GetComponent<PieceInfo>().new_z;
        }
        else if(selection.GetComponent<TileInfo>())
        {
            new_z = (int)selection.transform.position.z;
            new_x = (int)selection.transform.position.x;
        }
        
        if(selection.GetComponent<Renderer>().material.color == Color.red && chessPiecesGrid.chessPiecesGrid[new_x, new_z] != null)
        {
            _lastDestroyedGameObject = chessPiecesGrid.chessPiecesGrid[new_x, new_z];
            Destroy(chessPiecesGrid.chessPiecesGrid[new_x, new_z]);
        }

        if(tempCurrentGOSelection.GetComponent<Pawn>())
        {
            int temp_new_x = new_x;
            int temp_new_z = new_z;

            if(tempCurrentGOSelection.GetComponent<PieceInfo>()._whichSide == 0 && new_z - z == 2)
            {
                temp_new_x--;
                if(-1 < temp_new_x && temp_new_x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
                {
                    if(chessPiecesGrid.chessPiecesGrid[new_x - 1, new_z] != null)
                    {
                        if(chessPiecesGrid.chessPiecesGrid[new_x - 1, new_z].gameObject.GetComponent<Pawn>() && 
                        chessPiecesGrid.chessPiecesGrid[new_x - 1, new_z].gameObject.GetComponent<PieceInfo>()._whichSide == 1)
                        {
                            tempCurrentGOSelection.GetComponent<Pawn>()._canBeEnPassanted = true;
                            chessPiecesGrid.chessPiecesGrid[new_x - 1, new_z].gameObject.GetComponent<Pawn>()._canEnPassant = true;
                        }
                    }
                }
                
                temp_new_x = new_x;
                temp_new_x++;
                if(-1 < temp_new_x && temp_new_x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
                {
                    if(chessPiecesGrid.chessPiecesGrid[new_x + 1, new_z] != null)
                    {
                        if (chessPiecesGrid.chessPiecesGrid[new_x + 1, new_z].gameObject.GetComponent<Pawn>() && 
                        chessPiecesGrid.chessPiecesGrid[new_x + 1, new_z].gameObject.GetComponent<PieceInfo>()._whichSide == 1)
                        {
                            tempCurrentGOSelection.GetComponent<Pawn>()._canBeEnPassanted = true;
                            chessPiecesGrid.chessPiecesGrid[new_x + 1, new_z].gameObject.GetComponent<Pawn>()._canEnPassant = true;
                        }
                    }
                }
            }
            else if(tempCurrentGOSelection.GetComponent<PieceInfo>()._whichSide == 1 && z - new_z == 2)
            {
                temp_new_x--;
                if(-1 < temp_new_x && temp_new_x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
                {
                    if(chessPiecesGrid.chessPiecesGrid[new_x - 1, new_z] != null)
                    {
                        if(chessPiecesGrid.chessPiecesGrid[new_x - 1, new_z].gameObject.GetComponent<Pawn>() && 
                        chessPiecesGrid.chessPiecesGrid[new_x - 1, new_z].gameObject.GetComponent<PieceInfo>()._whichSide == 0)
                        {
                            tempCurrentGOSelection.GetComponent<Pawn>()._canBeEnPassanted = true;
                            chessPiecesGrid.chessPiecesGrid[new_x - 1, new_z].gameObject.GetComponent<Pawn>()._canEnPassant = true;
                        }
                    }
                }
                
                temp_new_x = new_x;
                temp_new_x++;
                if(-1 < temp_new_x && temp_new_x < gridCreator._xWidth && -1 < z && z < gridCreator._zWidth)
                {
                    if(chessPiecesGrid.chessPiecesGrid[new_x + 1, new_z] != null)
                    {
                        if(chessPiecesGrid.chessPiecesGrid[new_x + 1, new_z].gameObject.GetComponent<Pawn>() && 
                        chessPiecesGrid.chessPiecesGrid[new_x + 1, new_z].gameObject.GetComponent<PieceInfo>()._whichSide == 0)
                        {
                            tempCurrentGOSelection.GetComponent<Pawn>()._canBeEnPassanted = true;
                            chessPiecesGrid.chessPiecesGrid[new_x + 1, new_z].gameObject.GetComponent<Pawn>()._canEnPassant = true;
                        }
                    }
                }
            }
        }

        if(tempCurrentGOSelection.GetComponent<Pawn>() && 
        selection.GetComponent<Renderer>().material.color == Color.red && 
        chessPiecesGrid.chessPiecesGrid[new_x, new_z] == null)
        {
            if(tempCurrentGOSelection.GetComponent<PieceInfo>()._whichSide == 0)
            {
                _lastDestroyedGameObject = chessPiecesGrid.chessPiecesGrid[new_x, new_z];
                Destroy(chessPiecesGrid.chessPiecesGrid[new_x, new_z - 1]);
            }
            else if(tempCurrentGOSelection.GetComponent<PieceInfo>()._whichSide == 1)
            {
                _lastDestroyedGameObject = chessPiecesGrid.chessPiecesGrid[new_x, new_z];
                Destroy(chessPiecesGrid.chessPiecesGrid[new_x, new_z + 1]);
            }
        }

        new_z = (int)selection.transform.position.z;
        new_x = (int)selection.transform.position.x;

        chessPiecesGrid.chessPiecesGrid[x, z] = null;
        chessPiecesGrid.chessPiecesGrid[new_x, new_z] = tempCurrentGOSelection;
        
        tempCurrentGOSelection.GetComponent<PieceInfo>().new_x = new_x;
        tempCurrentGOSelection.GetComponent<PieceInfo>().new_z = new_z;
        StartCoroutine(MovePieceAnimation(tempCurrentGOSelection, tempCurrentGOSelection.transform.position + new Vector3(new_x - x, 0f, new_z - z), 0.5f));
        // tempCurrentGOSelection.transform.position = tempCurrentGOSelection.transform.position + new Vector3(new_x - x, 0f, new_z - z);

        new_z = (int)tempCurrentGOSelection.transform.position.z;
        new_x = (int)tempCurrentGOSelection.transform.position.x;

        if(tempCurrentGOSelection.CompareTag("White")){
            if(tempCurrentGOSelection.GetComponent<Pawn>() && new_z == gridCreator._zWidth - 1)
            {
                _lastDestroyedGameObject = tempCurrentGOSelection;
                Destroy(tempCurrentGOSelection);
                chessPiecesGrid.chessPiecesGrid[new_x, new_z] = Instantiate(chessPiecesGrid.WhiteQueenPrefab, new Vector3(new_x * gridCreator._gridSpaceSize, chessPiecesGrid._chessPieceYpos, new_z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid.chessPiecesGrid[new_x, new_z].transform.parent = chessPiecesGrid.transform;
            }
            _lastWhichSide = 0;
            _whosTurnNow = 1;
        }
        else if(tempCurrentGOSelection.CompareTag("Black"))
        {
            if(tempCurrentGOSelection.GetComponent<Pawn>() && new_z == 0)
            {
                _lastDestroyedGameObject = tempCurrentGOSelection;
                Destroy(tempCurrentGOSelection);
                chessPiecesGrid.chessPiecesGrid[new_x, new_z] = Instantiate(chessPiecesGrid.BlackQueenPrefab, new Vector3(new_x * gridCreator._gridSpaceSize, chessPiecesGrid._chessPieceYpos, new_z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid.chessPiecesGrid[new_x, new_z].transform.parent = chessPiecesGrid.transform;
            }
            _lastWhichSide = 1;
            _whosTurnNow = 0;
        }

        if(tempCurrentGOSelection.GetComponent<Pawn>())
        {
            if(tempCurrentGOSelection.GetComponent<Pawn>()._canEnPassant == true)
            {
                tempCurrentGOSelection.GetComponent<Pawn>()._canEnPassant = false;
            }
        }

        if(tempCurrentGOSelection.GetComponent<King>())
        {
            int xToAdd = tempCurrentGOSelection.GetComponent<King>().initX;
            int zToAdd = tempCurrentGOSelection.GetComponent<King>().initZ;

            if(tempCurrentGOSelection.GetComponent<King>()._hasMoved == false &&
            tempCurrentGOSelection.GetComponent<PieceInfo>()._whichSide == 0)
            {
                if(new_z == tempCurrentGOSelection.GetComponent<King>().initZ && new_x == tempCurrentGOSelection.GetComponent<King>().initX - 2)
                {
                    chessPiecesGrid.chessPiecesGrid[xToAdd - 1, zToAdd] = chessPiecesGrid.chessPiecesGrid[0, 0];
                    chessPiecesGrid.chessPiecesGrid[0, 0] = null;
                    chessPiecesGrid.chessPiecesGrid[xToAdd - 1, zToAdd].gameObject.transform.position = chessPiecesGrid.chessPiecesGrid[xToAdd - 1, zToAdd].gameObject.transform.position + new Vector3(3, 0f, 0);
                    chessPiecesGrid.chessPiecesGrid[xToAdd - 1, zToAdd].gameObject.GetComponent<Rook>()._hasMoved = true;
                }
                else if(new_z == tempCurrentGOSelection.GetComponent<King>().initZ && new_x == tempCurrentGOSelection.GetComponent<King>().initX + 2)
                {
                    chessPiecesGrid.chessPiecesGrid[xToAdd + 1, zToAdd] = chessPiecesGrid.chessPiecesGrid[7, 0];
                    chessPiecesGrid.chessPiecesGrid[7, 0] = null;
                    chessPiecesGrid.chessPiecesGrid[xToAdd + 1, zToAdd].gameObject.transform.position = chessPiecesGrid.chessPiecesGrid[xToAdd + 1, zToAdd].gameObject.transform.position + new Vector3(-2, 0f, 0);
                    chessPiecesGrid.chessPiecesGrid[xToAdd + 1, zToAdd].gameObject.GetComponent<Rook>()._hasMoved = true;
                }
            }
            else if(tempCurrentGOSelection.GetComponent<King>()._hasMoved == false &&
            tempCurrentGOSelection.GetComponent<PieceInfo>()._whichSide == 1)
            {
                if(new_z == tempCurrentGOSelection.GetComponent<King>().initZ && new_x == tempCurrentGOSelection.GetComponent<King>().initX - 2)
                {
                    chessPiecesGrid.chessPiecesGrid[xToAdd - 1, zToAdd] = chessPiecesGrid.chessPiecesGrid[0, 7];
                    chessPiecesGrid.chessPiecesGrid[0, 7] = null;
                    chessPiecesGrid.chessPiecesGrid[xToAdd - 1, zToAdd].gameObject.transform.position = chessPiecesGrid.chessPiecesGrid[xToAdd - 1, zToAdd].gameObject.transform.position + new Vector3(3, 0f, 0);
                    chessPiecesGrid.chessPiecesGrid[xToAdd - 1, zToAdd].gameObject.GetComponent<Rook>()._hasMoved = true;
                }
                else if(new_z == tempCurrentGOSelection.GetComponent<King>().initZ && new_x == tempCurrentGOSelection.GetComponent<King>().initX + 2)
                {
                    chessPiecesGrid.chessPiecesGrid[xToAdd + 1, zToAdd] = chessPiecesGrid.chessPiecesGrid[7, 7];
                    chessPiecesGrid.chessPiecesGrid[7, 7] = null;
                    chessPiecesGrid.chessPiecesGrid[xToAdd + 1, zToAdd].gameObject.transform.position = chessPiecesGrid.chessPiecesGrid[xToAdd + 1, zToAdd].gameObject.transform.position + new Vector3(-2, 0f, 0);
                    chessPiecesGrid.chessPiecesGrid[xToAdd + 1, zToAdd].gameObject.GetComponent<Rook>()._hasMoved = true;
                }
            }
        } 

        if(!tempCurrentGOSelection.GetComponent<Queen>() && 
        !tempCurrentGOSelection.GetComponent<Pawn>() && 
        !tempCurrentGOSelection.GetComponent<Knight>() &&
        !tempCurrentGOSelection.GetComponent<Bishop>())
        {
            if(tempCurrentGOSelection.GetComponent<King>())
            {
                tempCurrentGOSelection.GetComponent<King>()._hasMoved = true;
            } 
            else if(tempCurrentGOSelection.GetComponent<Rook>())
            {
                tempCurrentGOSelection.GetComponent<Rook>()._hasMoved = true;
            }
        }


        
        tempCurrentGoSelectionPieceInfo._isSelected = false;
        tempCurrentGoSelectionPieceInfo._flagForTileGeneration = false;

        tempCurrentGOSelection = null;

        CleanTiles();
        CleanChessPieces();
        ResetBeatableTiles();
        CheckBeatableTiles();
        CheckIfPawnsForThisSideTurnHaveEnPassant();
        CheckIfAnyPiecesAreDefendingTheirKings();
        CheckIfAnyPieceIsCheckingEnemyKing();
        CheckAmountOfPiecesCheckingEnemyKing();
        CheckIfAnyKingIsChecked();
        CheckMovesForPieces();
        CheckForWinConditions();
    }

    public void CleanChessPieces()
    {
        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            if(currentPiece.CompareTag("White"))        currentPiece.GetComponent<Renderer>().material.color = Color.white; 
            else if(currentPiece.CompareTag("Black")) currentPiece.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    public void CleanTiles()
    {
        int count = 0;
        
        for(int x = 0; x < gridCreator._xWidth; x++)
        {
            for(int z = 0; z < gridCreator._zWidth; z++)
            {
                if(gridCreator.chessBoardGrid[x, z].name == "WhiteTile(Clone)" || gridCreator.chessBoardGrid[x, z].name == "BlackTile(Clone)")
                {
                    if(count % 2 == 0)  gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.white;
                    else                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.black;
                }
                count++;
            }
            count++;
        }
    }

    public void ResetBeatableTiles()
    {
        int count = 0;
        for(int x = 0; x < gridCreator._xWidth; x++)
        {
            for(int z = 0; z < gridCreator._zWidth; z++)
            {
                count++;
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOffWhite();
                gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<TileInfo>().SetOffBlack();
            }
        }
    }

    public void CheckBeatableTiles()
    {
        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            if(currentPiece != _lastDestroyedGameObject)
            currentPiece.GetComponent<PieceInfo>().HandleBeatableTiles();
        }
    }

    public void CheckIfPawnsForThisSideTurnHaveEnPassant()
    {
        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        if(_whosTurnNow == 0)
        {
            for(int i = 0; i < howManyPieces; i++)
            {
                GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
                if(currentPiece.CompareTag("White") && currentPiece.GetComponent<Pawn>())
                {
                    currentPiece.GetComponent<Pawn>()._canBeEnPassanted = false;
                }
            }
        }
        else if(_whosTurnNow == 1)
        {
            for(int i = 0; i < howManyPieces; i++)
            {
                GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
                if(currentPiece.CompareTag("Black") && currentPiece.GetComponent<Pawn>())
                {
                    currentPiece.GetComponent<Pawn>()._canBeEnPassanted = false;
                }
            }
        }
    }

    public void CheckMovesForPieces()
    {
        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        _possibleWhitePiecesMoves = 0;
        _possibleBlackPiecesMoves = 0;

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            currentPiece.GetComponent<PieceInfo>().HandleIfCanDoMoves();
        }
    }

    public void CheckIfAnyPiecesAreDefendingTheirKings()
    {
        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            currentPiece.GetComponent<PieceInfo>()._isDefendingKing = false;
            currentPiece.GetComponent<PieceInfo>()._attackingPieceDirection = "";
        }

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            if(currentPiece.GetComponent<Queen>() || currentPiece.GetComponent<Bishop>() || currentPiece.GetComponent<Rook>())
            {
                if(currentPiece.GetComponent<Queen>() && currentPiece.GetComponent<Bishop>() && currentPiece.GetComponent<Rook>())
                {
                    currentPiece.GetComponent<Queen>().IterateForDefendingPieces();
                    currentPiece.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece = false;
                }
                else if(currentPiece.GetComponent<Bishop>() && !currentPiece.GetComponent<Queen>())
                {
                    currentPiece.GetComponent<Bishop>().IterateForDefendingPieces();
                    currentPiece.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece = false;
                }
                else if(currentPiece.GetComponent<Rook>() && !currentPiece.GetComponent<Queen>())
                {
                    currentPiece.GetComponent<Rook>().IterateForDefendingPieces();
                    currentPiece.GetComponent<PieceInfo>()._flagForFoundedDefendingPiece = false;
                }
            }
        }
    }

    public void CheckIfAnyPieceIsCheckingEnemyKing()
    {
        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            if(!currentPiece.GetComponent<King>())
            {
                if(currentPiece.GetComponent<Pawn>())
                {
                    currentPiece.GetComponent<Pawn>().CheckForCheckIfIsCheckingEnemyKing();
                }
                else if(currentPiece.GetComponent<Knight>())
                {
                    currentPiece.GetComponent<Knight>().CheckForCheckIfIsCheckingEnemyKing();
                }
                else if(currentPiece.GetComponent<Bishop>() && !currentPiece.GetComponent<Queen>())
                {
                    currentPiece.GetComponent<Bishop>().CheckForCheckIfIsCheckingEnemyKing();
                }
                else if(currentPiece.GetComponent<Rook>() && !currentPiece.GetComponent<Queen>())
                {
                    currentPiece.GetComponent<Rook>().CheckForCheckIfIsCheckingEnemyKing();
                }
                else if(currentPiece.GetComponent<Queen>() && currentPiece.GetComponent<Bishop>() && currentPiece.GetComponent<Rook>())
                {
                    currentPiece.GetComponent<Queen>().CheckForCheckIfIsCheckingEnemyKing();
                }
            }
        }
    }

    public void CheckAmountOfPiecesCheckingEnemyKing()
    {
        _amountOfWhitePiecesCheckingBlackKing = 0;
        _amountOfBlackPiecesCheckingWhiteKing = 0;

        _whitePiecesCheckingBlackKing.Clear();
        _blackPiecesCheckingWhiteKing.Clear();

        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            if(currentPiece.GetComponent<PieceInfo>()._isCheckingEnemyKing && currentPiece.CompareTag("White"))
            {
                _amountOfWhitePiecesCheckingBlackKing++;
                _whitePiecesCheckingBlackKing.Add(currentPiece);
            }
            else if(currentPiece.GetComponent<PieceInfo>()._isCheckingEnemyKing && currentPiece.CompareTag("Black"))
            {
                _amountOfBlackPiecesCheckingWhiteKing++;
                _blackPiecesCheckingWhiteKing.Add(currentPiece);
            }
        }

        if(_whitePiecesCheckingBlackKing.Count != 0 || _blackPiecesCheckingWhiteKing.Count != 0)
        {
            if(_amountOfWhitePiecesCheckingBlackKing == 1)
            {
                if(_blockableTilesByBlack.Count != 0)
                _blockableTilesByBlack.RemoveAt(_blockableTilesByBlack.Count - 1);

                for(int i = 0; i < _blockableTilesByBlack.Count; i++)
                {
                    int newX, newZ;
                    newX = int.Parse(_blockableTilesByBlack[i].Substring(0, 1));
                    newZ = int.Parse(_blockableTilesByBlack[i].Substring(1, 1));

                    gridCreator.chessBoardGrid[newX, newZ].GetComponent<TileInfo>()._canBeBlockedByBlack = true;
                }
            }
            
            if(_amountOfBlackPiecesCheckingWhiteKing == 1)
            {
                if(_blockableTilesByWhite.Count != 0)
                _blockableTilesByWhite.RemoveAt(_blockableTilesByWhite.Count - 1);

                for(int i = 0; i < _blockableTilesByWhite.Count; i++)
                {
                    int newX, newZ;
                    newX = int.Parse(_blockableTilesByWhite[i].Substring(0, 1));
                    newZ = int.Parse(_blockableTilesByWhite[i].Substring(1, 1));

                    gridCreator.chessBoardGrid[newX, newZ].GetComponent<TileInfo>()._canBeBlockedByWhite = true;
                }
            }
        }
        else
        {
            _blockableTilesByBlack.Clear();
            _blockableTilesByWhite.Clear();

            int howManyTiles = gridCreator.gameObject.transform.childCount;

            for(int i = 0; i < howManyTiles; i++)
            {
                GameObject currentTile = gridCreator.gameObject.transform.GetChild(i).gameObject;
                currentTile.GetComponent<TileInfo>()._canBeBlockedByWhite = false;
                currentTile.GetComponent<TileInfo>()._canBeBlockedByBlack = false;
            }
        }
    }

    public void CheckForWinConditions()
    {
        if(_possibleWhitePiecesMoves == 0 && GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == true)
        {
            Debug.Log("Black wins!");
            _gameHasBeenStarted = false;
            Vector3 targetObjectNextPosition = GameObject.Find("BlackKing(Clone)").transform.position;
            moveCameraAroundObject.targetObjectNextPosition = targetObjectNextPosition;
            moveCameraAroundObject._moveTarget = true;
            
            GameObject FireworksObject = GameObject.Find("FireworksObject");
            FireworksObject.transform.SetParent(GameObject.Find("BlackKing(Clone)").transform);
            FireworksObject.transform.localPosition = new Vector3(0, 0, 0);
            FireworksObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(_possibleBlackPiecesMoves == 0 && GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == true)
        {
            Debug.Log("White wins!");
            _gameHasBeenStarted = false;
            Vector3 targetObjectNextPosition = GameObject.Find("WhiteKing(Clone)").transform.position;
            moveCameraAroundObject.targetObjectNextPosition = targetObjectNextPosition;
            moveCameraAroundObject._moveTarget = true;

            GameObject FireworksObject = GameObject.Find("FireworksObject");
            FireworksObject.transform.SetParent(GameObject.Find("WhiteKing(Clone)").transform);
            FireworksObject.transform.localPosition = new Vector3(0, 0, 0);
            FireworksObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(_possibleWhitePiecesMoves == 0 && GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == false)
        {
            Debug.Log("Stalemate!");
            _gameHasBeenStarted = false;
        }
        else if(_possibleBlackPiecesMoves == 0 && GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == false)
        {
            Debug.Log("Stalemate!");
            _gameHasBeenStarted = false;
        }
    }

    public void CheckIfAnyKingIsChecked()
    {
        GameObject whiteKing = GameObject.Find("WhiteKing(Clone)");
        GameObject blackKing = GameObject.Find("BlackKing(Clone)");

        whiteKing.GetComponent<King>().CheckIfChecked();
        blackKing.GetComponent<King>().CheckIfChecked();

        if(whiteKing.GetComponent<King>()._isChecked == true)
        {
            _whiteIsChecked = true;
        }
        else if(whiteKing.GetComponent<King>()._isChecked == false)
        {
            _whiteIsChecked = false;
        }

        if(blackKing.GetComponent<King>()._isChecked == true)
        {
            _blackIsChecked = true;
        }
        else if(blackKing.GetComponent<King>()._isChecked == false)
        {
            _blackIsChecked = false;
        }

        if(_whiteIsChecked || _blackIsChecked)
        {
            
        }
    }

    IEnumerator MovePieceAnimation(GameObject pieceToMove, Vector3 newPosition, float duration)
    {
        if(pieceToMove != null)
        {
            float time = 0;
            Vector3 startingPosition = pieceToMove.transform.position;

            while(time < duration)
            {
                if(pieceToMove != null)
                {
                    pieceToMove.transform.position = Vector3.Lerp(startingPosition, newPosition, time / duration);
                    time += Time.deltaTime;
                    yield return null;
                }
                else
                break;
            }

            if(pieceToMove != null)
            pieceToMove.transform.position = newPosition;

            StopCoroutine("MovePieceAnimation");
        }
    }
}
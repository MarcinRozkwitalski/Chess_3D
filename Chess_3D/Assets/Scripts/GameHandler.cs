using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    GridCreator gridCreator;
    ChessPiecesGrid chessPiecesGrid;
    AdjustPlaneToGrid adjustPlaneToGrid;

    public bool _gameHasBeenStarted = false;
    public bool _letPlayersChoosePieces = false;
    public bool _whiteIsChecked = false;
    public bool _blackIsChecked = false;

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
            Debug.Log("Deselected: " + _currentGOSelection.name);
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
        GameObject tempCurrentGOSelection = _currentGOSelection;
        PieceInfo tempCurrentGoSelectionPieceInfo = _currentGOSelectionPieceInfo;
        int z = (int)_currentGOSelection.transform.position.z;
        int x = (int)_currentGOSelection.transform.position.x;
        _currentGOSelection = null;
        _currentGOSelectionPieceInfo = null;

        int new_z = (int)selection.transform.position.z;
        int new_x = (int)selection.transform.position.x;

        if(selection.GetComponent<Renderer>().material.color == Color.red) Destroy(chessPiecesGrid.chessPiecesGrid[new_x, new_z]);

        chessPiecesGrid.chessPiecesGrid[x, z] = null;
        chessPiecesGrid.chessPiecesGrid[new_x, new_z] = tempCurrentGOSelection;
        tempCurrentGOSelection.transform.position = tempCurrentGOSelection.transform.position + new Vector3(new_x - x, 0f, new_z - z);

        new_z = (int)tempCurrentGOSelection.transform.position.z;
        new_x = (int)tempCurrentGOSelection.transform.position.x;

        if(tempCurrentGOSelection.CompareTag("White")){
            if(tempCurrentGOSelection.GetComponent<Pawn>() && new_z == gridCreator._zWidth - 1)
            {
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
                Destroy(tempCurrentGOSelection);
                chessPiecesGrid.chessPiecesGrid[new_x, new_z] = Instantiate(chessPiecesGrid.BlackQueenPrefab, new Vector3(new_x * gridCreator._gridSpaceSize, chessPiecesGrid._chessPieceYpos, new_z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid.chessPiecesGrid[new_x, new_z].transform.parent = chessPiecesGrid.transform;
            }
            _lastWhichSide = 1;
            _whosTurnNow = 0;
        }
        
        tempCurrentGoSelectionPieceInfo._isSelected = false;
        tempCurrentGoSelectionPieceInfo._flagForTileGeneration = false;

        tempCurrentGOSelection = null;

        CleanTiles();
        CleanChessPieces();
        ResetBeatableTiles();
        CheckBeatableTiles();
        CheckMovesForPieces();
        CheckIfAnyKingIsChecked();
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
        
        for (int x = 0; x < gridCreator._xWidth; x++)
        {
            for (int z = 0; z < gridCreator._zWidth; z++)
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
        for (int x = 0; x < gridCreator._xWidth; x++)
        {
            for (int z = 0; z < gridCreator._zWidth; z++)
            {
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
            currentPiece.GetComponent<PieceInfo>().HandleBeatableTiles();
        }
    }

    public void CheckMovesForPieces()
    {
        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            currentPiece.GetComponent<PieceInfo>().HandleIfCanDoMoves();
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
        else if(GameObject.Find("WhiteKing(Clone)").GetComponent<King>()._isChecked == false)
        {
            _whiteIsChecked = false;
        }

        if(GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == true)
        {
            _blackIsChecked = true;
        }
        else if(GameObject.Find("BlackKing(Clone)").GetComponent<King>()._isChecked == false)
        {
            _blackIsChecked = false;
        }
    }
}
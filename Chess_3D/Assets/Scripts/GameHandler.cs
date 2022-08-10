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

    [SerializeField] public int _whosTurnNow = 0; //0 - white, 1 - black

    Transform _selection;
    public GameObject _currentGOSelection = null;
    public PieceLogic _currentGOSelectionPieceLogic = null;
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
                var pieceLogicScript = _selection.GetComponent<PieceLogic>();

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

                if(selection)
                {
                    HandlePlayer(selection);
                }
                else
                if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
            }
            else //RayCast hits nothing (false)
            if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
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
                    if(Input.GetMouseButtonDown(0))
                    {
                        HandlePieceMovement(selection);
                    }
                }
                else 
                {
                    HandleYellowHover(selection);
                }
            }
            else if(selection.CompareTag("WhiteTile") || selection.CompareTag("BlackTile"))
            {
                if(selection.GetComponent<Renderer>().material.color == Color.green)
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        HandlePieceMovement(selection);
                    }
                }
                if(selection.GetComponent<Renderer>().material.color == Color.red)
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        HandlePieceMovement(selection);
                    }
                }
            }
            else if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection(); 
        }
        else if(selection.name != "BlackTile(Clone)" && selection.name != "WhiteTile(Clone)" && selection.name != "Plane" && selection.gameObject != _currentGOSelection)
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
            _currentGOSelectionPieceLogic = _currentGOSelection.GetComponent<PieceLogic>();
            _currentGOSelectionPieceLogic._isSelected = true;
            _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;
        }
        else if(_currentGOSelection != null)
        {
            Debug.Log("Deselected: " + _currentGOSelection.name);
            CleanTiles();

            if(_currentGOSelectionPieceLogic._whichSide == 0)
            {
                _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;
                _lastWhichSide = 0;
            }
            else if(_currentGOSelectionPieceLogic._whichSide == 1)
            {
                _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;
                _lastWhichSide = 1;
            }
            
            _currentGOSelectionPieceLogic._isSelected = false;
            _currentGOSelectionPieceLogic._flagForTileGeneration = false;

            _currentGOSelection = selection.gameObject;
            _currentGOSelectionPieceLogic = _currentGOSelection.GetComponent<PieceLogic>();
            _currentGOSelectionPieceLogic._isSelected = true;
            _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public void ResetCurrentGOSelection()
    {
        if(_currentGOSelection && _currentGOSelectionPieceLogic)
        {
            if(_currentGOSelectionPieceLogic._whichSide == 0)
            {
                _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;
            }
            else if(_currentGOSelectionPieceLogic._whichSide == 1)
            {
                _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;
            }

            _currentGOSelectionPieceLogic._isSelected = false;
            _currentGOSelectionPieceLogic._flagForTileGeneration = false;

            _currentGOSelection = null;
            _currentGOSelectionPieceLogic = null;

            CleanTiles();
            CleanChessPieces();
        }
    }

    public void HandleYellowHover(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        var pieceLogicScript = selection.GetComponent<PieceLogic>();

        if(pieceLogicScript != null && selectionRenderer != null)
        {
            if(_whosTurnNow == 0 && pieceLogicScript._whichSide == 0)
            {
                selectionRenderer.material.color = Color.yellow;
                if(Input.GetMouseButtonDown(0)) CurrentGOSelection(selection);
            }

            if(_whosTurnNow == 1 && pieceLogicScript._whichSide == 1)
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
        PieceLogic tempCurrentGoSelectionPieceLogic = _currentGOSelectionPieceLogic;
        int z = (int)_currentGOSelection.transform.position.z;
        int x = (int)_currentGOSelection.transform.position.x;
        _currentGOSelection = null;
        _currentGOSelectionPieceLogic = null;

        int new_z = (int)selection.transform.position.z;
        int new_x = (int)selection.transform.position.x;

        if(selection.GetComponent<Renderer>().material.color == Color.red)
        {
            Destroy(chessPiecesGrid.chessPiecesGrid[new_x, new_z]);
        }

        chessPiecesGrid.chessPiecesGrid[x, z] = null;
        chessPiecesGrid.chessPiecesGrid[new_x, new_z] = tempCurrentGOSelection;
        tempCurrentGOSelection.transform.position = tempCurrentGOSelection.transform.position + new Vector3(new_x - x, 0f, new_z - z);

        new_z = (int)tempCurrentGOSelection.transform.position.z;
        new_x = (int)tempCurrentGOSelection.transform.position.x;

        if(tempCurrentGOSelection.CompareTag("White")){
            if(tempCurrentGOSelection.GetComponent<PieceLogic>()._typeOfChessPiece == 0 && new_z == 7)
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
            if(tempCurrentGOSelection.GetComponent<PieceLogic>()._typeOfChessPiece == 0 && new_z == 0)
            {
                Destroy(tempCurrentGOSelection);
                chessPiecesGrid.chessPiecesGrid[new_x, new_z] = Instantiate(chessPiecesGrid.BlackQueenPrefab, new Vector3(new_x * gridCreator._gridSpaceSize, chessPiecesGrid._chessPieceYpos, new_z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid.chessPiecesGrid[new_x, new_z].transform.parent = chessPiecesGrid.transform;
            }
            _lastWhichSide = 1;
            _whosTurnNow = 0;
        }
        
        tempCurrentGoSelectionPieceLogic._isSelected = false;
        tempCurrentGoSelectionPieceLogic._flagForTileGeneration = false;

        CleanTiles();
        CleanChessPieces();
    }

    public void CleanChessPieces()
    {
        int howManyPieces = chessPiecesGrid.gameObject.transform.childCount;

        for(int i = 0; i < howManyPieces; i++)
        {
            GameObject currentPiece = chessPiecesGrid.gameObject.transform.GetChild(i).gameObject;
            if(currentPiece.CompareTag("White")){
                currentPiece.GetComponent<Renderer>().material.color = Color.white;
            }
            else if(currentPiece.CompareTag("Black"))
            {
                currentPiece.GetComponent<Renderer>().material.color = Color.black;
            }
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
                    if(count % 2 == 0)
                    {
                        gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.white;
                    }
                    else
                    {
                        gridCreator.chessBoardGrid[x, z].gameObject.GetComponent<Renderer>().material.color = Color.black;
                    }
                }
                count++;
            }
            count++;
        }
    }
}
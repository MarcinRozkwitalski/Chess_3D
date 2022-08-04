using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    GridCreator gridCreator;

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
        gridCreator = GameObject.Find("Grid").GetComponent<GridCreator>();
        GameInfoText = GameObject.Find("GameInfoText").GetComponent<Text>();
    }

    private void Update() 
    {
        if(_currentGOSelection) _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;

        if(_gameHasBeenStarted && _letPlayersChoosePieces)
        {
            if(_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                var pieceLogicScript = _selection.GetComponent<PieceLogic>();

                if(pieceLogicScript != null)
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

                if(selection.CompareTag("Black") || selection.CompareTag("White"))
                {
                    if(selection.name != "BlackTile(Clone)" && selection.name != "WhiteTile(Clone)" && selection.name != "Plane" && selection.gameObject != _currentGOSelection)
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
                    else if(selection.gameObject == _currentGOSelection)
                    {
                        if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
                    }
                    else if(selection == null || selection.name == "BlackTile(Clone)" || selection.name == "WhiteTile(Clone)" || selection.name == "Plane")
                    {
                        if(selection.name == "BlackTile(Clone)" || selection.name == "WhiteTile(Clone)")
                        {
                            if(selection.GetComponent<Renderer>().material.color == Color.green)
                            {
                                if(Input.GetMouseButtonDown(0))
                                {
                                    GameObject tempCurrentGOSelection = _currentGOSelection;
                                    PieceLogic tempCurrentGoSelectionPieceLogic = _currentGOSelectionPieceLogic;
                                    int z = (int)_currentGOSelection.transform.position.z;
                                    int x = (int)_currentGOSelection.transform.position.x;
                                    _currentGOSelection = null;
                                    _currentGOSelectionPieceLogic = null;

                                    int new_z = (int)selection.transform.position.z;
                                    int new_x = (int)selection.transform.position.x;
                                    
                                    gridCreator.chessPiecesGrid[new_x, new_z] = tempCurrentGOSelection;
                                    tempCurrentGOSelection.transform.position = tempCurrentGOSelection.transform.position + new Vector3(new_x - x, 0f, new_z - z);

                                    if(z % 2 == 0 && x % 2 == 0 || z % 2 != 0 && x % 2 != 0)
                                    {
                                        gridCreator.chessBoardGrid[x, z].name = "WhiteTile(Clone)";
                                    }
                                    else
                                    {
                                        gridCreator.chessBoardGrid[x, z].name = "BlackTile(Clone)";
                                    }

                                    if(tempCurrentGoSelectionPieceLogic._whichSide == 0)
                                    {
                                        tempCurrentGOSelection.GetComponent<Renderer>().material.color = Color.white;
                                        _lastWhichSide = 0;
                                        _whosTurnNow = 1;
                                    }
                                    else if(tempCurrentGoSelectionPieceLogic._whichSide == 1)
                                    {
                                        tempCurrentGOSelection.GetComponent<Renderer>().material.color = Color.black;
                                        _lastWhichSide = 1;
                                        _whosTurnNow = 0;
                                    }
                                    
                                    tempCurrentGoSelectionPieceLogic._isSelected = false;
                                    tempCurrentGoSelectionPieceLogic._flagForTileGeneration = false;

                                    CleanTiles();
                                }
                            }
                        }
                        else if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection(); 
                    }
                }
                else //RayCast detected object other than with tag Black or White
                if(Input.GetMouseButtonDown(0)) ResetCurrentGOSelection();
            }
            else //RayCast hits nothing (false)
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
            switch(_currentGOSelectionPieceLogic._whichSide)
            {
                case 0:
                    _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;

                    _currentGOSelectionPieceLogic._isSelected = false;
                    _currentGOSelectionPieceLogic._flagForTileGeneration = false;

                    _currentGOSelection = null;
                    _currentGOSelectionPieceLogic = null;

                    CleanTiles();
                    break;

                case 1:
                    _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;

                    _currentGOSelectionPieceLogic._isSelected = false;
                    _currentGOSelectionPieceLogic._flagForTileGeneration = false;

                    _currentGOSelection = null;
                    _currentGOSelectionPieceLogic = null;

                    CleanTiles();
                    break;

                default:
                    Debug.LogError("Whoops!");
                    break;
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

    public void CheckGameStatusButton()
    {
        for(int i = 0; i < gridCreator._xWidth; i++)
        {
            for (int j = 0; j < gridCreator._zWidth; j++)
            {
                Debug.Log("x = " + i + " | z = " + j + " | " + gridCreator.chessPiecesGrid[i, j]);
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
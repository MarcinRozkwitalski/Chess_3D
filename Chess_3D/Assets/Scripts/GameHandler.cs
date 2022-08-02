using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
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
        GameInfoText = GameObject.Find("GameInfoText").GetComponent<Text>();
    }

    private void Update() 
    {
        if(_currentGOSelection)
        {
            _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;
        }
        else if(!_currentGOSelection)
        {
            //need to add here variable/method which will turn LAST _currentGOSelection to proper color before going NULL
            //use "_lastWhichSide"
        }

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
                                if(Input.GetMouseButtonDown(0))
                                {
                                    if(_currentGOSelection == null)
                                    {
                                        _currentGOSelection = selection.gameObject;
                                        _currentGOSelectionPieceLogic = _currentGOSelection.GetComponent<PieceLogic>();
                                        _currentGOSelectionPieceLogic._isSelected = true;
                                        _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;
                                        Debug.Log("NULL CurrentGO: " + _currentGOSelection);
                                        Debug.Log("NULL Piece Logic: " + _currentGOSelectionPieceLogic);
                                    }
                                    else if(_currentGOSelection != null)
                                    {
                                        if(_currentGOSelectionPieceLogic._whichSide == 0)
                                        {
                                            _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;
                                            _currentGOSelectionPieceLogic._isSelected = false;
                                            _lastWhichSide = 0;
                                        }
                                        else if(_currentGOSelectionPieceLogic._whichSide == 1)
                                        {
                                            _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;
                                            _currentGOSelectionPieceLogic._isSelected = false;
                                            _lastWhichSide = 1;
                                        }
                                        _currentGOSelection = selection.gameObject;
                                        _currentGOSelectionPieceLogic = _currentGOSelection.GetComponent<PieceLogic>();
                                        _currentGOSelectionPieceLogic._isSelected = true;
                                        _currentGOSelection.GetComponent<Renderer>().material.color = Color.green;
                                        Debug.Log("!NULL CurrentGO: " + _currentGOSelection);
                                        Debug.Log("!NULL Piece Logic: " + _currentGOSelectionPieceLogic);
                                    }
                                }
                            }

                            if(_whosTurnNow == 1 && pieceLogicScript._whichSide == 1)
                            {
                                selectionRenderer.material.color = Color.yellow;
                                if(Input.GetMouseButton(0))
                                {
                                    //need logic here
                                }
                            }
                        }

                        _selection = selection;
                    }
                    else if(selection.gameObject == _currentGOSelection)
                    {
                        if(Input.GetMouseButtonDown(0))
                        {
                            _currentGOSelection = null;
                            _currentGOSelectionPieceLogic = null;
                        }
                    }
                    else if(selection == null || selection.name == "BlackTile(Clone)" || selection.name == "WhiteTile(Clone)" || selection.name == "Plane")
                    {
                        if(Input.GetMouseButtonDown(0))
                        {
                            //to refactor
                            if(_currentGOSelection && _currentGOSelectionPieceLogic)
                            {
                                if(_currentGOSelectionPieceLogic._whichSide == 0)
                                {
                                    _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;
                                    _currentGOSelectionPieceLogic._isSelected = false;
                                }
                                else if(_currentGOSelectionPieceLogic._whichSide == 1)
                                {
                                    _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;
                                    _currentGOSelectionPieceLogic._isSelected = false;
                                }

                                _currentGOSelection = null;
                                _currentGOSelectionPieceLogic = null;
                            }
                        }
                    }
                }
                else
                if(Input.GetMouseButtonDown(0))
                {
                    //to refactor
                    if(_currentGOSelection && _currentGOSelectionPieceLogic)
                    {
                        if(_currentGOSelectionPieceLogic._whichSide == 0)
                        {
                            _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;
                            _currentGOSelectionPieceLogic._isSelected = false;
                        }
                        else if(_currentGOSelectionPieceLogic._whichSide == 1)
                        {
                            _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;
                            _currentGOSelectionPieceLogic._isSelected = false;
                        }

                        _currentGOSelection = null;
                        _currentGOSelectionPieceLogic = null;
                    }
                }
            }
            else
            if(Input.GetMouseButtonDown(0))
            {
                //to refactor
                if(_currentGOSelection && _currentGOSelectionPieceLogic)
                {
                    if(_currentGOSelectionPieceLogic._whichSide == 0)
                    {
                        _currentGOSelection.GetComponent<Renderer>().material.color = Color.white;
                        _currentGOSelectionPieceLogic._isSelected = false;
                    }
                    else if(_currentGOSelectionPieceLogic._whichSide == 1)
                    {
                        _currentGOSelection.GetComponent<Renderer>().material.color = Color.black;
                        _currentGOSelectionPieceLogic._isSelected = false;
                    }

                    _currentGOSelection = null;
                    _currentGOSelectionPieceLogic = null;
                }
            }
        }
    }

    public void CheckGameStatusButton()
    {
        if(_currentGOSelection)
        {
            GameInfoText.text = "CurrentGOSelection exists: " + _currentGOSelection + "\nname: " + _currentGOSelection.name;
        }
        else if(!_currentGOSelection)
        {
            GameInfoText.text = "CurrentGOSelection doesn't exist. " + _currentGOSelection;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public bool _gameHasBeenStarted = false;
    public bool _letPlayersChoosePieces = false;

    [SerializeField] public int _whosTurnNow = 0; //0 - white, 1 - black

    Transform _selection;

    private void Update() 
    {
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
                    if(selection.name != "BlackTile(Clone)" && selection.name != "WhiteTile(Clone)" && selection.name != "Plane")
                    {
                        var selectionRenderer = selection.GetComponent<Renderer>();
                        var pieceLogicScript = selection.GetComponent<PieceLogic>();

                        if(pieceLogicScript != null)
                        {
                            if(selectionRenderer != null && _whosTurnNow == 0 && pieceLogicScript._whichSide == 0)
                                selectionRenderer.material.color = Color.green;

                            if(selectionRenderer != null && _whosTurnNow == 1 && pieceLogicScript._whichSide == 1)
                                selectionRenderer.material.color = Color.green;
                        }

                        _selection = selection;
                    }
                }
            }
        }   
    }

    private void FixedUpdate()
    {
        if(_whosTurnNow == 0 && _gameHasBeenStarted == true)
        {
            //unblock movement for whites and block for blacks
        }
        else if(_whosTurnNow == 1 && _gameHasBeenStarted == true)
        {
            //unblock movement for blacks and block for whites
        }
    }
}
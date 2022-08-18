using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiecesGrid : MonoBehaviour
{
    GridCreator gridCreator;
    GameHandler gameHandler;

    [SerializeField] public float _chessPieceYpos = 0.11f;

    [SerializeField] private GameObject WhitePawnPrefab;

    [SerializeField] private GameObject BlackPawnPrefab;

    [SerializeField] public GameObject WhiteKnightPrefab;

    [SerializeField] public GameObject BlackKnightPrefab;

    [SerializeField] public GameObject WhiteBishopPrefab;

    [SerializeField] public GameObject BlackBishopPrefab;

    [SerializeField] public GameObject WhiteRookPrefab;

    [SerializeField] public GameObject BlackRookPrefab;

    [SerializeField] public GameObject WhiteQueenPrefab;

    [SerializeField] public GameObject BlackQueenPrefab;

    [SerializeField] private GameObject WhiteKingPrefab;

    [SerializeField] private GameObject BlackKingPrefab;

    public GameObject[,] chessPiecesGrid;


    void Start()
    {
        gridCreator = GameObject.Find("TileGrid").GetComponent<GridCreator>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    public IEnumerator PlaceChessPieces()
    {
        chessPiecesGrid = new GameObject[gridCreator._xWidth, gridCreator._zWidth];

        if(gridCreator._xWidth > 7 && gridCreator._zWidth > 5)
        {
            List<int> defaultWhiteChessPieces = new List<int>();
            List<int> defaultBlackChessPieces = new List<int>();

            defaultWhiteChessPieces.Add(3);
            defaultWhiteChessPieces.Add(1);
            defaultWhiteChessPieces.Add(2);
            defaultWhiteChessPieces.Add(4);
            defaultWhiteChessPieces.Add(5);
            defaultWhiteChessPieces.Add(2);
            defaultWhiteChessPieces.Add(1);
            defaultWhiteChessPieces.Add(3);

            for(int i = 0; i < 8; i++) defaultWhiteChessPieces.Add(0);

            defaultBlackChessPieces = defaultWhiteChessPieces;

            for(int i = 0; i < defaultWhiteChessPieces.Count; i++) {
                Debug.Log("[" + i + "] = " + defaultWhiteChessPieces[i] + " < WhiteChessPiece");
                Debug.Log("[" + i + "] = " + defaultBlackChessPieces[i] + " < BlackChessPiece");
            }

            yield return StartCoroutine(PlaceChessPiecesFromCoordinates((gridCreator._xWidth / 2) - 4, 0, "White", defaultWhiteChessPieces, defaultBlackChessPieces));
            yield return StartCoroutine(PlaceChessPiecesFromCoordinates((gridCreator._xWidth / 2) - 4, gridCreator._zWidth - 1, "Black", defaultWhiteChessPieces, defaultBlackChessPieces));

            gameHandler._gameHasBeenStarted = true;
            gameHandler._letPlayersChoosePieces = true;
        }

        yield return null;
    }

    public IEnumerator PlaceChessPiecesFromCoordinates(int x, int z, string WhichColor, List<int> whitePieces, List<int> blackPieces)
    {
        switch(WhichColor)
        {
            case "White":
                for(int i = 0; i < 2; i++)
                {
                    if(i == 0)
                    {
                        for(int j = 0; j < 8; j++)
                        {
                            int id = whitePieces[j];
                            CheckWhiteChessPiece(id, x, z);
                            if(gridCreator.normalSpeed) yield return new WaitForSeconds(0.1f);
                            x++;
                        }
                    }

                    if(i == 1)
                    {
                        x = (gridCreator._xWidth / 2) - 4;
                        z++;

                        for(int j = 8; j < 16; j++)
                        {
                            int id = whitePieces[j];
                            CheckWhiteChessPiece(id, x, z);
                            if(gridCreator.normalSpeed) yield return new WaitForSeconds(0.1f);
                            x++;
                        }
                    }
                }
                break;

            case "Black":
                for(int i = 0; i < 2; i++)
                {
                    if(i == 0)
                    {
                        for(int j = 0; j < 8; j++)
                        {
                            int id = blackPieces[j];
                            CheckBlackChessPiece(id, x, z);
                            if(gridCreator.normalSpeed) yield return new WaitForSeconds(0.1f);
                            x++;
                        }
                    }

                    if(i == 1)
                    {
                        x = (gridCreator._xWidth / 2) - 4;
                        z--;
                        for(int j = 8; j < 16; j++)
                        {
                            int id = blackPieces[j];
                            CheckBlackChessPiece(id, x, z);
                            if(gridCreator.normalSpeed) yield return new WaitForSeconds(0.1f);
                            x++;
                        }
                    }
                }
                break;
        }
    }

    public void CheckWhiteChessPiece(int id, int x, int z)
    {
        switch(id)
        {
            case 0:
                chessPiecesGrid[x, z] = Instantiate(WhitePawnPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 1:
                chessPiecesGrid[x, z] = Instantiate(WhiteKnightPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 2:
                chessPiecesGrid[x, z] = Instantiate(WhiteBishopPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 3:
                chessPiecesGrid[x, z] = Instantiate(WhiteRookPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 4:
                chessPiecesGrid[x, z] = Instantiate(WhiteQueenPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 5:
                chessPiecesGrid[x, z] = Instantiate(WhiteKingPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            default:
                break;
        }
    }

    public void CheckBlackChessPiece(int id, int x, int z)
    {
        switch(id)
        {
            case 0:
                chessPiecesGrid[x, z] = Instantiate(BlackPawnPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.Euler(new Vector3(0, 180, 0)));
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 1:
                chessPiecesGrid[x, z] = Instantiate(BlackKnightPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.Euler(new Vector3(0, 180, 0)));
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 2:
                chessPiecesGrid[x, z] = Instantiate(BlackBishopPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.Euler(new Vector3(0, 180, 0)));
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 3:
                chessPiecesGrid[x, z] = Instantiate(BlackRookPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.Euler(new Vector3(0, 180, 0)));
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 4:
                chessPiecesGrid[x, z] = Instantiate(BlackQueenPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.Euler(new Vector3(0, 180, 0)));
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            case 5:
                chessPiecesGrid[x, z] = Instantiate(BlackKingPrefab, new Vector3(x * gridCreator._gridSpaceSize, _chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.Euler(new Vector3(0, 180, 0)));
                chessPiecesGrid[x, z].transform.parent = transform;
                break;
            default:
                break;
        }
    }
}
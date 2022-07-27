using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiecesHandler : MonoBehaviour
{
    GridCreator gridCreator;

    [SerializeField]
    private float chessPieceYpos = 0.11f;

    [SerializeField]
    private GameObject WhitePawnPrefab;

    [SerializeField]
    private GameObject BlackPawnPrefab;

    [SerializeField]
    private GameObject WhiteKnightPrefab;

    [SerializeField]
    private GameObject BlackKnightPrefab;

    [SerializeField]
    private GameObject WhiteBishopPrefab;

    [SerializeField]
    private GameObject BlackBishopPrefab;

    [SerializeField]
    private GameObject WhiteRookPrefab;

    [SerializeField]
    private GameObject BlackRookPrefab;

    [SerializeField]
    private GameObject WhiteQueenPrefab;

    [SerializeField]
    private GameObject BlackQueenPrefab;

    [SerializeField]
    private GameObject WhiteKingPrefab;

    [SerializeField]
    private GameObject BlackKingPrefab;

    void Start()
    {
        gridCreator = GameObject.Find("Grid").GetComponent<GridCreator>();
    }

    public IEnumerator PlaceChessPieces()
    {
        if(gridCreator._xWidth == 8 && gridCreator._zWidth == 8)
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

            StartCoroutine(PlaceChessPiecesFromCoordinates(0, 0, "White", defaultWhiteChessPieces, defaultBlackChessPieces));
            StartCoroutine(PlaceChessPiecesFromCoordinates(0, 7, "Black", defaultWhiteChessPieces, defaultBlackChessPieces));
        }

        yield return null;
    }

    public IEnumerator PlaceChessPiecesFromCoordinates(int x, int z, string WhichColor, List<int> whitePieces, List<int> blackPieces)
    {
        if(WhichColor == "White")
        {
            for(int i = 0; i < 2; i++)
            {
                if(i == 0)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        int id = whitePieces[j];
                        CheckWhiteChessPiece(id , x, z);
                        yield return new WaitForSeconds(0.1f);
                        x++;
                    }
                }
                yield return new WaitForSeconds(0.5f);

                if(i == 1)
                {
                    x = 0;
                    z++;

                    for(int j = 8; j < 16; j++)
                    {
                        int id = whitePieces[j];
                        CheckWhiteChessPiece(id , x, z);
                        yield return new WaitForSeconds(0.1f);
                        x++;
                    }
                }
            }
        }

        if(WhichColor == "Black")
        {
            for(int i = 0; i < 2; i++)
            {
                if(i == 0)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        int id = blackPieces[j];
                        CheckBlackChessPiece(id , x, z);
                        yield return new WaitForSeconds(0.1f);
                        x++;
                    }
                }
                yield return new WaitForSeconds(0.5f);

                if(i == 1)
                {
                    x = 0;
                    z--;
                    for(int j = 8; j < 16; j++)
                    {
                        int id = blackPieces[j];
                        CheckBlackChessPiece(id , x, z);
                        yield return new WaitForSeconds(0.1f);
                        x++;
                    }
                }
            }
        }

        yield return null;
    }

    public void CheckWhiteChessPiece(int id, int x, int z)
    {
        switch(id)
        {
            case 0:
                gridCreator.chessBoardGrid[x, z] = Instantiate(WhitePawnPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 1:
                gridCreator.chessBoardGrid[x, z] = Instantiate(WhiteKnightPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 2:
                gridCreator.chessBoardGrid[x, z] = Instantiate(WhiteBishopPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 3:
                gridCreator.chessBoardGrid[x, z] = Instantiate(WhiteRookPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 4:
                gridCreator.chessBoardGrid[x, z] = Instantiate(WhiteQueenPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 5:
                gridCreator.chessBoardGrid[x, z] = Instantiate(WhiteKingPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
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
                gridCreator.chessBoardGrid[x, z] = Instantiate(BlackPawnPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 1:
                gridCreator.chessBoardGrid[x, z] = Instantiate(BlackKnightPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 2:
                gridCreator.chessBoardGrid[x, z] = Instantiate(BlackBishopPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 3:
                gridCreator.chessBoardGrid[x, z] = Instantiate(BlackRookPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 4:
                gridCreator.chessBoardGrid[x, z] = Instantiate(BlackQueenPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            case 5:
                gridCreator.chessBoardGrid[x, z] = Instantiate(BlackKingPrefab, new Vector3(x * gridCreator._gridSpaceSize, chessPieceYpos, z * gridCreator._gridSpaceSize), Quaternion.identity);
                gridCreator.chessBoardGrid[x, z].transform.parent = transform;
                break;
            default:
                break;
        }
    }
}
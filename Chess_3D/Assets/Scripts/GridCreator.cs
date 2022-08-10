using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    ChessPiecesGrid chessPiecesGrid;

    public int _xWidth = 8;
    public int _zWidth = 8;
    public float _gridSpaceSize = 1f;
    
    [SerializeField] public GameObject gridCellWhiteTilePrefab;

    [SerializeField] public GameObject gridCellBlackTilePrefab;

    public bool normalSpeed = false;

    public GameObject[,] chessBoardGrid;

    void Start()
    {
        chessPiecesGrid = GameObject.Find("ChessPiecesGrid").GetComponent<ChessPiecesGrid>();
    }

    public IEnumerator CreateGrid()
    {
        chessBoardGrid = new GameObject[_xWidth, _zWidth];

        if (gridCellWhiteTilePrefab == null || gridCellBlackTilePrefab == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab(s) not attached!");
            StopAllCoroutines();
            yield return null;
        }

        int x = 0, z = 0;

        List<int> xList = new List<int>();
        List<int> zList = new List<int>();

        for(int i = 0; i < _xWidth; i++) xList.Add(i);
        for(int i = 0; i < _zWidth; i++) zList.Add(i);

        if(_xWidth == 0 || _zWidth == 0)
        {
            Debug.LogError("ERROR: Given range(s) is/are equal to 0!");
        }
        else
        while(true)
        {
            if(xList.Contains(x) && z == 0)
            {
                if(x == 0 && z == 0)
                {
                    CreateTilePrefabOnGrid("White", x, z);
                    if(normalSpeed) yield return new WaitForSeconds(0.05f);
                }
                x++;
                CreateTilePrefabOnGrid("Black", x, z);
                if(normalSpeed) yield return new WaitForSeconds(0.05f);

                int tempX = x;

                if(tempX > _zWidth - 1) tempX = _zWidth - 1;

                for(int i = 0; i < tempX; i++)
                {
                    x--;
                    z++;
                    CreateTilePrefabOnGrid("Black", x, z);
                    if(normalSpeed) yield return new WaitForSeconds(0.1f/((float)tempX));
                }
            }

            if(xList.Contains(x) && z == _zWidth - 1)
            {
                x++;
                CreateTilePrefabOnGrid("White", x, z);
                if(normalSpeed) yield return new WaitForSeconds(0.05f); 

                int tempX = x;
                int tempZ = z;

                if(tempX == _xWidth - 1 && tempZ == _zWidth - 1)
                {
                    tempZ = 0;
                    tempX = 0;
                }
                else if(tempZ > _xWidth - 1)
                {
                    tempZ = _xWidth - 1 - x;
                    tempX = 0;
                }
                else if(tempZ == _zWidth - 1)
                {
                    tempZ = _zWidth - 1;
                    tempX = 0;
                }
                
                if(tempZ + x > _xWidth - 1)
                {
                    tempZ = _xWidth - 1 - x;
                    tempX = 0;
                }

                for(int i = 0; i < tempZ - tempX; i++)
                {
                    x++;
                    z--;
                    CreateTilePrefabOnGrid("White", x, z);
                    if(normalSpeed) yield return new WaitForSeconds(0.1f/((float)tempZ - (float)tempX));
                }
            }

            if(x == 0 && zList.Contains(z))
            {
                z++;
                CreateTilePrefabOnGrid("White", x, z);
                if(normalSpeed) yield return new WaitForSeconds(0.05f);

                int tempZ = z;

                if(tempZ > _xWidth - 1) tempZ = _xWidth - 1;

                for(int i = 0; i < tempZ; i++)
                {
                    x++;
                    z--;
                    CreateTilePrefabOnGrid("White", x, z);
                    if(normalSpeed) yield return new WaitForSeconds(0.1f/((float)tempZ));
                }
            }

            if(x == _xWidth - 1 && zList.Contains(z))
            {
                if(x == _xWidth - 1 && z == _zWidth - 1) break;
                else
                {
                    z++;
                    CreateTilePrefabOnGrid("Black", x, z);
                    if(normalSpeed) yield return new WaitForSeconds(0.05f);

                    int tempX = x;
                    int tempZ = z;

                    if(_xWidth != _zWidth) 
                        if(tempX == _xWidth - 1)
                        {
                            tempZ = 0;
                            if(tempX + z > _zWidth - 1) tempX = _zWidth - 1 - z;
                        }

                    for(int i = 0; i < tempX - tempZ; i++)
                    {
                        x--;
                        z++;
                        CreateTilePrefabOnGrid("Black", x, z);
                        if(normalSpeed) yield return new WaitForSeconds(0.1f/((float)tempX - (float)tempZ));
                    }
                }
            }

            if(x == _xWidth - 1 && z == _zWidth - 1) break;
        }

        // horizontal not line to line
        // int count = 0;
        // 
        // for (int x = 0; x < x_width; x++)
        // {
        //     for (int z = 0; z < z_width; z++)
        //     {
        //         if(count % 2 == 0)
        //         {
        //             grid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
        //             grid[x, z].transform.parent = transform;
        //             yield return new WaitForSeconds(0.04f);
        //         }
        //         else
        //         {
        //             grid[x, z] = Instantiate(gridCellBlackTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
        //             grid[x, z].transform.parent = transform;
        //             yield return new WaitForSeconds(0.04f);
        //         }
        //         count++;
        //     }
        //     count++;
        // }

        if(normalSpeed) yield return new WaitForSeconds(1f);
        if(normalSpeed) yield return chessPiecesGrid.PlaceChessPieces();
        else            StartCoroutine(chessPiecesGrid.PlaceChessPieces());

        StopCoroutine(CreateGrid());
    }

    private void CreateTilePrefabOnGrid(string whichTile, int x, int z)
    {
        if(whichTile == "Black")
        {
            chessBoardGrid[x, z] = Instantiate(gridCellBlackTilePrefab, new Vector3(x * _gridSpaceSize, 0.01f, z * _gridSpaceSize), Quaternion.identity);
            chessBoardGrid[x, z].transform.parent = transform;
        }
        else
        if(whichTile == "White")
        {
            chessBoardGrid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * _gridSpaceSize, 0.01f, z * _gridSpaceSize), Quaternion.identity);
            chessBoardGrid[x, z].transform.parent = transform;
        }
    }
}
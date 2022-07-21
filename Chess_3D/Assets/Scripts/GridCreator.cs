using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

    private int x_width = 25;
    private int z_width = 25;
    private float GridSpaceSize = 1f;
    
    [SerializeField]
    private GameObject gridCellWhiteTilePrefab;

    [SerializeField]
    private GameObject gridCellBlackTilePrefab;

    private GameObject[,] grid;

    void Start()
    {
        StartCoroutine(CreateGrid());
    }

    private IEnumerator CreateGrid()
    {
        grid = new GameObject[x_width, z_width];

        if (gridCellWhiteTilePrefab == null || gridCellBlackTilePrefab == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab(s) not attached!");
            yield return null;
        }

        bool finished = false;

        int x = 0, z = 0;

        List<int> x_list = new List<int>();
        List<int> z_list = new List<int>();

        for (int i = 0; i < x_width; i++) x_list.Add(i);

        for (int i = 0; i < z_width; i++) z_list.Add(i);

        while(!finished)
        {
            if(x_list.Contains(x) && z == 0)
            {
                if(x == 0 && z == 0)
                {
                    CreateTilePrefabOnGrid("White", x, z);
                    yield return new WaitForSeconds(0.05f); 
                }
                x++;
                CreateTilePrefabOnGrid("Black", x, z);
                yield return new WaitForSeconds(0.05f);

                int new_x = x;

                if(new_x > z_width - 1) new_x = z_width - 1;

                for(int i = 0; i < new_x; i++)
                {
                    x--;
                    z++;
                    CreateTilePrefabOnGrid("Black", x, z);
                    yield return new WaitForSeconds(0.1f/((float)new_x));
                }
            }

            if(x_list.Contains(x) && z == z_width - 1)
            {
                x++;
                CreateTilePrefabOnGrid("White", x, z);
                yield return new WaitForSeconds(0.05f); 

                int new_x = x;
                int new_z = z;

                if(new_x == x_width - 1 && new_z == z_width - 1)
                {
                    new_z = 0;
                    new_x = 0;
                }
                else if(new_z > x_width - 1)
                {
                    new_z = x_width - 1 - x;
                    new_x = 0;
                }
                else if(new_z == z_width - 1)
                {
                    new_z = z_width - 1;
                    new_x = 0;
                }
                
                if(new_z + x > x_width - 1)
                {
                    new_z = x_width - 1 - x;
                    new_x = 0;
                }

                for(int i = 0; i < new_z - new_x; i++)
                {
                    x++;
                    z--;
                    CreateTilePrefabOnGrid("White", x, z);
                    yield return new WaitForSeconds(0.1f/((float)new_z - (float)new_x));
                }
            }

            if(x == 0 && z_list.Contains(z))
            {
                z++;
                CreateTilePrefabOnGrid("White", x, z);
                yield return new WaitForSeconds(0.05f);

                int new_z = z;

                if(new_z > x_width - 1) new_z = x_width - 1;

                for(int i = 0; i < new_z; i++)
                {
                    x++;
                    z--;
                    CreateTilePrefabOnGrid("White", x, z);
                    yield return new WaitForSeconds(0.1f/((float)new_z));
                }
            }

            if(x == x_width - 1 && z_list.Contains(z))
            {
                if(x == x_width - 1 && z == z_width - 1) finished = true;
                else
                {
                    z++;
                    CreateTilePrefabOnGrid("Black", x, z);
                    yield return new WaitForSeconds(0.05f);

                    int new_x = x;
                    int new_z = z;

                    if(x_width != z_width) 
                        if(new_x == x_width - 1)
                        {
                            new_z = 0;
                            if(new_x + z > z_width - 1) new_x = z_width - 1 - z;
                        }

                    for(int i = 0; i < new_x - new_z; i++)
                    {
                        x--;
                        z++;
                        CreateTilePrefabOnGrid("Black", x, z);
                        yield return new WaitForSeconds(0.1f/((float)new_x - (float)new_z));
                    }
                }
            }

            if(x == x_width - 1 && z == z_width - 1) finished = true;
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

        StopCoroutine(CreateGrid());
    }

    private void CreateTilePrefabOnGrid(string whichTile, int x, int z)
    {
        if(whichTile == "Black")
        {
            grid[x, z] = Instantiate(gridCellBlackTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
            grid[x, z].transform.parent = transform;
        }
        else
        if(whichTile == "White")
        {
            grid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
            grid[x, z].transform.parent = transform;
        }
    }
}
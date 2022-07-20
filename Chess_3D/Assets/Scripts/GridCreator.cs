using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

    private int x_width = 8;
    private int z_width = 8;
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
            Debug.LogError("ERROR: Grid Cell Prefab not attached!");
            yield return null;
        }

        int count = 0;
        bool finished = false;

        int x = 0, z = 0;

        List<int> x_list = new List<int>();
        List<int> z_list = new List<int>();

        for (int i = 0; i < x_width; i++)
        {
            x_list.Add(i);
        }

        for (int i = 0; i < z_width; i++)
        {
            z_list.Add(i);
        }

        while(!finished)
        {
            Debug.Log("pętla jeszcze nie skończona");
            Debug.Log("x = " + x + "/z = " + z + "/x_width = " + x_width + "/z_width = " + z_width);

            if(x_list.Contains(x) && z == 0)
            {
                if(x == 0 & z == 0)
                {
                    grid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                    grid[x, z].transform.parent = transform;
                    yield return new WaitForSeconds(0.05f); 
                }
                x++;
                grid[x, z] = Instantiate(gridCellBlackTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                grid[x, z].transform.parent = transform;
                yield return new WaitForSeconds(0.05f);

                int new_x = x;

                for(int i = 0; i < new_x; i++)
                {
                    x--;
                    z++;
                    grid[x, z] = Instantiate(gridCellBlackTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                    grid[x, z].transform.parent = transform;
                    yield return new WaitForSeconds(0.05f);
                }
            }

            if(x_list.Contains(x) && z == z_width - 1)
            {
                x++;
                grid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                grid[x, z].transform.parent = transform;
                yield return new WaitForSeconds(0.05f); 

                int new_x = x;
                int new_z = z;

                for(int i = 0; i < new_z - new_x; i++)
                {
                    x++;
                    z--;
                    grid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                    grid[x, z].transform.parent = transform;
                    yield return new WaitForSeconds(0.05f);
                }
            }

            if(x == 0 && z_list.Contains(z))
            {
                z++;
                grid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                grid[x, z].transform.parent = transform;
                yield return new WaitForSeconds(0.05f);

                int new_z = z;

                for(int i = 0; i < new_z; i++)
                {
                    x++;
                    z--;
                    grid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                    grid[x, z].transform.parent = transform;
                    yield return new WaitForSeconds(0.05f);
                }
            }

            if(x == x_width - 1 && z_list.Contains(z))
            {
                if(x == x_width - 1 && z == z_width - 1)
                {
                    finished = true;
                }
                else
                {
                    z++;
                    grid[x, z] = Instantiate(gridCellBlackTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                    grid[x, z].transform.parent = transform;
                    yield return new WaitForSeconds(0.05f);

                    int new_x = x;
                    int new_z = z;

                    for(int i = 0; i < new_x - new_z; i++)
                    {
                        x--;
                        z++;
                        grid[x, z] = Instantiate(gridCellBlackTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                        grid[x, z].transform.parent = transform;
                        yield return new WaitForSeconds(0.05f);
                    }
                }
            }

            if(x == x_width - 1 && z == z_width - 1)
            {
                finished = true;
            }
        }

        // horizontal not line to line
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
}
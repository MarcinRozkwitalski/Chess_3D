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
            yield return new WaitForSeconds(1f);
        }

        int count = 0;

        for (int x = 0; x < x_width; x++)
        {
            for (int z = 0; z < z_width; z++)
            {
                if(count % 2 == 0)
                {
                    grid[x, z] = Instantiate(gridCellWhiteTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                    grid[x, z].transform.parent = transform;
                    yield return new WaitForSeconds(0.05f);
                }
                else
                {
                    grid[x, z] = Instantiate(gridCellBlackTilePrefab, new Vector3(x * GridSpaceSize, 0.01f, z * GridSpaceSize), Quaternion.identity);
                    grid[x, z].transform.parent = transform;
                    yield return new WaitForSeconds(0.05f);
                }
                
                count++;
            }
            
            count++;
        }

        StopCoroutine(CreateGrid());
    }
}
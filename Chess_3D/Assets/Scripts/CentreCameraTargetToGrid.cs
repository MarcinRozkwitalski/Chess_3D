using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreCameraTargetToGrid : MonoBehaviour
{
    GridCreator gridCreator;

    void Start()
    {
        gridCreator = GameObject.Find("Grid").GetComponent<GridCreator>();

        transform.position = new Vector3(((float)(gridCreator._xWidth - 1) / 2) * gridCreator._gridSpaceSize, 
                                            1, 
                                            ((float)(gridCreator._zWidth - 1) / 2) * gridCreator._gridSpaceSize);
    }
}
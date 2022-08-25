using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreCameraTargetToGrid : MonoBehaviour
{
    GridCreator gridCreator;
    MoveCameraAroundObject moveCameraAroundObject;

    void Start()
    {
        gridCreator = GameObject.Find("TileGrid").GetComponent<GridCreator>();
        moveCameraAroundObject = GameObject.Find("Main Camera").GetComponent<MoveCameraAroundObject>();

        transform.position = new Vector3(((float)(gridCreator._xWidth - 1) / 2) * gridCreator._gridSpaceSize, 
                                            1, 
                                            ((float)(gridCreator._zWidth - 1) / 2) * gridCreator._gridSpaceSize);

        moveCameraAroundObject.targetObjectNextPosition = gameObject.transform.position;
    }
}
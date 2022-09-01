using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPlaneToGrid : MonoBehaviour
{
    GridCreator gridCreator;
    
    Vector3 _newTransform;

    void Start()
    {
        gridCreator = GameObject.Find("TileGrid").GetComponent<GridCreator>();

        // - 0.1f *gridCreator._gridSpaceSize

        transform.position = new Vector3(((float)(gridCreator._xWidth - 1) / 2) * gridCreator._gridSpaceSize, 
                                            0, 
                                            ((float)(gridCreator._zWidth - 1) / 2) * gridCreator._gridSpaceSize);

        _newTransform = new Vector3(((0.1f * (float)gridCreator._xWidth) + 0.1f) * (float)gridCreator._gridSpaceSize, 
                                            1, 
                                            ((0.1f * (float)gridCreator._zWidth) + 0.1f) * (float)gridCreator._gridSpaceSize);

        // if(gridCreator.normalSpeed) StartCoroutine(LerpStrechOut(2f));
        // else                        StartCoroutine(LerpStrechOut(0.01f));
    }

    public void StartAnimation()
    {
        if(gridCreator.normalSpeed) StartCoroutine(LerpStrechOut(2f));
        else                        StartCoroutine(LerpStrechOut(0.01f));
    }

    private IEnumerator LerpStrechOut(float duration)
    {
        float time = 0;
        float t = time / duration;

        Transform startValue = transform;
        startValue.localScale = transform.localScale;

        float newDuration = duration * 8;

        while(time < duration)
        {   
            t = time / newDuration;
            // t = t * t * t * (t * (6f * t - 15f) + 10f);
            t = t * t * (3f - 2f * t);
            transform.localScale = Vector3.Lerp(startValue.localScale, _newTransform, t);
            // t += 0.007f * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = _newTransform;

        if(gridCreator.normalSpeed) yield return gridCreator.CreateGrid();
        else                        StartCoroutine(gridCreator.CreateGrid());

        StopCoroutine(LerpStrechOut(0f));
    }
}
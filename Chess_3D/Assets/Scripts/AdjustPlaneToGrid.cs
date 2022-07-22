using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPlaneToGrid : MonoBehaviour
{
    GridCreator gridCreator;
    Vector3 _newTransform;
    Vector3 endValue;

    void Start()
    {
        gridCreator = GameObject.Find("Grid").GetComponent<GridCreator>();

        // - 0.1f *gridCreator._gridSpaceSize

        transform.position = new Vector3(((float)(gridCreator._xWidth - 1) / 2) * gridCreator._gridSpaceSize, 
                                            0, 
                                            ((float)(gridCreator._zWidth - 1) / 2) * gridCreator._gridSpaceSize);

        _newTransform = new Vector3(((0.1f * (float)gridCreator._xWidth) + 0.1f) * (float)gridCreator._gridSpaceSize, 
                                            1, 
                                            ((0.1f * (float)gridCreator._zWidth) + 0.1f) * (float)gridCreator._gridSpaceSize);

        StartCoroutine(LerpStrechOut(2f));
    }

    private IEnumerator LerpStrechOut(float duration)
    {
        float time = 0;

        float t = time / duration;

        Transform startValue = transform;

        startValue.localScale = transform.localScale;

        float newDuration = duration * 8;

        while (time < duration)
        {   
            t = time / newDuration;
            // t = t * t * t * (t * (6f * t - 15f) + 10f);
            t = t * t * (3f - 2f * t);
            transform.localScale = Vector3.Lerp(startValue.localScale, _newTransform, t);
            // t += 0.007f * Time.deltaTime;
            time += Time.deltaTime;
            Debug.Log(t + "\t" + time);
            yield return null;
        }

        transform.localScale = _newTransform;

        StopCoroutine(LerpStrechOut(0f));
    }
}
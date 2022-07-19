using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeHandler : MonoBehaviour
{

    MeshRenderer meshRenderer;
    Color endValue;

    void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();

        if(meshRenderer.materials[0].name == "Black (Instance)")
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else if(meshRenderer.materials[0].name == "White (Instance)")
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        StartCoroutine(LerpFadeIn(0.5f));
    }

    private IEnumerator LerpFadeIn(float duration)
    {
        float time = 0;
        Color startValue = this.GetComponent<MeshRenderer>().material.color;

        if(meshRenderer.materials[0].name == "Black (Instance)")
        {
            endValue = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }
        else if(meshRenderer.materials[0].name == "White (Instance)")
        {
            endValue = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        while (time < duration)
        {
            meshRenderer.material.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        meshRenderer.material.color = endValue;

        StopCoroutine(LerpFadeIn(0f));
    }
}
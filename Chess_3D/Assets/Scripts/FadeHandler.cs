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

        switch(gameObject.tag)
        {
            case "White":
                this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                break;
            case "Black":
                this.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                break;
            default:
                break;
        }

        StartCoroutine(LerpFadeIn(0.5f));
    }

    private IEnumerator LerpFadeIn(float duration)
    {
        float time = 0;
        Color startValue = this.GetComponent<MeshRenderer>().material.color;

        switch(gameObject.tag){
            case "White":
                endValue = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                break;
            case "Black":
                endValue = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                break;
            default:
                break;
        }

        while(time < duration)
        {
            meshRenderer.material.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        meshRenderer.material.color = endValue;
        meshRenderer.material.SetFloat("_Mode", 0);
        meshRenderer.material.SetOverrideTag("RenderType", "");
        meshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        meshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        meshRenderer.material.SetInt("_ZWrite", 1);
        meshRenderer.material.DisableKeyword("_ALPHATEST_ON");
        meshRenderer.material.DisableKeyword("_ALPHABLEND_ON");
        meshRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        meshRenderer.material.renderQueue = -1;

        StopCoroutine(LerpFadeIn(0f));
    }
}
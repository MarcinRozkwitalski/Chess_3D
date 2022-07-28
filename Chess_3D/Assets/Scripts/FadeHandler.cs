using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeHandler : MonoBehaviour
{

    MeshRenderer _meshRenderer;
    Color _endValue;

    void Start()
    {
        _meshRenderer = this.GetComponent<MeshRenderer>();

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
                _endValue = Color.white;
                break;
            case "Black":
                _endValue = Color.black;
                break;
            default:
                break;
        }

        while(time < duration)
        {
            _meshRenderer.material.color = Color.Lerp(startValue, _endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        _meshRenderer.material.color = _endValue;
        _meshRenderer.material.SetFloat("_Mode", 0);
        _meshRenderer.material.SetOverrideTag("RenderType", "");
        _meshRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        _meshRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        _meshRenderer.material.SetInt("_ZWrite", 1);
        _meshRenderer.material.DisableKeyword("_ALPHATEST_ON");
        _meshRenderer.material.DisableKeyword("_ALPHABLEND_ON");
        _meshRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _meshRenderer.material.renderQueue = -1;

        StopCoroutine(LerpFadeIn(0f));
    }
}
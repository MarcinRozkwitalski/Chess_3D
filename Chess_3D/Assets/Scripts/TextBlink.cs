using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    TMP_Text _text;

    [SerializeField] public float _blinkFadeInTime = 0.5f;

    [SerializeField] public float _blinkStayTime = 1f;

    [SerializeField] public float _blinkFadeOutTime = 0.7f;

    public bool _destroyThis = false;

    private float _timeChecker = 0f;

    private Color _color;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _color = _text.color;
    }

    void Update()
    {
        _timeChecker += Time.deltaTime;
        if(_timeChecker < _blinkFadeInTime)
        {
            _text.color = new Color(_color.r, _color.g, _color.b, _timeChecker / _blinkFadeInTime);
        }
        else if(_timeChecker < _blinkFadeInTime + _blinkStayTime)
        {
            _text.color = new Color(_color.r, _color.g, _color.b, 1);
        }
        else if(_timeChecker < _blinkFadeInTime + _blinkStayTime + _blinkFadeOutTime)
        {
            _text.color = new Color(_color.r, _color.g, _color.b, 1 - (_timeChecker - (_blinkFadeInTime + _blinkStayTime)) / _blinkFadeOutTime);
        }
        else
        {
            _timeChecker = 0f;
            DestroyThis();
        }
    }

    public void DestroyThis()
    {
        if(_destroyThis)
        {
            Destroy(gameObject);
        }
    }
}

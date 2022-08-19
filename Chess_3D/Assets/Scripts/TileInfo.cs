using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public bool _isBeatableByWhite;
    public bool _isBeatableByBlack;

    void Start() {
        _isBeatableByWhite = false;
        _isBeatableByBlack = false;
    }

    public void SetOnWhite()
    {
        _isBeatableByWhite = true;
    }

    public void SetOffWhite()
    {
        _isBeatableByWhite = false;
    }

    public void SetOnBlack()
    {
        _isBeatableByBlack = true;
    }

    public void SetOffBlack()
    {
        _isBeatableByBlack = false;
    }
}
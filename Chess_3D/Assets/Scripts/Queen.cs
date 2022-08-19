using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : PieceInfo
{
    public void Movement(int _whichSide)
    {
        gameObject.GetComponent<Bishop>().Movement(_whichSide);
        gameObject.GetComponent<Rook>().Movement(_whichSide);
    }

    public void BeatableTiles(int _whichSide)
    {
        gameObject.GetComponent<Bishop>().BeatableTiles(_whichSide);
        gameObject.GetComponent<Rook>().BeatableTiles(_whichSide);
    }
}
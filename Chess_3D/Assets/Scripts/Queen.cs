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
}

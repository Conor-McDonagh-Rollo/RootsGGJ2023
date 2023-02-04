using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int basePower;
    public int accuracy;

    public virtual void callMove(){}

    protected Move(int bP, int acc)
    {
        basePower = bP;
        accuracy = acc;
    }
}

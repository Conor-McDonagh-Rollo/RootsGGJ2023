using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int basePower;
    public int accuracy;
    public string moveName;

    public virtual void callMove(){}

    protected Move(int bP, int acc, string name)
    {
        basePower = bP;
        accuracy = acc;
        this.moveName = name;
    }
}

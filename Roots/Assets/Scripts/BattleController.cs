using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public SaplingMonStats[] fightingMon = new SaplingMonStats[2];
    bool thisPlayersTurn = false;
    private BattleMenu menu;

    private void Start()
    {
        menu = GetComponent<BattleMenu>();
        menu.AddMon(fightingMon);
    }

    void TurnController()
    {
        if (thisPlayersTurn)
        {
            menu.Toggle(true);
        }

        else
        {
            menu.Toggle(false);
        }
    }


}

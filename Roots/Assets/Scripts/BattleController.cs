using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public SaplingMonStats[] fightingMon = new SaplingMonStats[2];

    float DamageCalculator(Move t_moveUsed, int attackingMon, int defendingMon)
    {
        float damage = 0;
        float level = 10; // temporary - if we want, we can swap this out later
                          // for actual saplingmon levels

        // got this calculation here: https://bulbapedia.bulbagarden.net/wiki/Damage#Generation_V_onward

        damage = ((2 * level) / 5.0f) + 2;
        damage = damage * t_moveUsed.basePower;

        damage = damage * (fightingMon[attackingMon].Attack /
            fightingMon[defendingMon].Defense);

        damage = (damage / 50.0f) + 2.0f;


        return damage;
    }
}

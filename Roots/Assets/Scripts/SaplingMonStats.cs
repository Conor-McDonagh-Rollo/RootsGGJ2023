using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingMonStats : MonoBehaviour
{
    // I got these ranges from here: https://pokemondb.net/pokebase/175092/what-the-lowest-and-highest-value-in-stat-any-pokemon-can-have

    public string monName;

    [Range(1.0f, 252.0f)]
    public int HP;

    [Range(4.0f, 252.0f)]
    public int Attack;

    [Range(4.0f, 252.0f)]
    public int Defense;

    // temporarily commented out Sp attack and defense,
    // because we dont have move categories in yet
    // attack and damage will cover it for now.

    //[Range(4.0f, 252.0f)]
    //public int SpAtk;

    //[Range(4.0f, 252.0f)]
    //public int SpDef;

    [Range(2.0f, 252.0f)]
    public int Speed;

    public Move[] learnedMoves = new Move[4];

    public bool takeDamage(int t_damage)
    {
        HP -= t_damage;
        // Might want to return some kind of way to tell
        // if the mon is "fainted"

        return HP <= 0;
    }
}

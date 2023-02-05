using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public SaplingMonStats[] fightingMon = new SaplingMonStats[2];
    private BattleMenu menu;

    private void Start()
    {
        menu = GetComponent<BattleMenu>();
        menu.AddMon(fightingMon);
    }

    public void TurnController()
    {
        if (!menu.thisPlayersTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2);

        int whichMove = Random.Range(0, 4);
        // A move on the mon is used. The move is chosen by the number passed in (ranging from 1-4)

        //I got the accuracy formula here: https://bulbapedia.bulbagarden.net/wiki/Accuracy#Generation_V_onward

        int randomNumber = Random.Range(1, 101);
        int accuracy = fightingMon[1].learnedMoves[whichMove].accuracy;

        while (menu.displayingMessage)
        {
            yield return new WaitForEndOfFrame();
        }

        if (randomNumber <= accuracy)
        {
            menu.thisPlayersTurn = true;
            bool fainted = fightingMon[0].takeDamage(menu.DamageCalculator(fightingMon[1].learnedMoves[whichMove], 0, 1));

            int faintedNum = fainted ? 1 : 0;

            StartCoroutine(menu.DisplayMessage(fightingMon[1].monName + " uses " + fightingMon[1].learnedMoves[whichMove].moveName + "!", faintedNum));
            
        }

        else
        {
            menu.thisPlayersTurn = true;
            StartCoroutine(menu.DisplayMessage(fightingMon[1].monName + " missed!"));
        }

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleMenu : MonoBehaviour
{
    private GameObject[] moveButtons;
    private GameObject[] battleButtons;
    private GameObject turnText;

    private SaplingMonStats[] fightingMon;

    int attemptsToFlee = 1;

    public void AddMon(SaplingMonStats[] t_mon)
    {
        fightingMon = t_mon;

        for (int i = 0; i < moveButtons.Length; i++)
        {
            if (i != 4)
            {
                moveButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = fightingMon[0].learnedMoves[i].moveName;
            }
        }
    }

    private void Start()
    {
        moveButtons = new GameObject[5];
        battleButtons = new GameObject[4];

        battleButtons[0] = GameObject.Find("Fight Button");
        battleButtons[1] = GameObject.Find("SaplingMon Button");
        battleButtons[2] = GameObject.Find("Bag Button");
        battleButtons[3] = GameObject.Find("Run Button");

        turnText = GameObject.Find("Player Reminder");
        turnText.SetActive(false);

        turnText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;

        int loopOneMore;
        for (int loop = 0; loop < 4; loop++)
        {
            loopOneMore = loop + 1;
            moveButtons[loop] = GameObject.Find("Move " + loopOneMore.ToString() + " Button");
        }

        moveButtons[4] = GameObject.Find("Cancel Move Button");

        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].SetActive(false);
        }
    }

    public void Run()
    {
        // i got this calculation from here: https://bulbapedia.bulbagarden.net/wiki/Escape#Generation_III_and_IV
        // roll die to see if you escape,

        int randomNumber = Random.Range(0, 255);
        float oddsOfEscape = fightingMon[0].Speed * 128;

        oddsOfEscape = Mathf.Abs(oddsOfEscape / fightingMon[1].Speed);

        oddsOfEscape = oddsOfEscape + (30 * attemptsToFlee);

        oddsOfEscape = oddsOfEscape % 256;

        
        if (oddsOfEscape > 255 || randomNumber < oddsOfEscape) // if you pass the roll
        {
            StartCoroutine(DisplayMessage("You got away safely!"));
        }

        else // u failed to escape!
        {
            StartCoroutine(DisplayMessage("You couldn't get away!"));
        }


    }

    public void Fight()
    {
        // turn off the Fight, Run, Items and SaplingMon buttons

        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].SetActive(true);
        }

        for (int i = 0; i < battleButtons.Length; i++)
        {
            battleButtons[i].SetActive(false);
        }
    }

    public void Items()
    {
        // Use/View any items u got here
    }

    public void SaplingMon()
    {
        // View ur mon/swap them out here
    }

    public void Cancel()
    {
        // turn off Move 1-4 buttons
        // turn on Fight, Run, items and saplingMon buttons

        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].SetActive(false);
        }

        for (int i = 0; i < battleButtons.Length; i++)
        {
            battleButtons[i].SetActive(true);
        }
    }

    public void Move(int whichMove)
    {
        // A move on the mon is used. The move is chosen by the number passed in (ranging from 1-4)

        bool fainted = fightingMon[1].takeDamage(DamageCalculator(fightingMon[0].learnedMoves[whichMove], 0, 1));

        StartCoroutine(DisplayMessage(fightingMon[0].monName + " uses " + fightingMon[0].learnedMoves[whichMove].moveName + "!", fainted));
    }

    int DamageCalculator(Move t_moveUsed, int attackingMon, int defendingMon)
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

        return (int)damage;
    }

    public void Toggle(bool t_toggle)
    {
        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].SetActive(false);
        }

        for (int i = 0; i < battleButtons.Length; i++)
        {
            battleButtons[i].SetActive(t_toggle);
        }

        turnText.SetActive(!t_toggle);
    }
        
    IEnumerator DisplayMessage(string t_message, bool t_fainted = false)
    {
        Toggle(false);

        TextMeshProUGUI text = turnText.GetComponent<TextMeshProUGUI>();

        text.text = t_message;

        yield return new WaitForSeconds(1);

        if (t_message == "You got away safely!" || t_message == fightingMon[1].monName + " fainted!")
        {
            SceneManager.LoadScene("Game"); // go back to the main game
        }

        else
        {
            Toggle(true);
        }

        if (t_fainted)
        {
            StartCoroutine(DisplayMessage(fightingMon[1].monName + " fainted!"));
        }

        yield return null;
    }
}

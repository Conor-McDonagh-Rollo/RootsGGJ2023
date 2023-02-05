using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class BattleMenu : MonoBehaviour
{
    private GameObject[] moveButtons;
    private GameObject[] battleButtons;
    private GameObject turnText;
    private RectTransform sideMenu;
    bool sideOpen = false;
    private GameObject[] sideButtons;

    private SaplingMonStats[] fightingMon;
    public MonParty party;

    int attemptsToFlee = 1;

    public bool thisPlayersTurn = true;

    public bool displayingMessage = false;

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
        sideButtons = new GameObject[6];

        battleButtons[0] = GameObject.Find("Fight Button");
        battleButtons[1] = GameObject.Find("SaplingMon Button");
        battleButtons[2] = GameObject.Find("Bag Button");
        battleButtons[3] = GameObject.Find("Run Button");

        turnText = GameObject.Find("Player Reminder");
        turnText.SetActive(false);

        int loopOneMore = 0;
        for (int loop = 0; loop < 4; loop++)
        {
            loopOneMore = loop + 1;
            moveButtons[loop] = GameObject.Find("Move " + loopOneMore.ToString() + " Button");
        }

        loopOneMore = 0;

        for (int loop = 0; loop < 6; loop++)
        {
            loopOneMore = loop + 1;
            sideButtons[loop] = GameObject.Find("Option " + loopOneMore.ToString());
        }

        moveButtons[4] = GameObject.Find("Cancel Move Button");

        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].SetActive(false);
        }

        party = GetComponent<MonParty>();

        foreach (SaplingMonStats mon in party.party)
        {
            mon.setFacingDirection(false);
        }

        sideMenu = GameObject.Find("Side Menu Background").GetComponent<RectTransform>();
        sideMenu.gameObject.SetActive(false);
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
            attemptsToFlee++;
        }

        thisPlayersTurn = false;
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
        //// Use/View any items u got here

        //if (sideMenu.localPosition.x <= -600)
        //{
        //    sideMenu.position += new Vector3(sideMenu.sizeDelta.x, 0, 0);
        //}

        //else
        //{
        //    sideMenu.position -= new Vector3(sideMenu.sizeDelta.x, 0, 0);
        //}
    }

    public void SaplingMon()
    {
        // View ur mon/swap them out here
        if (sideOpen)
        {
            //sideMenu.position -= new Vector3(sideMenu.sizeDelta.x, 0, 0);
            sideMenu.gameObject.SetActive(false);
        }

        else
        {
            sideMenu.gameObject.SetActive(true);
        }

        sideOpen = !sideOpen;

        for (int i = 0; i < 6; i++)
        {
            if (party.party.Count - 1 < i)
            {
                sideButtons[i].transform.Find("Image").gameObject.SetActive(false);
                sideButtons[i].transform.Find("Text").gameObject.SetActive(false);
                continue;
            }
            sideButtons[i].transform.Find("Image").GetComponent<Image>().sprite = party.party[i].front;
            sideButtons[i].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = party.party[i].monName;

            if (party.party[i].HP <= 0)
            {
                sideButtons[i].transform.Find("Image").GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 1);
            }

            else
            {
                sideButtons[i].transform.Find("Image").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void ChangeMon(int t_mon)
    {
        if (t_mon < party.party.Count && party.party[t_mon].HP > 0)
        {
            fightingMon[0].gameObject.SetActive(false);
            fightingMon[0] = party.party[t_mon];
            fightingMon[0].gameObject.SetActive(true);

            for (int i = 0; i < moveButtons.Length; i++)
            {
                if (i != 4)
                {
                    moveButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = fightingMon[0].learnedMoves[i].moveName;
                }
            }

            thisPlayersTurn = false;

            sideMenu.gameObject.SetActive(false);

            Toggle(false);

            sideOpen = !sideOpen;
        }
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

        //I got the accuracy formula here: https://bulbapedia.bulbagarden.net/wiki/Accuracy#Generation_V_onward

        int randomNumber = Random.Range(1, 101);
        int accuracy = fightingMon[0].learnedMoves[whichMove].accuracy;
        thisPlayersTurn = false;

        if (randomNumber <= accuracy)
        {
            bool fainted = fightingMon[1].takeDamage(DamageCalculator(fightingMon[0].learnedMoves[whichMove], 0, 1));

            int faintedNum = fainted ? 2 : 0;

            StartCoroutine(DisplayMessage(fightingMon[0].monName + " uses " + fightingMon[0].learnedMoves[whichMove].moveName + "!", faintedNum));
        }   
        
        else
        {
            StartCoroutine(DisplayMessage(fightingMon[0].monName + " missed!"));
        }
    }

    public int DamageCalculator(Move t_moveUsed, int attackingMon, int defendingMon)
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
        
    public IEnumerator DisplayMessage(string t_message, int t_fainted = 0)
    {
        displayingMessage = true;
        Toggle(false);

        TextMeshProUGUI text = turnText.GetComponent<TextMeshProUGUI>();

        text.text = t_message;

        yield return new WaitForSeconds(2);

        if (t_message == "You got away safely!" || t_message == fightingMon[1].monName + " fainted!")
        {
            SceneManager.LoadScene("Game"); // go back to the main game
        }

        if (t_message == fightingMon[0].monName + " fainted!")
        {
            bool alive = false;

            foreach (SaplingMonStats mon in party.party)
            {
                if (mon.HP > 0)
                {
                    alive = true;
                    break;
                }
            }

            if (alive)
            {
                SaplingMon();
            }
            
            else
            {
                SceneManager.LoadScene("Game"); // go back to the main game
            }
        }

        if (t_fainted != 0)
        {
            fightingMon[t_fainted - 1].gameObject.SetActive(false);
            StartCoroutine(DisplayMessage(fightingMon[t_fainted - 1].monName + " fainted!"));
        }

        displayingMessage = false;

        if (thisPlayersTurn && fightingMon[0].HP > 0)
        {
            Toggle(true);
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleMenu : MonoBehaviour
{
    private GameObject[] moveButtons;
    private GameObject[] battleButtons;

    private void Start()
    {
        moveButtons = new GameObject[5];
        battleButtons = new GameObject[4];

        battleButtons[0] = GameObject.Find("Fight Button");
        battleButtons[1] = GameObject.Find("SaplingMon Button");
        battleButtons[2] = GameObject.Find("Bag Button");
        battleButtons[3] = GameObject.Find("Run Button");


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
        // roll die to see if you escape,
        
        if (true) // if you pass the roll
        {
            SceneManager.LoadScene("Game"); // go back to the main game
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
    }
}

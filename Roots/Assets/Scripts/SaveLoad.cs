using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    public void Save()
    {
        // PLAYER SAVING
        PlayerPrefs.SetFloat("Player_X", player.position.x);
        PlayerPrefs.SetFloat("Player_Y", player.position.y);
    }

    public void Load()
    {
        // IS PLAYERPREF DATA OK?
        if (!PlayerPrefs.HasKey("Player_X"))
            return;

        // PLAYER LOADING
        player.position = new Vector3(PlayerPrefs.GetFloat("Player_X"), PlayerPrefs.GetFloat("Player_Y"), 0.0f);
    }
}

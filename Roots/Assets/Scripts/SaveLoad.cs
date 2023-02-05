using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    Transform player;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;
        Load();
    }

    public void Save()
    {
        // PLAYER SAVING
        PlayerPrefs.SetFloat("Player_X", player.position.x);
        PlayerPrefs.SetFloat("Player_Y", player.position.y);
        // CAMERA SAVING
        PlayerPrefs.SetFloat("Camera_X", cam.position.x);
        PlayerPrefs.SetFloat("Camera_Y", cam.position.y);
    }

    public void Load()
    {
        // IS PLAYERPREF DATA OK?
        if (!PlayerPrefs.HasKey("Player_X"))
            return;

        // PLAYER LOADING
        player.position = new Vector3(PlayerPrefs.GetFloat("Player_X"), PlayerPrefs.GetFloat("Player_Y"), 0.0f);
        // CAMERA LOADING
        cam.position = new Vector3(PlayerPrefs.GetFloat("Camera_X"), PlayerPrefs.GetFloat("Camera_Y"), -10.0f);
    }
}

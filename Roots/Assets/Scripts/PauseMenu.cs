using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject pause;

    private void Start()
    {
        pause = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pause.SetActive(!pause.activeSelf);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}

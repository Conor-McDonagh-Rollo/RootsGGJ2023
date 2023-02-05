using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] public TMP_Text volumeTextValue = null;
    [SerializeField] public Slider volumeSlider = null;

    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float vol = PlayerPrefs.GetFloat("masterVolume");
            AudioListener.volume = vol;
            vol *= 100;
            volumeTextValue.text = vol.ToString("0.0");
            
            volumeSlider.value = vol;
        }
        else
        {
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        }
        Camera.main.GetComponent<AudioSource>().Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        volume /= 100.0f;
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }
}

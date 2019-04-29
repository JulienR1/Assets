using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;
    public GameObject beforePlayHolder;
    public GameObject creditsHolder;
    public GameObject deadMenuHolder;
    public GameObject pauseMenuHolder;

    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public int[] screenWidths;
    public Toggle fullScreenToggle;
    int activeScreenResIndex;


    public void Start()
    {

        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullScreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;
        /*
        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;
        */
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }

        fullScreenToggle.isOn = isFullScreen;

        if (FamePoints.mort == true)
        {
            DeadMenu();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void ResetAll()
    {
        mainMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(false);
        creditsHolder.SetActive(false);
        beforePlayHolder.SetActive(false);
        deadMenuHolder.SetActive(false);
    }

    public void LeaveGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuManager", LoadSceneMode.Single);
        
    }

    public void Pause()
    {    
        pauseMenuHolder.SetActive(!pauseMenuHolder.activeSelf);
        Time.timeScale = 1 - Time.timeScale;
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
         
    }

    public void Play()
    {
        SceneManager.LoadScene("Main");
        FamePoints.mort = false;
    }

    public void Leave()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        ResetAll();
        mainMenuHolder.SetActive(true);
       
    }

    public void OptionsMenu(){
        ResetAll();
        optionsMenuHolder.SetActive(true);
    }

    public void BeforePlay()
    {
        ResetAll();
        beforePlayHolder.SetActive(true);
    }

    public void CreditsMenu()
    {
        ResetAll();
        creditsHolder.SetActive(true);
    }

    public void DeadMenu()
    {
        ResetAll();
        deadMenuHolder.SetActive(true);
    }

    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetFloat("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }

    }

    public void SetFullScreen(bool isFullScreen)
    {
        for(int i=0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullScreen;
        }
        if (isFullScreen)
        {
            Resolution[] allResolution = Screen.resolutions;
            Resolution maxResolution = allResolution[allResolution.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }

        PlayerPrefs.SetInt("fullscreen", ((isFullScreen) ? 1 : 0));
        PlayerPrefs.Save();

    }
    
   /* public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
    */
}

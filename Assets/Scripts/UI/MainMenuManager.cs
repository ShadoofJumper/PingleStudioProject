using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject layoutPanel;
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Slider sliderFXVolume;

    private string sceneOnPlayName = "Game";
    private string sceneOnReturnName = "MainMenu";

    static private float volumePower;
    static private float volumeFXPower;

    private void Start()
    {
        LoadSettings();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneOnPlayName);
    }
    
    public void ExitGameToMenu()
    {
        SceneManager.LoadScene(sceneOnReturnName);
    }
    
    private void LoadSettings()
    {
        volumePower = PlayerPrefs.GetFloat("VolumeMusic", 1);
        volumeFXPower = PlayerPrefs.GetFloat("VolumeFX", 1);
        if(sliderVolume!=null)
            sliderVolume.value = volumePower;
        if(sliderFXVolume!=null)
            sliderFXVolume.value = volumeFXPower;
    }


    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        volumePower = 1;
        volumeFXPower = 1;
        sliderVolume.value = 1;
        sliderFXVolume.value = 1;
    }
    
    public void TurnSettings()
    {
        settingsPanel.SetActive(true);
    }
    
    public void TurnLayout(bool value)
    {
        layoutPanel.SetActive(value);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    
    public void BackToMenu()
    {
        settingsPanel.SetActive(false);

        PlayerPrefs.SetFloat("VolumeMusic", volumePower);
        PlayerPrefs.SetFloat("VolumeFX", volumeFXPower);
    }
    
    public void SliderVolumeChange()
    {
        volumePower = sliderVolume.value;
    }
    public void SliderVolumeVFXChange()
    {
        volumeFXPower = sliderFXVolume.value;
    }
    
}

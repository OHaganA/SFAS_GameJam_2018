using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{

    public Slider volSlider;
    public LevelManager levelManager;
    public Dropdown resDropdown, qualDropdown, aaDropdown;
    public Toggle fullscreen, vsync;
    public Resolution[] resolutions;
    public GameSettings gameSettings;
    public Button applyButton;
    private MusicPlayer musicPlayer;
    private void OnEnable()
    {
        gameSettings = new GameSettings();
        resolutions = Screen.resolutions;
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();

        fullscreen.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        vsync.onValueChanged.AddListener(delegate { OnVSyncToggle(); });
        resDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        qualDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        aaDropdown.onValueChanged.AddListener(delegate { OnAAChange(); });
        volSlider.onValueChanged.AddListener(delegate { OnMusicVolChange(); });
        applyButton.onClick.AddListener(delegate { ApplySettings(); });

        foreach (Resolution resolution in resolutions)
        {
            resDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }

    public void OnFullScreenToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreen.isOn;
    }

    public void OnVSyncToggle()
    {
        if (vsync.isOn)
        {
            QualitySettings.vSyncCount = 2;
            gameSettings.vsync = true;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            gameSettings.vsync = false;
        }
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resDropdown.value].width,resolutions[resDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resDropdown.value;
    }

    public void OnTextureQualityChange()
    {
        gameSettings.texQuality = QualitySettings.masterTextureLimit = qualDropdown.value;

    }

    public void OnAAChange()
    {
       QualitySettings.antiAliasing = (int) Mathf.Pow(2f, aaDropdown.value);
       gameSettings.antiAliasing = aaDropdown.value;
    }

    public void OnMusicVolChange()
    {
        gameSettings.musicVolume = volSlider.value;
        musicPlayer.changeVolume(volSlider.value / 2);
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);

    }

    public void LoadSettings()
    {
        File.ReadAllText(Application.persistentDataPath + "/gamesettings.json");
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        volSlider.value = gameSettings.musicVolume;

        aaDropdown.value = gameSettings.antiAliasing;

        resDropdown.value = gameSettings.resolutionIndex;

        fullscreen.isOn = gameSettings.fullscreen;

        Screen.fullScreen = gameSettings.fullscreen;
        vsync.isOn = gameSettings.fullscreen;

        resDropdown.RefreshShownValue();
    }

    public void ApplySettings()
    {
        SaveSettings();
    }
}

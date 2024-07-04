                                                                                                                                                                    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    [Header("ScreenSettings")]
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Toggle vsyncToggle;
    [SerializeField] TMP_InputField ResWidthInput;
    [SerializeField] TMP_InputField ResHeightInput;
    [SerializeField] TMP_Text ResWidthPlaceholder;
    [SerializeField] TMP_Text ResHeightPlaceholder;
    [Header("GraphicSettings")]
    [SerializeField] TMP_Dropdown qualityDropdown;
    [Header("VolumeSettings")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer masterMixer;

    

    [Header("FpsSettings")]
    [SerializeField] TMP_InputField FpsMaxInput;
    [SerializeField] TMP_Text FPSMaxPlaceholder;
    
    [Header("CrosshairSettings")]
    [SerializeField] TMP_InputField CrosshairGapSetting;
    [SerializeField] TMP_InputField CrosshairHeightSetting;
    [SerializeField] TMP_InputField CrosshairWidthSetting;
    [SerializeField] Slider CrosshairRSettingSlider;
    [SerializeField] Slider CrosshairGSettingSlider;
    [SerializeField] Slider CrosshairBSettingSlider;

    [SerializeField] TMP_Text CrosshairGapSettingPlaceholder;
    [SerializeField] TMP_Text CrosshairHeightSettingPlaceholder;
    [SerializeField] TMP_Text CrosshairWidthSettingPlaceholder;


    int resWidth, resHeight, fpsMax, CG, CH, CW;

    
    public void Start()
    {

        ResHeightPlaceholder.text = PlayerPrefs.GetInt("ResolutionHeight").ToString();
        ResWidthPlaceholder.text = PlayerPrefs.GetInt("ResolutionWidth").ToString();
        FPSMaxPlaceholder.text = PlayerPrefs.GetInt("FPSMax").ToString();

        CrosshairGapSettingPlaceholder.text = PlayerPrefs.GetInt("CrosshairGap").ToString();
        CrosshairHeightSettingPlaceholder.text = PlayerPrefs.GetInt("CrosshairHeight").ToString();
        CrosshairWidthSettingPlaceholder.text = PlayerPrefs.GetInt("CrosshairWidth").ToString();

        CrosshairRSettingSlider.value = PlayerPrefs.GetFloat("CrosshairRed");
        CrosshairGSettingSlider.value = PlayerPrefs.GetFloat("CrosshairGreen");
        CrosshairBSettingSlider.value = PlayerPrefs.GetFloat("CrosshairBlue");



        if(PlayerPrefs.HasKey("QualityLevel"))
        {
            qualityDropdown.value = PlayerPrefs.GetInt("QualityLevel");
        }

        if(PlayerPrefs.HasKey("VolumeMasterValue"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("VolumeMasterValue");
        }

    }
    

    public void SetResolution()
    {
        string tmpW = ResWidthInput.GetComponent<TMP_InputField>().text;
        string tmpH = ResHeightInput.GetComponent<TMP_InputField>().text;

        if(tmpW == "" | tmpH == "")
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), Screen.fullScreen);
        }
        else
        {
            resWidth = int.Parse(tmpW);
            resHeight = int.Parse(tmpH);
            PlayerPrefs.SetInt("ResolutionWidth", resWidth);
            PlayerPrefs.SetInt("ResolutionHeight", resHeight);
            Screen.SetResolution(resWidth, resHeight, Screen.fullScreen);
        }

        
    }

    public void SetVsync()
    {
        if(vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("Vsync", 1);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("Vsync", 0);
        }
    }

    public void SetFPSMax()
    {
        string tmpF = FpsMaxInput.GetComponent<TMP_InputField>().text;

        if(tmpF == "")
        {
            Application.targetFrameRate = PlayerPrefs.GetInt("FPSMax");
        }
        else
        {
            fpsMax = int.Parse(tmpF);
            Application.targetFrameRate = fpsMax;
            PlayerPrefs.SetInt("FPSMax", fpsMax);
        }
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
        PlayerPrefs.SetInt("QualityLevel", qualityDropdown.value);


    }

    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnVolumeSliderValueChanged()
    {
        masterMixer.SetFloat("volumeMaster", volumeSlider.value);
        PlayerPrefs.SetFloat("VolumeMasterValue", volumeSlider.value);
    }

    public void SetCrosshairSettings()
    {
        string tmpCG = CrosshairGapSetting.GetComponent<TMP_InputField>().text;
        string tmpCH = CrosshairHeightSetting.GetComponent<TMP_InputField>().text;
        string tmpCW = CrosshairWidthSetting.GetComponent<TMP_InputField>().text;
        
        float tmpCR = CrosshairRSettingSlider.value;
        float tmpCGN = CrosshairGSettingSlider.value;
        float tmpCB = CrosshairBSettingSlider.value;
        
        //properties

        if(tmpCG == "")
        {
            PlayerPrefs.SetInt("CrosshairGap", PlayerPrefs.GetInt("CrosshairGap"));
        }
        else
        {
            CG = int.Parse(tmpCG);
            PlayerPrefs.SetInt("CrosshairGap", CG);
        }

        if(tmpCH == "")
        {
            PlayerPrefs.SetInt("CrosshairHeight", PlayerPrefs.GetInt("CrosshairHeight"));
        }
        else
        {
            CH = int.Parse(tmpCH);
            PlayerPrefs.SetInt("CrosshairHeight", CH);
        }  

        if(tmpCW == "")
        {
            PlayerPrefs.SetInt("CrosshairWidth", PlayerPrefs.GetInt("CrosshairWidth"));
        }
        else
        {
            CW = int.Parse(tmpCW);
            PlayerPrefs.SetInt("CrosshairWidth", CW);
        }

        //colors


        
        PlayerPrefs.SetFloat("CrosshairRed", tmpCR);
        PlayerPrefs.SetFloat("CrosshairGreen", tmpCGN);
        PlayerPrefs.SetFloat("CrosshairBlue", tmpCB);


    }


    public void check()
    {
        Debug.Log(PlayerPrefs.GetInt("QualityLevel"));
        Debug.Log(PlayerPrefs.GetInt("Vsync"));
        Debug.Log(PlayerPrefs.GetInt("FPSMax"));
        Debug.Log(PlayerPrefs.GetInt("ResolutionWidth"));
        Debug.Log(PlayerPrefs.GetInt("ResolutionHeight"));
        Debug.Log(PlayerPrefs.GetFloat("VolumeMasterValue"));
        Debug.Log(PlayerPrefs.GetInt("CrosshairGap"));
        Debug.Log(PlayerPrefs.GetInt("CrosshairHeight"));
        Debug.Log(PlayerPrefs.GetInt("CrosshairWitdh"));
        Debug.Log(PlayerPrefs.GetFloat("CrosshairRed"));
        Debug.Log(PlayerPrefs.GetFloat("CrosshairGreen"));
        Debug.Log(PlayerPrefs.GetFloat("CrosshairBlue"));

    }


}

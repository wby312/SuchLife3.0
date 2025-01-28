using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


//Code tutorial Re hope Games and Brackeys for this one

namespace SuchLife_UI
{
    public class ResolutionSettingsController : MonoBehaviour
    {

        [SerializeField]
        TMP_Dropdown resolutionDropdown;

        Resolution[] screenResolutions;

        const String ScreenResolutionAmountSave = "amountOfScreenResolutions";
        const String ScreenResolutionIndexSave = "screenResolutionIndex";

        void OnEnable()
        {
            resolutionDropdown.onValueChanged.AddListener(setResolution);
        }

        void OnDisable()
        {
            resolutionDropdown.onValueChanged.RemoveListener(setResolution);
        }

        void Start()
        {
            int currentResolution = 0;
            List<string> formatedResolutions = new List<string>();

            screenResolutions = Screen.resolutions;
            
            resolutionDropdown.ClearOptions();


            for (int i = 0; i < screenResolutions.Length; i++) 
            {
                string option = $"{screenResolutions[i].height} x {screenResolutions[i].width} @ {screenResolutions[i].refreshRateRatio.value:0.00}hz";
                
                
                formatedResolutions.Add(option);

                if (screenResolutions[i].height == Screen.height && screenResolutions[i].width == Screen.width) {
                    currentResolution = i;
                }
            }

            resolutionDropdown.AddOptions(formatedResolutions);
            //Want to check if it has same key amount because otherwise the monitor
            //that the game is on might not support the current monitors
            //TODO: Detect when monitors is changed and switch this over...
            if (PlayerPrefs.HasKey(ScreenResolutionAmountSave) && PlayerPrefs.GetInt(ScreenResolutionAmountSave) == screenResolutions.Length)
            {
                currentResolution = PlayerPrefs.GetInt(ScreenResolutionIndexSave);
                resolutionDropdown.value = currentResolution;
            }
            else
            {
                resolutionDropdown.value = currentResolution;
                PlayerPrefs.SetInt(ScreenResolutionAmountSave, screenResolutions.Length);
            }


            setResolution(currentResolution);
            resolutionDropdown.RefreshShownValue();
        }

        void setResolution(int resolutionIndex)
        {
            Resolution resolution = screenResolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            Application.targetFrameRate = ((int)resolution.refreshRateRatio.value);
            PlayerPrefs.SetInt(ScreenResolutionIndexSave, resolutionIndex);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System;

namespace SuchLife_UI
{
    //Code tutorial Brackeys used for this one

    public class FullscreenSettingsController : MonoBehaviour
    {
        [SerializeField]
        Toggle fullScreenToggle;

        const String FullScreenSave = "fullScreen";

        void OnEnable()
        {
            fullScreenToggle.onValueChanged.AddListener(setFullscreen);
        }

        void OnDisable()
        {
            fullScreenToggle.onValueChanged.RemoveListener(setFullscreen);
        }

        void Start()
        {
            if (PlayerPrefs.HasKey(FullScreenSave))
            {
                loadFullscreen();
            }
            else
            {
                bool startingIsOnVal = fullScreenToggle.isOn;
                setFullscreen(startingIsOnVal);
            }
        }

        void setFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
            PlayerPrefs.SetInt(FullScreenSave, Convert.ToInt32(isFullscreen));
        }

        void loadFullscreen()
        {
            int fullScreen = PlayerPrefs.GetInt(FullScreenSave);
            bool fullScreenBool = Convert.ToBoolean(fullScreen);
            fullScreenToggle.isOn = fullScreenBool;
            setFullscreen(fullScreenBool);  //We though line above will trigger isOn because of the bind if its
                                            //a different val it doesn't count as a change so keep this explicit
                                            //call to ensure its properly changed
        }


    }
}

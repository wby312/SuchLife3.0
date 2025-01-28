using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using System;


//Code tutorials provided by Dapper Dino (Key Rebinding), Brackeys (Screen Resolution and Fullscreen), 

namespace SuchLife_UI
{
    public class VolumeSettingsController : MonoBehaviour
    {
        [SerializeField]
        SliderDataPair[] sliderDataPairs;

        [SerializeField]
        GameObject[] musicViewObjs;

        [SerializeField]
        AudioMixer audioMixer;

        //Unity cant properly serialize Interfaces I guess hence why unserialized field
        ISetVal musicView;

        //Dont like this but Start gets called after OnEnable so we have to do stuff
        //here unfortunatley
        void Awake()
        {
            for (int i = 0; i < sliderDataPairs.Length; i++)
            {
                var pair = sliderDataPairs[i];
                pair.view = musicViewObjs[i].GetComponent<ISetVal>();
                pair.audioMixer = audioMixer;
            }
        }

        void OnEnable()
        {
            foreach (var sliderDataPair in sliderDataPairs)
            {
                sliderDataPair.model.onValueChanged.AddListener(sliderDataPair.setSliderVal);
            }
        }

        void OnDisable()
        {
            foreach (var sliderDataPair in sliderDataPairs)
            {
                sliderDataPair.model.onValueChanged.RemoveListener(sliderDataPair.setSliderVal);
            }
        }

        void Start()
        {
            //Basic check if they don't already have PlayerPrefs make em otherwise load em
            SliderDataPair sliderDataPair = sliderDataPairs[0];
            if (PlayerPrefs.HasKey(sliderDataPair.mixerToSet))
            {
                sliderDataPair.loadSliderVal();
            }
            else
            {
                sliderDataPair.setSliderVal(sliderDataPair.model.value);
            }

        }
    }

    [System.Serializable]
    class SliderDataPair
    {
        //Cant use getter and setter because Serializable system can't recognise it
        public Slider model;
        public ISetVal view;

        [NonSerialized]
        public AudioMixer audioMixer;

        public string mixerToSet;

        public void setSliderVal(float val)
        {
            view.SetValue(MathHelper.ToIntPercent(val));
            audioMixer.SetFloat(mixerToSet, MathHelper.FloatToAudioLog(val));
            PlayerPrefs.SetFloat(mixerToSet, val);
        }

        public void loadSliderVal()
        {
            float val = PlayerPrefs.GetFloat(mixerToSet);
            model.value = val;
            setSliderVal(val);

        }
    }
}

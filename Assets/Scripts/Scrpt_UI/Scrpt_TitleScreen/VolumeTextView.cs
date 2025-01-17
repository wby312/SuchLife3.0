using UnityEngine;
using TMPro;

public class VolumeTextView : MonoBehaviour, ISetVal
{
    [SerializeField]
    string prefixString = "Music : ";

    [SerializeField]
    string suffixString = "%";


    TextMeshProUGUI mProUGUI;

    void Start()
    {
        mProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetValue(int value)
    {
        mProUGUI.text = prefixString + value.ToString() + suffixString;
    }
}

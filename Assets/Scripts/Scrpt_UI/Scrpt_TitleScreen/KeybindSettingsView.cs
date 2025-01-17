using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SuchLife_UI
{
    public class KeybindSettingsView : MonoBehaviour
    {
        TextMeshProUGUI mProUGUI;

        void Start()
        {
            mProUGUI = transform.Find("KeyText").GetComponent<TextMeshProUGUI>();
        }


        public void SetText(string newText)
        {
            mProUGUI.text = newText;
        }
    }
}

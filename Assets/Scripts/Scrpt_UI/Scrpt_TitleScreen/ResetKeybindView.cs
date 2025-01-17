using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SuchLife_UI
{
    public class ResetKeybindView : MonoBehaviour
    {
        [SerializeField]
        Color alreadyResetColor;
        [SerializeField]
        Color resetableColor;

        Image imageIsResetable;

        private void Start()
        {
            imageIsResetable = transform.Find("IsAlreadyDefault").GetComponent<Image>();
        }

        public void SetRestButtonColor(bool isBaseValue = false)
        {
            imageIsResetable.color = isBaseValue ? alreadyResetColor : resetableColor;
        }
    }
}

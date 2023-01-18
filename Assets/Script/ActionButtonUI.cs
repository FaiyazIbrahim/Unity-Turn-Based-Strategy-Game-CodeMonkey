using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Script
{
    public class ActionButtonUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }


        public void SetBaseAction(BaseAction baseAction)
        {
            _text.text = baseAction.GetActionName();
        }
    }
}


using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Script
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private Image m_BorderImage;
        private TextMeshProUGUI _text;
        private Button _button;

        private BaseAction _baseAction;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }


        public void SetBaseAction(BaseAction baseAction)
        {
            _baseAction = baseAction;
            _text.text = baseAction.GetActionName();

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
            });
        }


        public void UpdateSelectedButtonVisual()
        {
            BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
             
            m_BorderImage.gameObject.SetActive(selectedBaseAction == _baseAction);
        }

    }
}


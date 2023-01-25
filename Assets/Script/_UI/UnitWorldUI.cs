using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Script
{
    public class UnitWorldUI : MonoBehaviour
    {
        [SerializeField] private Image m_HealthBarFulledImage;
        [SerializeField] private TextMeshProUGUI m_ActionPointText;
        private Unit _unit;
        private HealthSystem _healthSystem;


        private void Awake()
        {
            _healthSystem = GetComponentInParent<HealthSystem>();
            _unit = GetComponentInParent<Unit>();
        }

        private void Start()
        {
            Unit.OnAnyActionPointsChanged += UpdateActionPointsText;
            UpdateActionPointsText();

            _healthSystem.OnDamage += UpdateHealthBar;
        }

        private void UpdateActionPointsText()
        {
            m_ActionPointText.text = _unit.GetActionPoints().ToString();
        }

        private void UpdateHealthBar(float value)
        {
            m_HealthBarFulledImage.fillAmount = value;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Script
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform m_ActionButton;
        [SerializeField] private Transform m_ActionButtonContainer;

        private void Start()
        {
            UnitActionSystem.Instance.onSelectedUnitChanged += OnSelectedUnitChanged;
            CreateActionButton();
        }


        public void OnSelectedUnitChanged()
        {
            CreateActionButton();
        }


        public void CreateActionButton()
        {
            foreach(Transform buttonTransform in m_ActionButtonContainer)
            {
                Destroy(buttonTransform.gameObject);
            }

            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

            foreach(BaseAction baseAction in selectedUnit.GetBaseActionsArray())
            {
                Transform actionButtonTransform =  Instantiate(m_ActionButton, m_ActionButtonContainer);
                ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(baseAction);
            }
        }
    }
}


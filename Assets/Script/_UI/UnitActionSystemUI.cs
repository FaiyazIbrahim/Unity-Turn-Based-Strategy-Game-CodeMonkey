using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Script
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform m_ActionButton;
        [SerializeField] private Transform m_ActionButtonContainer;
        [SerializeField] private TextMeshProUGUI m_ActionpointsText;

        private List<ActionButtonUI> actionButtonUIList;


        private void Awake()
        {
            actionButtonUIList = new List<ActionButtonUI>();
        }

        private void Start()
        {
            UnitActionSystem.Instance.onSelectedUnitChanged += OnSelectedUnitChanged;
            UnitActionSystem.Instance.onSelectedActionChanged += OnSelectedActionChanged;
            UnitActionSystem.Instance.onActionStarted += UpdateActionPoints;
            Unit.OnAnyActionPointsChanged += AnyTurnChanged;  

            CreateActionButton();
            UpdateSelectedVisual();
            UpdateActionPoints();

            TurnSystem.Instance.OnTurnChange += TurnOnChanged;
        }


        public void OnSelectedUnitChanged()
        {
            CreateActionButton();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }



        private void OnSelectedActionChanged()
        {
            UpdateSelectedVisual();
            UpdateActionPoints();
        }


        public void CreateActionButton()
        {
            foreach(Transform buttonTransform in m_ActionButtonContainer)
            {
                Destroy(buttonTransform.gameObject);
            }

            actionButtonUIList.Clear();

            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

            foreach(BaseAction baseAction in selectedUnit.GetBaseActionsArray())
            {
                Transform actionButtonTransform =  Instantiate(m_ActionButton, m_ActionButtonContainer);
                ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(baseAction);

                actionButtonUIList.Add(actionButtonUI);
            }
        }


        public void UpdateSelectedVisual()
        {
            foreach(ActionButtonUI actionButtonUI in actionButtonUIList)
            {
                actionButtonUI.UpdateSelectedButtonVisual();
            }
        }

        private void UpdateActionPoints()
        {
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            m_ActionpointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
        }

        private void TurnOnChanged()
        {
            UpdateActionPoints();
        }

        private void AnyTurnChanged()
        {
            UpdateActionPoints();
        }
    }
}


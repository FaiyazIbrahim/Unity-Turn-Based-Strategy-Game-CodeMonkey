using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance;
        [SerializeField] private Unit m_Unit;
        [SerializeField] private LayerMask m_UnitLayer;


        private bool _isbusy;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (_isbusy) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (TryHandleUnitSelection()) return;

                GridPosition mouseGridposition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetHitPoint());

                if (m_Unit.GetMoveAction().IsValidActionGridPosition(mouseGridposition))
                {
                    m_Unit.GetMoveAction().Move(mouseGridposition, SetAsFree);
                }
                SetAsBusy();
            }

            if(Input.GetMouseButtonDown(1))
            {
                m_Unit.GetSpinAction().Spin(SetAsFree);
                SetAsBusy();
            }

        }

        private bool TryHandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, m_UnitLayer))
            {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    m_Unit = unit;
                    return true;
                }
            }

            return false;
        }

        public Unit GetSelectedUnit()
        {
            return m_Unit;
        }

        public void SetAsBusy()
        {
            _isbusy = true;
        }

        public void SetAsFree()
        {
            _isbusy = false;
        }
    }

}


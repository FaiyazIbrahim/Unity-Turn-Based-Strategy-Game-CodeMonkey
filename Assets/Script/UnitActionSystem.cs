using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script
{
    public class UnitActionSystem : MonoBehaviour
    {
        public event Action onSelectedUnitChanged;
        public event Action onSelectedActionChanged;
        public event Action<bool> onBusyChanged;
        public event Action onActionStarted;


        public static UnitActionSystem Instance;
        [SerializeField] private Unit m_Unit;
        [SerializeField] private LayerMask m_UnitLayer;

        private BaseAction _selectedBaseAction;
        private bool _isbusy;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SetSelectedUnit(m_Unit);
        }

        private void Update()
        {
            
            if (_isbusy) return;
            if (!TurnSystem.Instance.IsPlayerTurn()) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (TryHandleUnitSelection()) return;
            HandleSelectedAction();

        }

        private bool TryHandleUnitSelection()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, m_UnitLayer))
                {
                    if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                    {
                        if (m_Unit == unit) return false;

                        if (unit.IsEnemy()) return false;
                        SetSelectedUnit(unit);
                        return true;
                    }
                }
            }

            return false;
        }

        private void HandleSelectedAction()
        {
            if(Input.GetMouseButtonDown(0))
            {
                GridPosition mouseGridposition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetHitPoint());

                if (!_selectedBaseAction.IsValidActionGridPosition(mouseGridposition)) return;
                if (!m_Unit.TrySpendActionPointsToTakeAction(_selectedBaseAction)) return;

                SetAsBusy();
                _selectedBaseAction.TakeAction(mouseGridposition, SetAsFree);

                onActionStarted?.Invoke();

                //switch (_selectedBaseAction)
                //{
                //    case MoveAction moveAction:
                //        if (moveAction.IsValidActionGridPosition(mouseGridposition))
                //        {
                //            SetAsBusy();
                //            moveAction.Move(mouseGridposition, SetAsFree);
                //        }
                //        break;
                //    case SpinAction spinAction:

                //        SetAsBusy();
                //        spinAction.Spin(SetAsFree);

                //        break;
                //}
            }
        }

        private void SetSelectedUnit(Unit unit)
        {
            m_Unit = unit;
            SetSelectedAction(unit.GetMoveAction());
            onSelectedUnitChanged?.Invoke();
        }

        public void SetSelectedAction(BaseAction baseAction)
        {
            
            _selectedBaseAction = baseAction;
            
            onSelectedActionChanged?.Invoke();
        }

        public Unit GetSelectedUnit()
        {
            return m_Unit;
        }

        public void SetAsBusy()
        {
            _isbusy = true;
            onBusyChanged?.Invoke(_isbusy);
        }

        public void SetAsFree()
        {
            _isbusy = false;
            onBusyChanged?.Invoke(_isbusy);
        }

        public BaseAction GetSelectedAction()
        {
            return _selectedBaseAction; 
        }
    }

}


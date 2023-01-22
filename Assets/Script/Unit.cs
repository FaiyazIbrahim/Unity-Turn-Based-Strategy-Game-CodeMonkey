using System;
using UnityEngine;

namespace Script
{
    public class Unit : MonoBehaviour
    {

        [SerializeField] private bool m_IsEnemy;

        private const int ACTION_POINTS_MAX = 2;

        private MoveAction _MoveAction;
        private SpinAction _spinAction;
        private BaseAction[] _baseActionArray;
        
        private GridPosition gridPosition;

        private int ActionPoints = ACTION_POINTS_MAX;

        public static event Action OnAnyActionPointsChanged;

        private void Awake()
        {
            _spinAction = GetComponent<SpinAction>();
            _MoveAction = GetComponent<MoveAction>();
            _baseActionArray = GetComponents<BaseAction>();
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

            TurnSystem.Instance.OnTurnChange += OnTurnChanged;
        }

        private void Update()
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if(newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedToGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }

        }

        public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
        {
            if(CanSpendActionPointsToTakeAction(baseAction))
            {
                SpendActionPoints(baseAction.GetActionPointCost());
                return true;
            }
            else
            {
                return false;
            }
        }

        public MoveAction GetMoveAction()
        {
            return _MoveAction;
        }


        public SpinAction GetSpinAction()
        {
            return _spinAction;
        }

        public GridPosition GetGridPosition()
        {
            return gridPosition;
        }

        public BaseAction[] GetBaseActionsArray()
        {
            return _baseActionArray;
        }

        public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
        {
            return ActionPoints >= baseAction.GetActionPointCost();
        }

        private void SpendActionPoints(int value)
        {
            ActionPoints -= value;

            OnAnyActionPointsChanged?.Invoke();
        }

        public int GetActionPoints()
        {
            return ActionPoints;
        }

        private void OnTurnChanged()
        {
            if(IsEnemy() && !TurnSystem.Instance.IsPlayerTurn() ||
                !IsEnemy() && TurnSystem.Instance.IsPlayerTurn())
            {
                ActionPoints = ACTION_POINTS_MAX;

                OnAnyActionPointsChanged?.Invoke();
            }
            
        }

        public bool IsEnemy()
        {
            return m_IsEnemy;
        }
    }
}
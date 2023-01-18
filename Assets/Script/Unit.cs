using System;
using UnityEngine;

namespace Script
{
    public class Unit : MonoBehaviour
    {

        private MoveAction _MoveAction;
        private SpinAction _spinAction;
        private BaseAction[] _baseActionArray;
        
        private GridPosition gridPosition;

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
    }
}
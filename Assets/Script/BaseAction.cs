using System;
using System.Collections.Generic;
using UnityEngine;


namespace Script
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit _unit;
        protected bool _isActive;
        protected Action onActionComplete;


        protected virtual void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        public abstract string GetActionName();

        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);


        public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPosition();

            return validGridPositionList.Contains(gridPosition);

        }

        public abstract List<GridPosition> GetValidActionGridPosition();


        public virtual int GetActionPointCost()
        {
            return 1;
        }
    }
}


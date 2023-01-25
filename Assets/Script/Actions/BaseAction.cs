using System;
using System.Collections.Generic;
using UnityEngine;


namespace Script
{
    public abstract class BaseAction : MonoBehaviour
    {

        public static event EventHandler OnActionStarted;
        public static event EventHandler OnActionCompleted;

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

        protected void ActionStart(Action onActionStart)
        {
            _isActive = true;
            this.onActionComplete = onActionStart;

            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }

        protected void ActionComplete()
        {
            _isActive = false;
            onActionComplete();

            OnActionCompleted?.Invoke(this, EventArgs.Empty);
        }

        public Unit GetUnit()
        {
            return _unit;
        }

        public EnemyActionAI GetEnemyBestActionAI()
        {
            List<EnemyActionAI> enemyAiActionList = new List<EnemyActionAI>();

            List<GridPosition> validActionGridPosition = GetValidActionGridPosition();

            foreach(GridPosition gridPosition in validActionGridPosition)
            {
                EnemyActionAI enemyAiAction = GetEnemyAIAction(gridPosition);
                enemyAiActionList.Add(enemyAiAction);
            }

            if (enemyAiActionList.Count > 0)
            {
                enemyAiActionList.Sort((EnemyActionAI a, EnemyActionAI b) => b.actionValue - a.actionValue);
                return enemyAiActionList[0];
            }
            else
            {
                return null;
            }
       
        }


        public abstract EnemyActionAI GetEnemyAIAction(GridPosition gridPosition);
    }
}


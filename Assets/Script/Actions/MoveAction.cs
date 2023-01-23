using System.Collections.Generic;
using UnityEngine;
using System;

namespace Script
{
    public class MoveAction : BaseAction
    {
        public event Action OnStatMoving;
        public event Action OnStopMoving;

        [SerializeField] private float m_MovementSpeed;
        [SerializeField] private int m_MaxMoveDistance = 4;


        private Vector3 targetPosition;


        protected override void Awake()
        {
            base.Awake();
            targetPosition = transform.position;
        }



        private void Update()
        {

            if (!_isActive) return;

            Vector3 moveDirection = (targetPosition - transform.position).normalized;


            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                
                transform.position += moveDirection * (m_MovementSpeed * Time.deltaTime);
                
            }
            else
            {
                OnStopMoving?.Invoke();
                ActionComplete();
            }

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * 15f);
        }


        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            ActionStart(onActionComplete);
            targetPosition = LevelGrid.Instance.GetWorldposition(gridPosition);
            OnStatMoving?.Invoke();
        }


        //public bool IsValidActionGridPosition(GridPosition gridPosition)
        //{
        //    List<GridPosition> validGridPositionList =  GetValidActionGridPosition();
            
        //    return validGridPositionList.Contains(gridPosition);

        //}


        public override List<GridPosition> GetValidActionGridPosition()
        {
            List<GridPosition> validGridPosition = new List<GridPosition>();

            GridPosition unitGridPosition = _unit.GetGridPosition();

            for (int x = -m_MaxMoveDistance; x <= m_MaxMoveDistance; x++)
            {
                for (int z = -m_MaxMoveDistance; z <= m_MaxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPOsition = unitGridPosition + offsetGridPosition;


                    if(!LevelGrid.Instance.IsValidGridPosition(testGridPOsition))
                    {
                        continue;
                    }


                    if(unitGridPosition == testGridPOsition)
                    {
                        continue;
                    }

                    if(LevelGrid.Instance.hasAnyUnitOnGridPosition(testGridPOsition))
                    {
                        continue;
                    }

                    validGridPosition.Add(testGridPOsition);
                }
            }

            return validGridPosition;
        }

        public override string GetActionName()
        {
            return "Move";
        }

    }
}


using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class MoveAction : MonoBehaviour
    {

        [SerializeField] private float m_MovementSpeed;
        [SerializeField] private int m_MaxMoveDistance = 4;


        private Vector3 targetPosition;
        private Unit _unit;


        private void Awake()
        {
            _unit = GetComponent<Unit>();
            targetPosition = transform.position;
        }

        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * (m_MovementSpeed * Time.deltaTime);
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * 15f);
            }
        }


        public List<GridPosition> GetValidActionGridPosition()
        {
            List<GridPosition> validGridPosition = new List<GridPosition>();

            GridPosition unitGridPosition = _unit.GetGridPosition();

            for (int x = -m_MaxMoveDistance; x <= m_MaxMoveDistance; x++)
            {
                for (int z = -m_MaxMoveDistance; z <= m_MaxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPOsition = unitGridPosition + offsetGridPosition;
                }
            }

            return validGridPosition;
        }

    }
}


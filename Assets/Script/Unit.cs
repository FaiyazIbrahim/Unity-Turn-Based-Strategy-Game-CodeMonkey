using System;
using UnityEngine;

namespace Script
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float m_MovementSpeed;

        private Vector3 targetPosition;
        private GridPosition gridPosition;



        private void Awake()
        {
            targetPosition = transform.position;
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * (m_MovementSpeed * Time.deltaTime);
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * 15f);
            }

            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if(newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedToGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }

        }
        
        
        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}
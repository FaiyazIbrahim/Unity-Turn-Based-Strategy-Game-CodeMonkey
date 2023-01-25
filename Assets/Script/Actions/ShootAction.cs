using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class ShootAction : BaseAction
    {

        public event Action<Unit> OnShoot;

        private enum State
        {
            Aiming,
            Shooting,
            Cooloff,
        }


        private State _state;
        private int m_MaxShootDistance = 7;
        private float _stateTimer;
        private Unit _targetUnit;
        private bool _canShootBullet;



        private void Update()
        {
            if (!_isActive) return;

            _stateTimer -= Time.deltaTime;

            switch(_state)
            {
                case State.Aiming:
                    Vector3 aimDirection = (_targetUnit.GetWorldPosition() - _unit.GetWorldPosition()).normalized; 
                    transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 15f);
                    break;
                case State.Shooting:
                    if(_canShootBullet)
                    {
                        Shoot();
                        _canShootBullet = false;
                    }
                    break;
                case State.Cooloff:
                  
                    break;
            }


            if(_stateTimer <= 0)
            {
                ChangeState();
            }

        }


        private void Shoot()
        {
            OnShoot?.Invoke(_targetUnit);
            _targetUnit.Damage(40f);
        }

        private void ChangeState()
        {
            switch (_state)
            {
                case State.Aiming:
                    _state = State.Shooting;
                    float shootingTime = 0.1f;
                    _stateTimer = shootingTime;
                    break;
                case State.Shooting:
                    _state = State.Cooloff;
                    float cooloffTime = 0.5f;
                    _stateTimer = cooloffTime;
                    break;
                case State.Cooloff:
                    ActionComplete();
                    break;
            }

            
        }

        public override string GetActionName()
        {
            return "Shoot";
        }


        public override List<GridPosition> GetValidActionGridPosition()
        {
            GridPosition unitGridPosition = _unit.GetGridPosition();
            return GetValidActionGridPosition(unitGridPosition);
        }


        public List<GridPosition> GetValidActionGridPosition(GridPosition unitGridPosition)
        {

            List<GridPosition> validGridPosition = new List<GridPosition>();

            

            for (int x = -m_MaxShootDistance; x <= m_MaxShootDistance; x++)
            {
                for (int z = -m_MaxShootDistance; z <= m_MaxShootDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPOsition = unitGridPosition + offsetGridPosition;


                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPOsition))
                    {
                        continue;
                    }


                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if(testDistance > m_MaxShootDistance)
                    {
                        continue;
                    }


                    if (!LevelGrid.Instance.hasAnyUnitOnGridPosition(testGridPOsition))
                    {
                        continue;
                    }


                    Unit targetUnit =  LevelGrid.Instance.GetUnitAtGridPosition(testGridPOsition);

                    if(targetUnit.IsEnemy() == _unit.IsEnemy())
                    {
                        //both same
                        continue;
                    }

                    validGridPosition.Add(testGridPOsition);
                }
            }

            return validGridPosition;
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {

            _targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);


            _state = State.Aiming;
            float aimingStateTime = 1;
            _stateTimer = aimingStateTime;

            _canShootBullet = true;


            ActionStart(onActionComplete);

        }

        public Unit GetTargetunit()
        {
            return _targetUnit;
        }

        public int GetShootRange()
        {
            return m_MaxShootDistance;
        }

        public override EnemyActionAI GetEnemyAIAction(GridPosition gridPosition)
        {
            Unit targetUnit =  LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
            
            return new EnemyActionAI
            {
                gridPosition = gridPosition,
                actionValue = 100 + Mathf.RoundToInt(( 1- targetUnit.GetHealthNormalized()) * 100f),
            };
        }

        public int GetTargetCountAtGridPosition(GridPosition gridPosition)
        {
            return GetValidActionGridPosition(gridPosition).Count;
        }
    }
}


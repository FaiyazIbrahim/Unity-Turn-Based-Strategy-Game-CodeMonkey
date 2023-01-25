using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Script
{
    public class EnemyAI : MonoBehaviour
    {
        public enum State
        {
            WaitingForEnemyTurn,
            TakingTurn,
            Busy
        }


        private State _state;
        private float _timer;

        private void Awake()
        {
            _state = State.WaitingForEnemyTurn;
        }


        private void Start()
        {
            TurnSystem.Instance.OnTurnChange += OnTurnChanged;
        }

        private void Update()
        {
            if (TurnSystem.Instance.IsPlayerTurn()) return;


            switch (_state)
            {
                case State.WaitingForEnemyTurn:

                    break;
                case State.TakingTurn:

                    _timer -= Time.deltaTime;

                    if (_timer <= 0f)
                    {
                        if(TryTakeEnemyAiAction(SetStateTakingTurn))
                        {
                            _state = State.Busy;
                        }
                        else
                        {
                            //no more actions
                            TurnSystem.Instance.nextTurn();
                        }
                    }

                    break;
                case State.Busy:

                    break;
            }
        }

        private void SetStateTakingTurn()
        {
            _timer = 0.5f;
            _state = State.TakingTurn;
        }

        private void OnTurnChanged()
        {
            if (!TurnSystem.Instance.IsPlayerTurn())
            {
                _state = State.TakingTurn;
                _timer = 2f;
            }
        }

        private bool TryTakeEnemyAiAction(Action onEnemyAiActionComplete)
        {
            foreach (Unit enemyunit in UnitManager.Instance.GetEnemyUnitList())
            {
                if (TryTakeEnemyAiAction(enemyunit, onEnemyAiActionComplete))
                {
                    return true;
                }
                
            }

            return false;
        }

        private bool TryTakeEnemyAiAction(Unit enemyunit, Action onEnemyAiActionComplete)
        {
            EnemyActionAI bestEnemyActionAI = null;
            BaseAction bestBaseAction = null;
            
            foreach (BaseAction baseAction in enemyunit.GetBaseActionsArray())
            {
                if (!enemyunit.CanSpendActionPointsToTakeAction(baseAction))
                {
                    //cannon affort action
                    continue;
                }

                if (bestBaseAction == null)
                {
                    bestEnemyActionAI = baseAction.GetEnemyBestActionAI();
                    bestBaseAction = baseAction;
                }
                else
                {
                    EnemyActionAI testEnemyAiAction = baseAction.GetEnemyBestActionAI();
                    if (testEnemyAiAction != null && testEnemyAiAction.actionValue > bestEnemyActionAI.actionValue)
                    {
                        bestEnemyActionAI = testEnemyAiAction;
                        bestBaseAction = baseAction;
                    }
                }
                
            }

            if (bestEnemyActionAI != null && enemyunit.TrySpendActionPointsToTakeAction(bestBaseAction))
            {
                bestBaseAction.TakeAction(bestEnemyActionAI.gridPosition, onEnemyAiActionComplete);
                return true;
            }
            else
            {
                return false;
            }
            
            
            
            // SpinAction spinAction = enemyunit.GetSpinAction();
            // GridPosition ActionGridposition = enemyunit.GetGridPosition();
            //
            // if (!spinAction.IsValidActionGridPosition(ActionGridposition)) return false;
            // if (!enemyunit.TrySpendActionPointsToTakeAction(spinAction)) return false;
            //
            //
            // spinAction.TakeAction(ActionGridposition, onEnemyAiActionComplete);
            //
            // return true;

        }
    }
}


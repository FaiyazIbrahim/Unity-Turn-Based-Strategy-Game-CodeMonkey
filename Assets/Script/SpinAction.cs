using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class SpinAction : BaseAction
    {

        private float _totalSpin;
        private float _spiningAmount = 360f;


        private void Update()
        {
            if (!_isActive) return;
            transform.eulerAngles += new Vector3(0, _spiningAmount * Time.deltaTime, 0);

            _totalSpin += _spiningAmount * Time.deltaTime;

            if (_totalSpin >= 360f)
            {
                _isActive = false;
                onActionComplete();
            }
        }


        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            this.onActionComplete = onActionComplete;
            _isActive = true;
            _totalSpin = 0f;
        }

        public override string GetActionName()
        {
            return "Spin";
        }

        public override List<GridPosition> GetValidActionGridPosition()
        {

            GridPosition unitGridPosition = _unit.GetGridPosition();

            return new List<GridPosition>
            {
                unitGridPosition
            };

        }

    }

}

using System;
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
    }
}


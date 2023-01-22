using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class ActionBusyUI : MonoBehaviour
    {

        private void Start()
        {
            UnitActionSystem.Instance.onBusyChanged += SetAsActive;
            SetAsActive(false);
        }

        private void SetAsActive(bool value)
        {
            gameObject.SetActive(value);
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class UnitSelectedVisual : MonoBehaviour
    {

        private Unit _unit;

        private MeshRenderer meshRenderer;

        private void Awake()
        {
            _unit = GetComponentInParent<Unit>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            UnitActionSystem.Instance.onSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

            UpdateVisual();
        }

        private void UnitActionSystem_OnSelectedUnitChanged()
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (UnitActionSystem.Instance.GetSelectedUnit() == _unit)
            {
                meshRenderer.enabled = true;
            }
            else
            {
                meshRenderer.enabled = false;
            }
        }

        private void OnDestroy()
        {
            UnitActionSystem.Instance.onSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
        }


    }
}


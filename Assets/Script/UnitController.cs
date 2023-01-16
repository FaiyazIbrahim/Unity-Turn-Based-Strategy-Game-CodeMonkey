using System;
using UnityEngine;

namespace Script
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private Unit m_Unit;
        [SerializeField] private LayerMask m_UnitLayer;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(TryHandleUnitSelection()) return;
                m_Unit.Move(MouseWorld.GetHitPoint());
            }
        }

        private bool TryHandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, m_UnitLayer))
            {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    m_Unit = unit;
                    return true;
                }
            }

            return false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Script
{
    public class Testing : MonoBehaviour
    {
        [SerializeField] private Unit m_Unit;
        [SerializeField] private GridSystemVisual m_GridSystemVisual;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                m_GridSystemVisual.HideAllGridPosition();
                m_GridSystemVisual.ShowGridPositionList(m_Unit.GetMoveAction().GetValidActionGridPosition());
            }
        }
    }

}

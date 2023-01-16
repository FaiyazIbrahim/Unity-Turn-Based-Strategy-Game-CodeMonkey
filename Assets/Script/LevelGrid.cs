using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class LevelGrid : MonoBehaviour
    {
        public static LevelGrid Instance;

        [SerializeField] private Transform m_debugObject;

        private GridSystem _gridSystem;


        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            _gridSystem = new GridSystem(10, 10, 3f);
            _gridSystem.CreateDebugObject(m_debugObject);

        }

        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.AddUnit(unit);
        }

        public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnitList();
        }

        public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveList(unit);
        }

        public void UnitMovedToGridPosition(Unit unit, GridPosition fromPosition, GridPosition toPosition)
        {
            RemoveUnitAtGridPosition(fromPosition, unit);
            AddUnitAtGridPosition(toPosition, unit);
        }


        public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);

    }

}


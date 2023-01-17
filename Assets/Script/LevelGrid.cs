
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

            _gridSystem = new GridSystem(10, 10, 3f);
            _gridSystem.CreateDebugObject(m_debugObject);
        }


        private void Start()
        {
          

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

        public Vector3 GetWorldposition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);

        public bool IsValidGridPosition(GridPosition gridPosition) => _gridSystem.IsValidGridPosition(gridPosition);

        public int GetWidth() => _gridSystem.GetWidth();
        public int GetHeight() => _gridSystem.GetHeight();

        public bool hasAnyUnitOnGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            return gridObject.HasAnyUnit();
        }

    }

}


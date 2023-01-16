using System.Collections.Generic;

namespace Script
{
    public class GridObject
    {
        private GridSystem _gridSystem;
        private GridPosition _gridPosition;
        private List<Unit> _unitList = new List<Unit>();

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this._gridPosition = gridPosition;
            this._gridPosition = gridPosition;
        }

        public override string ToString()
        {
            string unitName = "";

            foreach(var Unit in _unitList)
            {
                unitName += Unit + "\n";
            }

            return _gridPosition.ToString() + "\n" + unitName;

        }

        public void AddUnit(Unit unit)
        {
            _unitList.Add(unit);
        }

        public void RemoveList(Unit unit)
        {
            _unitList.Remove(unit);
        }

        public List<Unit> GetUnitList()
        {
            return _unitList;
        }
    }
}
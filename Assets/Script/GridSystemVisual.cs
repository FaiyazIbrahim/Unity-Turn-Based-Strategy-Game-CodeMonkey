using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class GridSystemVisual : MonoBehaviour
    {
        [SerializeField] private Transform m_GridSystemVisualPrefab;

        private GridVisualSingle[,] gridSystemVisualSingleArray;


        private void Start()
        {
            gridSystemVisualSingleArray = new GridVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform gridVisualSingleTransform =  Instantiate(m_GridSystemVisualPrefab, LevelGrid.Instance.GetWorldposition(gridPosition), Quaternion.identity);

                    gridSystemVisualSingleArray[x, z] = gridVisualSingleTransform.GetComponent<GridVisualSingle>();

                }
            }
        }

        private void Update()
        {
            UpdateGridVisual();
        }

        public void HideAllGridPosition()
        {
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    gridSystemVisualSingleArray[x, z].Hide();

                }
            }
        }

        public void ShowGridPositionList(List<GridPosition> gridPositions)
        {
            foreach(GridPosition gridPosition in gridPositions)
            {
                gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
            }
        }


        public void UpdateGridVisual()
        {
            Unit unit = UnitActionSystem.Instance.GetSelectedUnit();
            HideAllGridPosition();
            ShowGridPositionList(unit.GetMoveAction().GetValidActionGridPosition());
        }

    }
}


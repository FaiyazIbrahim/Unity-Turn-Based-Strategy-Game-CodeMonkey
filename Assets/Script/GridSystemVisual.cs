using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class GridSystemVisual : MonoBehaviour
    {


        [SerializeField] private List<GridVisualTypeMaterial> m_GridVisualTypeMaterialList;
        [SerializeField] private Transform m_GridSystemVisualPrefab;

        private GridVisualSingle[,] gridSystemVisualSingleArray;


        public enum GridVisualType
        {
            White,
            Blue,
            Red,
            Yellow,
            RedSoft
        }


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

            UnitActionSystem.Instance.onSelectedActionChanged += UpdateGridVisual;
            LevelGrid.Instance.OnAnyUnitMovedGridPosition += UpdateGridVisual;

            UpdateGridVisual();
        }


        //private void Update()
        //{
        //    //UpdateGridVisual();
        //}


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

        private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
        {
            List<GridPosition> gridPositionList = new List<GridPosition>();
            for(int x = -range; x <= range; x++)
            {
                for(int z = -range; z <= range; z++)
                {
                    GridPosition testGridPosition = gridPosition + new GridPosition(x, z);


                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }


                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if(testDistance > range)
                    {
                        continue;
                    }

                    gridPositionList.Add(testGridPosition);
                }
            }

            ShowGridPositionList(gridPositionList, gridVisualType);
        } 



        public void ShowGridPositionList(List<GridPosition> gridPositions, GridVisualType gridVisualType)
        {
            foreach(GridPosition gridPosition in gridPositions)
            {
                gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisualType));
            }
        }


        public void UpdateGridVisual()
        {
            HideAllGridPosition();

            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            BaseAction SelectedAction = UnitActionSystem.Instance.GetSelectedAction();

            GridVisualType gridVisualType;

            switch(SelectedAction)
            {
                default:
                case MoveAction moveAction:
                    gridVisualType = GridVisualType.White;
                    break;

                case SpinAction spinAction:
                    gridVisualType = GridVisualType.Blue;
                    break;

                case ShootAction shootAction:
                    gridVisualType = GridVisualType.Red;

                    ShowGridPositionRange(selectedUnit.GetGridPosition(), shootAction.GetShootRange(), GridVisualType.RedSoft);

                    break;
            }

            ShowGridPositionList(SelectedAction.GetValidActionGridPosition(), gridVisualType);
        }


        [System.Serializable]
        public struct GridVisualTypeMaterial
        {
            public GridVisualType gridVisualType;
            public Material material;
        }




        private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
        {
            foreach(GridVisualTypeMaterial gridVisualTypeMaterial in m_GridVisualTypeMaterialList)
            {
                if(gridVisualTypeMaterial.gridVisualType == gridVisualType)
                {
                    return gridVisualTypeMaterial.material;
                }
            }

            Debug.LogError("Could not find GridVisualType " + gridVisualType);
            return null;
        }

    }
}


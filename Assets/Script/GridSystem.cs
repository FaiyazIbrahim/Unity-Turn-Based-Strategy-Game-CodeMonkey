using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Rendering;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] _gridObjectsArray;
    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;


        _gridObjectsArray = new GridObject[width, height];
        for (int x = 0; x < this.width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Debug.DrawLine(GetWorldPosition(x,z), GetWorldPosition(x, z) + Vector3.right * .2f, Color.black, 100000);
                GridPosition gridPosition = new GridPosition(x, z);
                _gridObjectsArray[x, z] = new GridObject(this, gridPosition);
            }
        }

    }

    private Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }


    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    public void CreateDebugObject(Transform debugObject)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                
                Transform debugGridObject = GameObject.Instantiate(debugObject, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject debuggridobject =  debugGridObject.GetComponent<GridDebugObject>();
                debuggridobject.SetDebugObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return _gridObjectsArray[gridPosition.x, gridPosition.z];

    }
    
}

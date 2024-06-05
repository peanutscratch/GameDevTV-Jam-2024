using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GridData 
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPos, Vector2Int objectSize, string Name, int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPos, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, Name, placedObjectIndex);

        foreach(var pos in positionToOccupy)
        {
            if(placedObjects.ContainsKey(pos))
            {
                throw new System.Exception($"Dictionary already contains this cell position {pos}");
            }
            placedObjects[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPos, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPos + new Vector3Int(x,y,0));
            }
        }
        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPos, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPos, objectSize);
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                return false;
            }
        }
        return true;
    }

    internal int GetRepresentationIndex(Vector3Int gridPos)
    {
        if(placedObjects.ContainsKey(gridPos) == false)
        {
            return -1;
        }
        return placedObjects[gridPos].PlacedObjectIndex;
    }

    internal void RemoveObjectAt(Vector3Int gridPos)
    {
        foreach(var pos in placedObjects[gridPos].occupiedPositions)
        {
            placedObjects.Remove(pos);
            
        }
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public string Name {get; private set; }
    public int PlacedObjectIndex {get; private set;}


    public PlacementData(List<Vector3Int> occupiedPositions, string Name, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        this.Name = Name;
        PlacedObjectIndex = placedObjectIndex;
    }

}

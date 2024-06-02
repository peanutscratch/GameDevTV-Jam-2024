using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlacementState : IObjectState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectsDatabaseSO database;
    GridData saleData;
    GridData equipmentData;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD, Grid grid, PreviewSystem previewSystem, ObjectsDatabaseSO database, GridData saleData, GridData equipmentData, ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.saleData = saleData;
        this.equipmentData = equipmentData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefab, database.objectsData[selectedObjectIndex].Size);
    
        }
        else
        {
            throw new System.Exception($"No object with ID {ID}");
        }
        
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPos)
    {
        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if(placementValidity == false)
        {
            return;
        }

        

        int index = objectPlacer.PlaceObject(database.objectsData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPos));
        
        GridData selectedData = database.objectsData[selectedObjectIndex].isPromotion == true ? saleData : equipmentData;
        selectedData.AddObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, index);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPos), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPos, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].isPromotion == true ? saleData : equipmentData;
        //Debug.Log("grid position is: "+gridPos+", object index is: "+selectedObjectIndex);
        return selectedData.CanPlaceObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int gridPos)
    {
        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPos), placementValidity);
    }

}

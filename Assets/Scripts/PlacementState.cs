using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlacementState : IObjectState
{
    private int selectedObjectIndex = -1;
    GameObject tile;
    string Name;
    Grid grid;
    PreviewSystem previewSystem;
    ObjectsDatabaseSO database;
    GridData saleData;
    GridData equipmentData;
    ObjectPlacer objectPlacer;
    SoundManager soundManager;
    List<ObjectData> placedGameObjectsMetadata;

    public PlacementState(GameObject tile, Grid grid, PreviewSystem previewSystem, ObjectsDatabaseSO database, GridData saleData, GridData equipmentData, ObjectPlacer objectPlacer, SoundManager soundManager, List<ObjectData> placedGameObjectsMetadata)
    {
        this.tile = tile;
        this.Name = tile.GetComponent<TableMetadata>().Name;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.saleData = saleData;
        this.equipmentData = equipmentData;
        this.objectPlacer = objectPlacer;
        this.soundManager = soundManager;
        this.placedGameObjectsMetadata = placedGameObjectsMetadata;

        selectedObjectIndex = database.objectsData.FindIndex(data => data.Name == Name);
        if(selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefab, database.objectsData[selectedObjectIndex].Size);
    
        }
        else
        {
            throw new System.Exception($"No object with Name {Name}");
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
            soundManager.PlaySound(SoundType.wrongPlacement);
            return;
        }

        soundManager.PlaySound(SoundType.Place);

        int index = objectPlacer.PlaceObject(database.objectsData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPos));
        
        GridData selectedData = database.objectsData[selectedObjectIndex].isPromotion == true ? saleData : equipmentData;
        selectedData.AddObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].Name, index);
        placedGameObjectsMetadata.Add(database.objectsData[selectedObjectIndex]);

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

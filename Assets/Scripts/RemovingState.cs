using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IObjectState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    GridData saleData;
    GridData equipmentData;
    ObjectPlacer objectPlacer;
    SoundManager soundManager;
    public RemovingState(Grid grid, PreviewSystem previewSystem, GridData saleData, GridData equipmentData, ObjectPlacer objectPlacer, SoundManager soundManager)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.saleData = saleData;
        this.equipmentData = equipmentData;
        this.objectPlacer = objectPlacer;
        this.soundManager = soundManager;
        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPos)
    {
        GridData selectedData = null;
        if(equipmentData.CanPlaceObjectAt(gridPos,Vector2Int.one) == false)
        {
            selectedData = equipmentData;
        }
        else if(saleData.CanPlaceObjectAt(gridPos, Vector2Int.one) == false)
        {
            selectedData = saleData;
        }

        

        if(selectedData == null)
        {
            //sound
            soundManager.PlaySound(SoundType.wrongPlacement);
        }
        else
        {
            soundManager.PlaySound(SoundType.Remove);
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPos);
            if(gameObjectIndex == -1)
            {
                return;
            }
            selectedData.RemoveObjectAt(gridPos);
            objectPlacer.RemoveObjectAt(gameObjectIndex);

        }

        Vector3 cellPos = grid.CellToWorld(gridPos);
        previewSystem.UpdatePosition(cellPos, CheckIfSelectionIsValid(gridPos));
    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPos)
    {
        return !(equipmentData.CanPlaceObjectAt(gridPos, Vector2Int.one) && saleData.CanPlaceObjectAt(gridPos, Vector2Int.one));
    }

    public void UpdateState(Vector3Int gridPos)
    {
        bool validity = CheckIfSelectionIsValid(gridPos);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPos), validity);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;

    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip correctPlacementClip;
    
    [SerializeField]
    private AudioClip wrongPlacementClip;

    public GridData saleData, equipmentData;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    IObjectState objectState;

    private void Start()
    {
        gridVisualization.SetActive(false);
        saleData = new();
        equipmentData = new();

    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        objectState = new PlacementState(ID, grid, preview, database, saleData, equipmentData,objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;

    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        objectState = new RemovingState(grid, preview, saleData, equipmentData, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePos = inputManager.GetSelectedMapPos();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        objectState.OnAction(gridPos);

    }

    // private bool CheckPlacementValidity(Vector3Int gridPos, int selectedObjectIndex)
    // {
    //     GridData selectedData = database.objectsData[selectedObjectIndex].isPromotion == true ? saleData : equipmentData;

    //     return selectedData.CanPlaceObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size);
    // }

    private void StopPlacement()
    {
        if(objectState == null)
        {
            return;
        }
        gridVisualization.SetActive(false);
        objectState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        objectState = null;
    }

    private void Update()
    {
        if(objectState == null)
        {
            return;
        }
        Vector3 mousePos = inputManager.GetSelectedMapPos();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if(lastDetectedPosition != gridPos)
        {
            objectState.UpdateState(gridPos);
            lastDetectedPosition = gridPos;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator; //3D mouse pointer

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip correctPlacementClip;
    
    [SerializeField]
    private AudioClip wrongPlacementClip;

    public GridData saleData, equipmentData;


    private List<GameObject> placedGameObjects = new();

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;
    private void Start()
    {
        StopPlacement();
        saleData = new();
        equipmentData = new();

    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            UnityEngine.Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefab, database.objectsData[selectedObjectIndex].Size);
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

        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if(placementValidity == false)
        {
            source.PlayOneShot(wrongPlacementClip);
            return;
        }

        source.PlayOneShot(correctPlacementClip);
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPos);
        placedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].isPromotion == true ? saleData : equipmentData;
        selectedData.AddObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, placedGameObjects.Count-1);

        preview.UpdatePosition(grid.CellToWorld(gridPos), false);

    }

    private bool CheckPlacementValidity(Vector3Int gridPos, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].isPromotion == true ? saleData : equipmentData;

        return selectedData.CanPlaceObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    private void Update()
    {
        if(selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 mousePos = inputManager.GetSelectedMapPos();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if(lastDetectedPosition != gridPos)
        {
            bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);

            mouseIndicator.transform.position = mousePos;
            preview.UpdatePosition(grid.CellToWorld(gridPos), placementValidity);
            lastDetectedPosition = gridPos;
        }
    }
}

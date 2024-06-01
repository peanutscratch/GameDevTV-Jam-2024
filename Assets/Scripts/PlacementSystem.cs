using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject mouseIndicator, cellIndicator; //3D mouse pointer

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

    private GridData saleData, furnitureData;

    private Renderer[] previewRenderers;

    private List<GameObject> placedGameObjects = new();
    private void Start()
    {
        StopPlacement();
        saleData = new();
        furnitureData = new();
        previewRenderers = cellIndicator.GetComponentsInChildren<Renderer>();

    }

    public void StartPlacement(int ID)
    {
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if(selectedObjectIndex < 0)
        {
            UnityEngine.Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
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
            return;
        }

        source.Play();
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        newObject.transform.position = grid.CellToWorld(gridPos);
        placedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].isPromotion == true ? saleData : furnitureData;
        selectedData.AddObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, placedGameObjects.Count-1);

    }

    private bool CheckPlacementValidity(Vector3Int gridPos, int selectedObjectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].isPromotion == true ? saleData : furnitureData;

        return selectedData.CanPlaceObjectAt(gridPos, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if(selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 mousePos = inputManager.GetSelectedMapPos();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        foreach(Renderer r in previewRenderers)
        {
            r.material.color = placementValidity ? Color.white : Color.red;
        }
    

        mouseIndicator.transform.position = mousePos;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
    }
}

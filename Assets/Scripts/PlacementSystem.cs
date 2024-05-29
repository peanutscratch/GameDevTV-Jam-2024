using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject mouseIndicator, cellIndicator; //3D mouse pointer

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    private void Update()
    {
        Vector3 mousePos = inputManager.GetSelectedMapPos();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        mouseIndicator.transform.position = mousePos;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
    }
}

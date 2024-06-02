using UnityEngine;

public interface IObjectState
{
    void EndState();
    void OnAction(Vector3Int gridPos);
    void UpdateState(Vector3Int gridPos);
}

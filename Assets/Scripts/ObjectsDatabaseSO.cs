using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabaseSO : ScriptableObject
{
    public List<ObjectData> objectsData;

}


[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public string Name {get; set;}

    [field: SerializeField]
    public int ID {get; set;}

    [field: SerializeField]
    public bool isPromotion {get; set;}

    [field: SerializeField]
    public Vector2Int Size {get; set;} = Vector2Int.one;

    [field: SerializeField]
    public GameObject Prefab {get; set;}

    [field: SerializeField]
    public ItemScriptableObject ItemInformation {get; set;}
}

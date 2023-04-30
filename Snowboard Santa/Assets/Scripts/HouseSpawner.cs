using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.EditorTools;
#endif

using Unity.VisualScripting;

public class HouseSpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private Transform HouseParent;


    [Header("Houses")]
    public House HousePrefab;
    public float MinHouseWidth, MaxHouseWidth;
    public float MinHouseHeight, MaxHouseHeight;
    public float MinRoofHeight, MaxRoofHeight;

    [Header("Gaps")]
    public float MinGapWidth;
    public float MaxGapWidth;

    private Vector3 spawnPoint;

    public House SpawnHouse()
    {
        if (HousePrefab == null)
        {
            Debug.LogError("There's no house!");
            return null;
        }
        House latest = Instantiate(HousePrefab, spawnPoint, Quaternion.identity, HouseParent);
        HousePrefab.SetDimensions(Random.Range(MinHouseWidth, MaxHouseWidth), Random.Range(MinHouseHeight, MaxHouseHeight), Random.Range(MinRoofHeight, MaxRoofHeight));
        spawnPoint.x = latest.RightEdge + Random.Range(MinGapWidth, MaxGapWidth);
        return latest;
    }


    public void ClearHouses()
    {
        int count = HouseParent.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            DestroyImmediate(HouseParent.GetChild(i).gameObject);
        }
        spawnPoint = Vector3.zero;
    }


}

#if (UNITY_EDITOR)

[CustomEditor(typeof(HouseSpawner)), CanEditMultipleObjects]
public class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        HouseSpawner houseSpawner = (HouseSpawner)target;

        if (GUILayout.Button("Spawn"))
        {
            for (int i = 0; i < 5; i++)
            {
                houseSpawner.SpawnHouse();
            }
        }

        if (GUILayout.Button("Clear"))
        {
            houseSpawner.ClearHouses();
        }
    }
}
#endif

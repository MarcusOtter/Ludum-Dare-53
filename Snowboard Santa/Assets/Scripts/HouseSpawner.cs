using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    private void Start()
    {
        spawnPoint = HouseParent.position;
    }
    
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
        spawnPoint = HouseParent.position;
    }


}

#if (UNITY_EDITOR)

[CustomEditor(typeof(HouseSpawner)), CanEditMultipleObjects]
public class SpawnerEditor : Editor
{
    // ReSharper disable Unity.PerformanceAnalysis
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

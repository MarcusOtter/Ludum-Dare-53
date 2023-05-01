using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewHouse : MonoBehaviour
{

}

#if (UNITY_EDITOR)

[CustomEditor(typeof(NewHouse)), CanEditMultipleObjects]
public class NewHouseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NewHouse house = (NewHouse)target;

        if (GUILayout.Button("Generate"))
        {
        }
    }
}
#endif

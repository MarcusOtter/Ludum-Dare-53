using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
#endif
using UnityEngine;

public class House : MonoBehaviour
{
    public float MainWidth; 
    public float MainHeight;
    public float RoofHeight = 3f;
    public float LeftEdge => leftCorner.Left();
    public float RightEdge => rightCorner.Right();
    public float TopEdge => roofTop.Top();
    public float BottomEdge => mainPart.Bottom();

    public Vector2 Middle => mainPart.bounds.center;

    [SerializeField] private SpriteRenderer mainPart, roofTop, leftCorner, rightCorner;

    public void SetDimensions(float w, float h, float roofH)
    {
        MainWidth = w; MainHeight = h;
        RoofHeight = roofH;
        Generate();
    }

    private void Start()
    {
        Generate();
    }

    private void ScaleToVariables()
    {
        mainPart.transform.localScale = new Vector2(MainWidth, MainHeight);
        roofTop.transform.position = mainPart.TopLeft();
        roofTop.transform.localScale = new Vector2(mainPart.transform.localScale.x, RoofHeight);
        leftCorner.transform.localScale = rightCorner.transform.localScale = new Vector2(roofTop.transform.localScale.y, roofTop.transform.localScale.y);
    }

    public void Generate()
    {
        ScaleToVariables();
        leftCorner.transform.position = roofTop.MidLeft();
        rightCorner.transform.position = roofTop.MidRight();
    }

}

#if (UNITY_EDITOR)

[CustomEditor(typeof(House)), CanEditMultipleObjects]
public class HouseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        House house = (House)target;

        if (GUILayout.Button("Generate"))
        {
            house.Generate();
        }
    }
}
#endif
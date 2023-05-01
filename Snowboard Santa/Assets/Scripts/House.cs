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


    [SerializeField] private Color[] MainBitColors;
    [SerializeField] private Color[] RoofBitColors;

    public Vector2 Middle => mainPart.bounds.center;

    [SerializeField] private SpriteRenderer mainPart, roofTop, leftCorner, rightCorner, chimney;

    public void SetDimensions(float w, float h, float roofH)
    {
        MainWidth = w; MainHeight = h;
        RoofHeight = roofH;
        Generate();
    }

    private void Start()
    {

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
        ChooseColors();
        PositionSlopes();
        PositionChimney();
    }

    private void ChooseColors()
    {
        leftCorner.color = rightCorner.color  = roofTop.color = RoofBitColors[Random.Range(0, RoofBitColors.Length)];
        mainPart.color = MainBitColors[Random.Range(0, MainBitColors.Length)];
    }

    private void PositionSlopes()
    {
        leftCorner.transform.position = roofTop.MidLeft();
        rightCorner.transform.position = roofTop.MidRight();
    }

    private void PositionChimney()
    {
        float diff = roofTop.HalfWidth() - chimney.HalfWidth();
        chimney.transform.position = roofTop.TopMid() - new Vector2(chimney.HalfWidth() + Random.Range(-diff, diff), 0f);
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
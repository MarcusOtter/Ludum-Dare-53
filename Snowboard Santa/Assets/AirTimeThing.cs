using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
public class AirTimeThing : MonoBehaviour
{
    Vector3 EulerRotation;
    private TextMeshProUGUI text;
    public static bool AirTimeOn;
    public static bool WheelieOn;

    public enum TextType
    {
        Air, Wheelie
    }

    public TextType Type;

    private void OnEnable()
    {
        AirTimeOn = false;
        WheelieOn = false;

        text = GetComponent<TextMeshProUGUI>();
        text.color = new Color(1f, 1f, 1f, 0f);
        transform.localScale = Vector3.one * 0.6f;
    }

    void Update()
    {
        if(GameStateHandler.GameEnded) { AirTimeOn = false; }
        EulerRotation.z = Mathf.Sin(Time.time * 4f) * 15f;

        transform.rotation = Quaternion.Euler(EulerRotation);

        if (Type == TextType.Air && AirTimeOn || Type == TextType.Wheelie && WheelieOn)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 12f * Time.deltaTime);
            text.color = Color.Lerp(text.color, Color.white, 12f * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 12f * Time.deltaTime);
            text.color = Color.Lerp(text.color, new Color(1f, 1f, 1f, 0f), 12f * Time.deltaTime);
        }
    }
}

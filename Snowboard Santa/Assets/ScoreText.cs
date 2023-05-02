using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public string scoreMessage;
    public float rotationDelta, rotationDeltaMax;
    void Start()
    {
        text = gameObject.AddComponent<TextMeshProUGUI>();
        text.text = scoreMessage;
        text.color = new Color(1f, 1f, 1f, 0f);
        transform.localScale = Vector3.one * 0.6f;
        rotationDelta = Random.Range(-rotationDeltaMax, rotationDeltaMax);
        transform.position += new Vector3(100f, 100f, 0);
        //transform.RotateAround();
    }

    void Update()
    {
        text.color = Color.Lerp(text.color, Color.white, 6f * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 6f * Time.deltaTime);
        transform.RotateAround(Vector3.forward, rotationDelta);
    }
}

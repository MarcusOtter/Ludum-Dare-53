using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public string scoreMessage;
    public float rotationDelta, rotationDeltaMax;
    private float startTime, timeAlive = 1f;
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = scoreMessage;
        text.color = new Color(1f, 1f, 1f, 0f);
        transform.localScale = Vector3.one * 0.6f;
        rotationDelta = Random.Range(-rotationDeltaMax, rotationDeltaMax);
        transform.position += new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0);
        transform.Rotate(transform.forward, Random.Range(-15f, 15f));
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime < timeAlive)
        {
            text.color = Color.Lerp(text.color, Color.white, 12f * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 12f * Time.deltaTime);
            transform.Rotate(Vector3.forward, rotationDelta * Time.deltaTime);
        }
        else
        {
            text.color = Color.Lerp(text.color, new Color(1f, 1f, 1f, 0f), 12f * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 12f * Time.deltaTime);

            if(transform.localScale.sqrMagnitude < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleResetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent != null)
            transform.localScale = new Vector3(1f / transform.parent.localScale.x, 1f / transform.parent.localScale.y, 1);
    }

}

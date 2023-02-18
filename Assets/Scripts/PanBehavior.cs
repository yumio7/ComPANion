using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanBehavior : MonoBehaviour
{
    public Vector2 PointerPos { get; set; }

    // Update is called once per frame
    void Update()
    {
        var t = transform;
        t.right = (PointerPos - (Vector2)t.position).normalized;
    }
}

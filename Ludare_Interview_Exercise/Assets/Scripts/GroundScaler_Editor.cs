using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GroundScaler_Editor : MonoBehaviour
{
    public float FixeScale = 1;
    public GameObject parent;

    public float scaleDiff = 1.33f;
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(parent.transform.lossyScale.x /scaleDiff, parent.transform.lossyScale.y/scaleDiff, 1);
      
    }
}

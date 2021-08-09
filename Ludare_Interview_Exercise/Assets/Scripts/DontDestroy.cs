using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Just dont destroy this object (used to make music not restart across scenes)
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

  
}

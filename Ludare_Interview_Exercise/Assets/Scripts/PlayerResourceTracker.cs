using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerResourceTracker : MonoBehaviour
{
    public float coins;

    [SerializeField]
    private TextMeshProUGUI coinText;

    private void Update()
    {
        coinText.text = "X" + coins.ToString();
    }
}

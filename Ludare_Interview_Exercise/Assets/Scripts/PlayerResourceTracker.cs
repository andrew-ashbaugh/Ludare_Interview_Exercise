using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerResourceTracker : MonoBehaviour
{
    // Sits on the player, but ideally we would have this as a seperate object with a dont destroy in the startup scene
    public float coins;

    [SerializeField]
    private TextMeshProUGUI coinText;

    private void Update()
    {
        coinText.text = "X" + coins.ToString();
    }
}

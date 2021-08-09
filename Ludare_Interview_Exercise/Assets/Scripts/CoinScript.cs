using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // All coins have this
    //-------- Coin Variables------------

    [Header("Coin Variables")]
    [SerializeField]
    private float value; // how much are coins worth? We can reuse this script for all sorts of valuables

    [SerializeField]
    private GameObject sprite; // what is the coins sprite?

    [SerializeField]
    private ParticleSystem coinParts; // coin particles

    [SerializeField]
    private AudioSource pickupSfx; //pickup sfx

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") // if the player touches a coin, destroy it, play the sfx, and add it to our total
        {
            sprite.SetActive(false);
            coinParts.Stop();
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject,2f);
            other.gameObject.GetComponent<PlayerResourceTracker>().coins += value;
            pickupSfx.Play();
        }
    }
}

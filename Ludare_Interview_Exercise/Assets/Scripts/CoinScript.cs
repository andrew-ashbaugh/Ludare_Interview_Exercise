using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    private float value;

    [SerializeField]
    private GameObject sprite;

    [SerializeField]
    private ParticleSystem coinParts;

    [SerializeField]
    private AudioSource pickupSfx;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
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

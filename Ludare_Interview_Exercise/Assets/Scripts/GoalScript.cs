using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    // Used on each goal object

    //-------- Goal Variables------------

    [Header("Goal Variables")]

    [SerializeField]
    private GameObject[] explosions; // initial explosions
    [SerializeField]
    private GameObject fireworks; // fireworks
    [SerializeField]
    private AudioSource winSfx;

    //-------- Back End Variables------------

    [Header("Back End Variables")]
    [SerializeField]
    private GameObject sprite; // goal flag sprite
    [SerializeField]
    private GameObject winScreen; 
    private bool hasWon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(hasWon == false) // if you havent won yet
            {
                hasWon = true;
               
                StartCoroutine("PlayWin"); // play the win animation
                other.gameObject.GetComponent<PlayerController>().ScreenShake(); 
                other.gameObject.GetComponent<PlayerController>().enabled = false; // stop player from moving
            }
        }
    }

    private IEnumerator PlayWin()
    {
        // We set 3 individual explosions off
        explosions[0].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        explosions[1].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        explosions[2].SetActive(true);
        // then we turn the goal sprite off

        sprite.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        // And play our win sfx, and setoff our fireworks

        fireworks.SetActive(true);
        winSfx.Play();
        // after 2 seconds, show the win screen

        yield return new WaitForSeconds(2f); 
        winScreen.SetActive(true);
       
    }
}

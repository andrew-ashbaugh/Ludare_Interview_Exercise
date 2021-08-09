using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] explosions;
    [SerializeField]
    private GameObject fireworks;
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private AudioSource winSfx;
    private bool hasWon;
    // Start is called before the first frame update



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(hasWon == false)
            {
                hasWon = true;
               
                StartCoroutine("PlayWin");
                other.gameObject.GetComponent<PlayerController>().ScreenShake();
                other.gameObject.GetComponent<PlayerController>().enabled = false;
            }
        }
    }

    private IEnumerator PlayWin()
    {
        explosions[0].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        explosions[1].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        explosions[2].SetActive(true);
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        fireworks.SetActive(true);
        winSfx.Play();
        yield return new WaitForSeconds(2f);
        winScreen.SetActive(true);
       
    }
}

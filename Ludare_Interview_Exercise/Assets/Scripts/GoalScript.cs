using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject fireworks;
    [SerializeField]
    private GameObject sprite;
    private bool hasWon;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(hasWon == false)
            {
                hasWon = true;
                sprite.SetActive(false);
                StartCoroutine("PlayWin");
            }
        }
    }

    private IEnumerator PlayWin()
    {
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        fireworks.SetActive(true);
       
    }
}

using System.Collections;
using UnityEngine;

public class Two_Coin : MonoBehaviour
{

    private Two_GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Applies to every instantiation of the prefab
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Two_GameManager>();
        StartCoroutine(waitForDestruct());
    }


    IEnumerator waitForDestruct() 
    { 
        yield return new WaitForSecondsRealtime(3f);
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            gameManager.AddScore(1);
            Destroy(this.gameObject);
        }
    }
}

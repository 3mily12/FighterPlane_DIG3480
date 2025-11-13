using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEx : MonoBehaviour
{

    public GameObject explosionPrefab;
    
    private GameManagerEx gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerEx>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerControllerEx>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        } else if(whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Three_EnemyTwo : MonoBehaviour
{

    public GameObject explosionPrefab;

    private Vector3 horMovement;
    private Three_GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Three_GameManager>();
    }

    private void Awake()
    {
        horMovement = new Vector3(Random.Range(-1f, 1f), -1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Translate downward at a slow rate than base enemy
        //Translate along the X-axis at a random float between -1.0 and 1.0
        transform.Translate(horMovement * Time.deltaTime * 3f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<Three_Player>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Debug.Log("Hit sucessful");
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}

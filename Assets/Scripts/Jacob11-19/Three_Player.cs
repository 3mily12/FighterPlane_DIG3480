using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Three_Player : MonoBehaviour
{
    //how to define a variable
    //1. access modifier: public or private
    //2. data type: int, float, bool, string
    //3. variable name: camelCase
    //4. value: optional

    public int lives;

    private float playerSpeed;
    private Three_GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    //private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    // ---VERTICAL COLLIDER VARIABLES (Emily)
    // Center of screen
    private float verticalUpLimit = 0.0f;

    // 4 units below screen center (bottom of the game screen for me)
    private float verticalDownLimit = -4.0f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Three_GameManager>();
        lives = 3;
        playerSpeed = 6f;
        gameManager.ChangeLivesText(lives);
        //This is like a create script in GMS

    }

    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        //lives = lives - 1;
        //lives -= 1;
        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            gameManager.GameOver();
        }
    }
    void Shooting()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Pew  Pew" + verticalScreenLimit); // Debug log, try to use a variable that changes often to test

            //spawn bullet at player position with 1 space of padding --> Quaternion basically means that it will spawn in turned the way the player is.
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        //Read the input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);
        //Player leaves the screen horizontally
        if (transform.position.x > horizontalScreenLimit || transform.position.x < -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        //Player leaves the screen vertically --> || represents "or". && is "and".
        //if (transform.position.y > verticalScreenLimit || transform.position.y < -verticalScreenLimit)
        //{
        //transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        //}

        // Vertical colliders (Emily)
        if (transform.position.y < verticalDownLimit)
        {
            transform.position = new Vector3(transform.position.x, verticalDownLimit, 0);
        }
        if (transform.position.y > verticalUpLimit)
        {
            transform.position = new Vector3(transform.position.x, verticalUpLimit, 0);
        }
    }

}

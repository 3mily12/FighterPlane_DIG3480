using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private int weaponType;

    private float playerSpeed;
    private Three_GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;
    private float horizontalScreenLimit = 9.5f;
    //private float verticalScreenLimit = 6.5f;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;

    // ---VERTICAL COLLIDER VARIABLES (Emily)
    // Center of screen
    private float verticalUpLimit = 0.0f;

    // 4 units below screen center (bottom of the game screen for me)
    private float verticalDownLimit = -4.0f;

    private bool poweredUp;
    public bool shieldActive;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Three_GameManager>();
        thrusterPrefab = GameObject.Find("Thruster");
        lives = 3;
        playerSpeed = 6f;
        gameManager.ChangeLivesText(lives);
        weaponType = 1;
        thrusterPrefab.SetActive(false);
        shieldPrefab.SetActive(false);

        shieldActive = false;

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
        if (shieldActive == true) {
            Debug.Log("Protected!");
            gameManager.PlaySound(3);
        }
        else {
            lives--;
            gameManager.ChangeLivesText(lives);
            if (lives == 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                gameManager.PlaySound(6);
                gameManager.GameOver();
            }
        }

    }
    void Shooting()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Pew  Pew" + verticalScreenLimit); // Debug log, try to use a variable that changes often to test

            //spawn bullet at player position with 1 space of padding --> Quaternion basically means that it will spawn in turned the way the player is.

            //amount of bullets instantiated changes if weapon upgrade achieved, previously unimplemented power-up - JACOB
            
            if (weaponType == 2)
            {
                Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
                Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);
                
                gameManager.PlaySound(5);
            }
            else if (weaponType == 3)
            {
                Instantiate(bulletPrefab, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
                Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                Instantiate(bulletPrefab, transform.position + new Vector3(-1, 1, 0), Quaternion.identity);

                gameManager.PlaySound(5);
            }
            else 
            {
                Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);

                gameManager.PlaySound(5);
            }
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

    IEnumerator SpeedPowerDown() 
    { 
        yield return new WaitForSeconds(3f);
        playerSpeed = 6f;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
        thrusterPrefab.SetActive(false);
        poweredUp = false;
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
        poweredUp = false;
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(3f);
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(4);
        shieldActive = false;
        shieldPrefab.SetActive(false);

        poweredUp = false;
    }

    //Power up activates when collided with by player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup" && poweredUp == false) 
        {
            Destroy(collision.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup) 
            {
                case 1:
                    //speed boost
                    playerSpeed = 10f;
                    thrusterPrefab.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    poweredUp = true;
                    gameManager.ManagePowerupText(1);
                    break;

                case 2:
                    //weapon
                    weaponType = 2;
                    StartCoroutine(WeaponPowerDown());
                    poweredUp = true;
                    gameManager.ManagePowerupText(2);
                    break;
                
                case 3:
                    //weapon
                    weaponType = 3;
                    StartCoroutine(WeaponPowerDown());
                    poweredUp = true;
                    gameManager.ManagePowerupText(3);
                    break;

                case 4:
                    //shield
                    shieldActive = true;
                    shieldPrefab.SetActive(true);
                    StartCoroutine(ShieldPowerDown());
                    poweredUp = true; //Remember to set poweredUp to false when creating its Coroutine! || Got it! -Emily
                    gameManager.ManagePowerupText(4);
                    break;
                default:
                    break;
            }
        }
    }

}

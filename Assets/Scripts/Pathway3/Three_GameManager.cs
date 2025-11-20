using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Three_GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject cloudPrefab;
    public GameObject coinPrefab;
    public GameObject gameOverText;
    public GameObject restartText;

    //Enables on Start
    public GameObject powerUpPrefab;
    public GameObject audioPlayer;

    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;
    

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public bool gameOver;

    public int cloudMovement;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        cloudMovement = 1;
        gameOver = false;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        AddScore(0);


        CreateSky();

        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 2, 3);
        InvokeRepeating("CreateCoin", 2, 3);
        
        StartCoroutine(SpawnPowerup());
        //powerUpText.text = "No Powers Yet";
        
    }

    IEnumerator SpawnPowerup() 
    {
        float spawnTime = Random.Range(3,5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    void CreatePowerup() 
    {
        Instantiate(powerUpPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(0 *0.8f, -4.0f), 0), Quaternion.identity);
        Debug.Log("power up created");
    }


    public void ManagePowerupText(int powerupType) 
    {
        switch (powerupType) 
        {
            case 1:
                powerUpText.text = "Speed Boost";
                break;
            case 2:
                powerUpText.text = "Double Shot";
                break;
            case 3:
                powerUpText.text = "Triple Shot";
                break;
            case 4:
                powerUpText.text = "Shield Activated";
                break;
            default:
                powerUpText.text = "None";
                break;
        }
    }

    public void PlaySound(int soundType) 
    {
        AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
        switch (soundType) 
        {
            case 1:
                audioSource.PlayOneShot(powerUpSound);
                break;
            case 2:
                audioSource.PlayOneShot(powerDownSound);
                break;
            default :
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        if (gameOver && Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-4.5f, 4.5f), 6.5f, 0), Quaternion.identity);
    }

    //Coin spawn range matches player movement range - Jacob A.
    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 0f), 0), Quaternion.identity);
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }

    }
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
    }

    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    public void GameOver() 
    { 
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        gameOver = true;
        CancelInvoke();
        cloudMovement = 0;
    }
}
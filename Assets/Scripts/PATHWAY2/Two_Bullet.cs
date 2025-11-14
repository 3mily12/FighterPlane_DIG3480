using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Bullet : MonoBehaviour
{

    public bool goingUp;
    public float speed;

    private Two_GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Two_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (goingUp == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (transform.position.y >= gameManager.verticalScreenSize * 1.25f || transform.position.y <= -gameManager.verticalScreenSize * 1.25f)
        {
            Destroy(this.gameObject);
        }
    }
}

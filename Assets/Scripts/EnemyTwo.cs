using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Vector3 horMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}

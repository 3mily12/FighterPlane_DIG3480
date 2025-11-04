using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Translate downward at a slow rate than base enemy
        //Translate along the X-axis at a random float between -1.0 and 1.0
        transform.Translate(new Vector3(Random.Range(-1.0f, 1.0f), -0.5f, 0) * Time.deltaTime * 3f);
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }
}

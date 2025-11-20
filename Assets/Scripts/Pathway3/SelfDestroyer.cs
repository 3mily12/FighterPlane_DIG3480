using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float destructionTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class Roater : MonoBehaviour
{

    public float rotationSpeed = 50f; // Adjust speed as needed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,1,0);
    }
}

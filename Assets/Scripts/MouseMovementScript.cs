using UnityEngine;

public class MouseMovementScript : MonoBehaviour
{
    Vector3 mousePosition;
    // Update is called once per frame

    private void Start()
    {
        
    }
    void Update()
    {
       mousePosition = Input.mousePosition;
       mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Set the distance from the camera

        transform.position = mousePosition;
    }
}

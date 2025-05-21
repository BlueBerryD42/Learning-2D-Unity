using UnityEngine;

public class CalicoMonoBehaviourScript : MonoBehaviour
{
    private float speed = 6.0f; // Speed of the object
    public Vector3 moveInput; // Vector3 to store movement input

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)
        moveInput.y = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrow keys)

        transform.position += speed * Time.deltaTime * moveInput; // Move the object based on input
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 360, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        Debug.Log("Position: " + transform.position); // Log the position of the object
    }
}

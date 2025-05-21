using UnityEngine;

public class CalicoMonoBehaviourScriptTran : MonoBehaviour
{
    float movementSpeed = 6.0f; // Speed of the object
    float yRotation;
    float x, y;
    Vector3 currPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        currPos = new Vector3(x, 0, y);
        transform.Translate(currPos * movementSpeed * Time.deltaTime, Space.World);
        if (currPos.x != 0)
        {
            yRotation = (currPos.x > 0) ? 0f : 180f;
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
        Debug.Log("Position: " + transform.position); // Log the position of the object
    }
}

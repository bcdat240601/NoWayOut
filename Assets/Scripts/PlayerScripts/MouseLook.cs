using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{    

    [SerializeField] private Transform Camera;
    [Range(50,150)]
    public float Sentivity = 10f;

    public static float UpDownRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        LockMouse();
    }

    // Update is called once per frame
    void Update()
    {
        Mouselook();
    }
    private void Mouselook()
    {
        float MouseX = Input.GetAxis("Mouse X")* Sentivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * Sentivity * Time.deltaTime;

        // look up and down
        UpDownRotation -= MouseY;
        UpDownRotation = Mathf.Clamp(UpDownRotation, -90f,90f);

        Camera.localRotation = Quaternion.Euler(UpDownRotation,0,0);
        // look left and right
        transform.Rotate(Vector3.up * MouseX);
    }
    public void StartCamera()
    {
        this.enabled = true;        
    }
    public void StopCamera()
    {
        this.enabled = false;
    }
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void SetCamera()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        Camera.rotation = Quaternion.Euler(Vector3.zero);
    }
    public void DeadCamera()
    {
        Camera.gameObject.AddComponent<Rigidbody>();
        Camera.GetComponent<BoxCollider>().enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f, runSpeed = 8.0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Running action
        float actualSpeed = Input.GetButton("Jump") ? runSpeed : speed;

        float zMove = Input.GetAxis("Vertical") * Time.deltaTime * actualSpeed;
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * actualSpeed;

        transform.Translate(xMove, 0, zMove);
    }
}

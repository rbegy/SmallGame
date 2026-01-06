using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public bool isGrounded = true;
    public float jumpForce = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ);

        transform.Translate(move * speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.Space) && isGrounded)
        { 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, 1.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}

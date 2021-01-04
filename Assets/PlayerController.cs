using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    public Transform camera;
    private Vector2 movement;

    private bool sameCoordinates;
    
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.y = Input.GetAxisRaw("Vertical");

        if (rb2d.position.y >= 0)
        {
            Debug.Log("Координаты совпали");
            sameCoordinates = true;
        }

        if (sameCoordinates)
        {
            camera.position = new Vector3(camera.position.x, rb2d.position.y, camera.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (movement.y == 1f)
        {
            rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}

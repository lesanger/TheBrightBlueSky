using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    public Transform mainCamera;
    private Vector2 movement;

    private bool passStart;
    private bool canMove = true;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.y = Input.GetAxisRaw("Vertical");

        if (rb2d.position.y >= 0)
        {
            passStart = true;
        }

        if (passStart)
        {
            mainCamera.position = new Vector3(mainCamera.position.x, rb2d.position.y, mainCamera.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (passStart)
        {
            if (canMove)
            {
                rb2d.MovePosition(rb2d.position + new Vector2(0, 1f) * moveSpeed * Time.fixedDeltaTime);
            }
        }
        else if (movement.y == 1f)
        {
            rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Сработал триггер");
        Destroy(other.gameObject);
        canMove = false;
    }
}

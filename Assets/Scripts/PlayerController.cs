using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movement;
    public float moveSpeed = 5f;

    private bool passStart;
    private bool finalMove = false;
    
    public bool canMove = true;
    public bool isReading = false;

    public GameObject memoryPanel;
    public Color color;
    public string text;
    
    public Transform mainCamera;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        memoryPanel.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
    }

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

        if (passStart && !finalMove)
        {
            mainCamera.position = new Vector3(mainCamera.position.x, rb2d.position.y, mainCamera.position.z);
        }

        if (Input.GetButtonDown("Jump") && isReading)
        {
            CloseMemory();
        }
    }

    private void FixedUpdate()
    {
        if (movement.y == 1f && !passStart)
        {
            rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        
        if (passStart && !finalMove)
        {
            if (canMove)
            {
                rb2d.MovePosition(rb2d.position + new Vector2(0, 1f) * moveSpeed * Time.fixedDeltaTime);
            }
        }
        
        if (movement.y == 1f && finalMove)
        {
            rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Memory>(out Memory memory))
        {
            SetNewMemory(other.gameObject);
        }

        if (other.gameObject.name == "EndTrigger")
        {
            EndTrigger();
        }
    }

    private void EndTrigger()
    {
        Debug.Log("Финальный рывок");
        
        finalMove = true;
        mainCamera.gameObject.GetComponent<CameraController>().finalMove = true;
    }

    private void SetNewMemory(GameObject memory)
    {
        color = memory.GetComponent<Memory>().data.color;
        text = memory.GetComponent<Memory>().data.text;

        memoryPanel.SetActive(true);
        Destroy(memory);
        
        memoryPanel.GetComponent<Animation>().Play("Anim_MemoryStart");
    }

    private void CloseMemory()
    {
        memoryPanel.GetComponent<Animation>().Play("Anim_MemoryEnd");
    }
}

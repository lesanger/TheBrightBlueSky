using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movement;
    public float moveSpeed = 5f;

    private bool passStart;
    private bool finalMove = false;

    public GameObject tank;
    public AudioClip tankShot;
    
    public bool canMove = true;
    public bool isReading = false;

    public GameObject memoryPanel;
    public GameObject endGamePanel;
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
        
        if (movement.y == 1f && finalMove && canMove)
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

        if (other.gameObject.name == "StartEndTrigger")
        {
            EndTrigger();
        }
        
        if (other.gameObject.name == "EndTrigger")
        {
            Debug.Log("Выстрел танка и черный экран... Несколько секунд он висит, потом анимация красит экран в синее небо и плавно включает звуки птиц и мира");
            canMove = false;
            tank.GetComponent<AudioSource>().loop = false;
            tank.GetComponent<AudioSource>().PlayOneShot(tankShot);
            endGamePanel.SetActive(true);
        }
    }

    private void EndTrigger()
    {
        Debug.Log("Финальный рывок");
        
        finalMove = true;
        mainCamera.gameObject.GetComponent<CameraController>().finalMove = true;
        tank.GetComponent<AudioSource>().Stop();
    }

    private void SetNewMemory(GameObject memory)
    {
        color = memory.GetComponent<Memory>().data.color;
        text = memory.GetComponent<Memory>().data.text;

        memoryPanel.SetActive(true);
        Destroy(memory);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0);
        
        memoryPanel.GetComponent<Animation>().Play("Anim_MemoryStart");
    }

    private void CloseMemory()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1);
        
        memoryPanel.GetComponent<Animation>().Play("Anim_MemoryEnd");
    }
}

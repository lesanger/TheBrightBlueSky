using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPanel : MonoBehaviour
{
    public Color color;
    public string text;

    public GameObject textGO;

    public void SetNewMemory()
    {
        Debug.Log("Воспоминание началось");

        gameObject.GetComponent<Image>().color = PlayerController.instance.color;
        text = PlayerController.instance.text;
        textGO.GetComponent<Text>().text = text;
        
        PlayerController.instance.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0);
    }

    public void CloseMemory()
    {
        Debug.Log("Воспоминание закончилось");
        
        PlayerController.instance.canMove = true;
        PlayerController.instance.isReading = false;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

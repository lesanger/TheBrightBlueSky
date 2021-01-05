using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    public void SetNewMelody()
    {
        Debug.Log("Плавное затихание ветра...");
        
        PlayerController.instance.SetNewMelody();
    }
}

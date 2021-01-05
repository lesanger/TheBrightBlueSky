using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    public AudioClip birdsSinging;

    public void TurnOnSong()
    {
        Debug.Log("Плавное затихание ветра и песня птиц и природы...");
        //PlayerController.instance.GetComponent<AudioSource>().PlayOneShot(birdsSinging);
    }
}

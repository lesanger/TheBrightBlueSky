using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool finalMove;
    public Transform endGamePos;

    void Update()
    {
        if (finalMove)
        {
            Transform self = gameObject.transform;
            gameObject.transform.position = Vector3.Lerp(self.position, endGamePos.position, 1f * Time.deltaTime);
        }
    }
}

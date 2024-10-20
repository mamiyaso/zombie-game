using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        if(cameraPosition!=null)
        {
            transform.position = cameraPosition.position;
        }
    }
}

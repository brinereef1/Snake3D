using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topCamera : MonoBehaviour
{
    [SerializeField] private Transform snake; 
    [SerializeField] private float followHeight = 15f; 
    [SerializeField] private float smoothTime = 1f; 

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = snake.position + new Vector3(0, followHeight, 0);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}

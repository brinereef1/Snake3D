using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followSnake : MonoBehaviour
{
    [SerializeField] private Transform snake;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, snake.position + new Vector3(0, 50, 0), 100 * Time.deltaTime);
    }
}

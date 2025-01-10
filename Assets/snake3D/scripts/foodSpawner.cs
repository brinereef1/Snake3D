using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodSpawner : MonoBehaviour
{
    public GameObject[] foodPrefab;
    void Start()
    {
        SpawnFood();
    }

    private void Update()
    {
        
    }

    public void SpawnFood()
    {
        int randomFruit = Random.Range(1, foodPrefab.Length);
        Instantiate(foodPrefab[randomFruit], new Vector3(Random.Range(-75, 75), -0.044f, Random.Range(-45, 40)), Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}



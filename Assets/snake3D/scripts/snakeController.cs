using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class snakeController : MonoBehaviour
{
    private float Speed = 10;
    private float SteerSpeed = 180;
    private int Gap = 10;
    [SerializeField] private GameObject BodyPrefab;
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
    [SerializeField] private TextMeshProUGUI score;
    private int points;
    private int highestPoints;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] private GameObject gameover;
    [SerializeField] AudioSource eating;
    [SerializeField] AudioSource hit;

    void Start()
    {
        GrowSnake();
        GrowSnake();
        score.text = "Score : 0";
        eating = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highestPoints = PlayerPrefs.GetInt("HighScore");
        }
        highScore.text = $"HighScore : {highestPoints}";
    }

    void Update()
    {

        transform.position += transform.forward * Speed * Time.deltaTime;

        float steerDirection = Input.GetAxis("Horizontal"); 
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        PositionsHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * Speed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }

    private void GrowSnake()
    {
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        if (BodyParts.Count > 0)
        {
            GameObject lastBodyPart = BodyParts[BodyParts.Count - 1];
            spawnPosition = lastBodyPart.transform.position;
            spawnRotation = lastBodyPart.transform.rotation;
        }
        else
        {
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;
        }

        GameObject body = Instantiate(BodyPrefab, spawnPosition, spawnRotation);
        BodyParts.Add(body);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "food")
        {
            eating.Play();
            GrowSnake();
            Destroy(other.gameObject);
            Debug.Log("eat");
            FindObjectOfType<foodSpawner>().SpawnFood();
            Speed += 0.5f;
            points += 1;
            if (points > highestPoints)
            {
                highestPoints = points;
                PlayerPrefs.SetInt("HighScore", highestPoints);
                PlayerPrefs.Save();
            }
            score.text = $"Score : {points}";
            highScore.text = $"HighScore : {highestPoints}";
            
        }

        if (other.gameObject.tag == "bodypart")
        {
            Time.timeScale = 0;
            gameover.SetActive(true);
            hit.Play();
        }

        if (other.gameObject.tag == "wall")
        {
            Time.timeScale = 0;
            gameover.SetActive(true);
            hit.Play();
        }
    }
}

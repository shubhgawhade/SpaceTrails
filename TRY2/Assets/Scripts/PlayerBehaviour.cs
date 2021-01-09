using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private GameObject explosion;

    private int numberOfEnemies = 1;
    private int numberOfHits = 0;
    public float level = 0;
    public float playerSpeed = 5;
    private float playerRotationSpeed = 150;
    
    private Vector2 playerLocation;
    
    [SerializeField] private Text livesText;

    public static Action<Vector2> Follow;
    public static Action<Vector2, GameObject> CameraLoop;
    public static Action HighScore;

    [SerializeField] private AudioClip playerKilledAudio;


    void Start()
    {
        livesText.text = "14";
        
        DontDestroyOnLoad(this.gameObject);

        EnemyBehaviour.RemoveEnemyCount += NumberOfEnemies;
        EnemyBehaviour.Wo += TimeOut;
    }

    void Update()
    {
        SpawnEnemies();
        Movement();

        playerLocation = transform.position;

        Wrap();
        PlayerPosition();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);

        }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, 0, playerRotationSpeed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, 0, -playerRotationSpeed * Time.deltaTime));
            }
    }

    IEnumerator Delay(float t)
    {
        yield return new WaitForSeconds(t);
    }

    private void SpawnEnemies()
    {
        bool spawned = false;
        while (!spawned)
        {
            Vector2 enemyPosition = new Vector2(UnityEngine.Random.Range(-playerLocation.x - 10f, playerLocation.x + 10f), UnityEngine.Random.Range(-playerLocation.y - 10f, playerLocation.y + 10f));
            if ((enemyPosition - new Vector2(transform.position.x, transform.position.y)).magnitude < 10)
            {
                continue;
            }
            else
            {
                if (numberOfEnemies < level || numberOfEnemies < 2)
                {
                    Instantiate(enemy, enemyPosition, Quaternion.identity);
                    numberOfEnemies++;
                }
                spawned = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            numberOfEnemies--;
            numberOfHits++;
            int lives = 14 - numberOfHits;
            livesText.text = lives.ToString();

            StartCoroutine("Delay", 0.8f);
            
            Vector2 a = new Vector2(transform.localScale.x, transform.localScale.y);
            if (numberOfHits % 2 == 0 && a.x < 0.8)
            {
                if (numberOfHits == 14) //CHANGE THIS NUMBER FOR LIVES
                {
                    if (HighScore != null)
                    {
                        AudioSource.PlayClipAtPoint(playerKilledAudio, transform.position);
                        HighScore();
                        livesText.enabled = false;
                        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
            }
            if (level > 2)
            {
                level-=2;
                
                if(playerSpeed > 4.5 && lives > 7)
                {
                    playerSpeed -= 0.1f;
                }
                else if (playerSpeed < 4.75)
                {
                    playerSpeed = 5;
                }
            }
        }
    }

    private void PlayerPosition()
    {
        if (Follow != null)
        {
            Follow(playerLocation);
        }
    }

    private void Wrap()
    {
        if (CameraLoop != null)
        {
            CameraLoop(playerLocation, this.gameObject);
        }
    }

    private void NumberOfEnemies()
    {
        numberOfEnemies--;
        level += 0.5f;
    }

    private void TimeOut()
    {
        numberOfEnemies--;
    }
}
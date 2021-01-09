using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    
    private float enemySpeed = 3;
    private float enemyRotationSpeed = 150;
    
    private Vector2 pp;
    
    public static Action Score;
    public static Action RemoveEnemyCount;
    public static Action Wo;

    [SerializeField] private AudioClip explosionAudio;
    
    void Start()
    {
        PlayerBehaviour.Follow += Follow;
    }

    public void Follow(Vector2 playerPosition)
    {
        if (this != null)
        {

            StartCoroutine("FollowTime", 10);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            if ((playerPosition - pos).magnitude > 4)
            {
                enemySpeed = 3;
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemySpeed * Time.deltaTime);
            }
            else
            {
                enemySpeed = 4;
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemySpeed * Time.deltaTime);
            }
            Vector2 direction = playerPosition - pos;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, enemyRotationSpeed * Time.deltaTime);

            //float deltaX = transform.position.x - playerPosition.x;
            //float deltaY = transform.position.y - playerPosition.y;

            ////float distance = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)); // sqrt(x^2 + y^2)

            //float angle = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI);
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            pp = playerPosition;
        }
    }

    IEnumerator FollowTime(float t)
    {
        yield return new WaitForSeconds(t);
        No();
        AudioSource.PlayClipAtPoint(explosionAudio, transform.position);
        Destroy(this.gameObject);
        GameObject xp = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            var main = xp.GetComponent<ParticleSystem>().main;
            main.startColor = Color.white;
        }
        PlayerBehaviour.Follow -= Follow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(explosionAudio, pp);
            Destroy(this.gameObject);
            GameObject xp = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, -4), Quaternion.identity);

            var main = xp.GetComponent<ParticleSystem>().main;
            main.startColor = Color.red;

            PlayerBehaviour.Follow -= Follow;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (Score != null)
            {
                Score();
            }

            PlayerBehaviour.Follow -= Follow;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>() != null)
            {
                AudioSource.PlayClipAtPoint(explosionAudio, transform.position);
                Destroy(this.gameObject);
                GameObject xp = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Ok();
            }
        }
    }
    private void Ok()
    {
        if (RemoveEnemyCount != null)
        {
            RemoveEnemyCount();
        }
    }

    private void No()
    {
        if (Wo != null)
        {
            Wo();
        }
    }
}
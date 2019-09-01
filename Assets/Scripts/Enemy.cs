using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed = 10.0f, waitTime = 1.0f;
    [SerializeField]
    private Animator anim;
    public Manager manager;
    private Coroutine movement;

    private void OnEnable()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        anim.SetBool("Explosion", false);
        if (movement != null)
            StopCoroutine(movement);
        movement = StartCoroutine(RandomMovement());
    }

    private IEnumerator RandomMovement()
    {
        float posX = UnityEngine.Random.Range(-3.0f, 3.0f);
        while(transform.position.y >= -6)
        {
            if (Mathf.Abs(transform.position.x - posX) > 0.5f)
                transform.position = new Vector3(transform.position.x - speed * 2.0f * Time.deltaTime * (transform.position.x - posX > 0.5f ? 1 : -1), transform.position.y - 0.5f * speed * Time.deltaTime, 0);
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f * speed * Time.deltaTime, 0);
                if (waitTime > 0)
                    waitTime -= Time.deltaTime;
                else
                {
                    waitTime = UnityEngine.Random.Range(0.5f, 1.5f);
                    posX = UnityEngine.Random.Range(-3.0f, 3.0f);
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (movement != null)
                StopCoroutine(movement);
            GetComponent<CircleCollider2D>().enabled = false;
            manager.SetScore = 1;
            anim.SetBool("Explosion", true);
        }
    }

    public void DisableEnemy()
    {
        anim.SetBool("Explosion", false);
        gameObject.SetActive(false);
    }
}

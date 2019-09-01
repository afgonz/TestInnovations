using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Animator anim;
    private Coroutine move;
    public float bulletSpeed = 1.0f;

    private void OnEnable()
    {
        anim.SetBool("Hit", false);
        if (move != null)
            StopCoroutine(move);
        move = StartCoroutine(StartMoving());
    }

    private IEnumerator StartMoving()
    {
        while(transform.position.y <= 6.0f)
        {
            transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        DisableBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            StopCoroutine(move);
            anim.SetBool("Hit", true);
        }
    }

    public void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}

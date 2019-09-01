using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPref, enemyPref;
    [SerializeField]
    private Manager manager;
    [SerializeField]
    private Sprite[] enemySkin;
    private List<Transform> bullets = new List<Transform> { };
    private List<Transform> enemies = new List<Transform> { };
    public float bulletSpeed = 10.0f, enemySpeed = 5.0f;

    public GameObject NewBullet()
    {
        for (int b = 0; b < bullets.Count; b++)
        {
            if (!bullets[b].gameObject.activeSelf)
                return bullets[b].gameObject;
        }
        GameObject newBullet = Instantiate(bulletPref, transform);
        bullets.Add(newBullet.transform);
        return newBullet;
    }

    public void SetBullet(float x)
    {
        GameObject bullet = NewBullet();
        try
        {
            bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            bullet.transform.position = new Vector3(x, -2.0f, 0.0f);
            bullet.SetActive(true);
        }
        catch { }
    }

    public GameObject NewEnemy()
    {
        for (int e = 0; e < enemies.Count; e++)
        {
            if (!enemies[e].gameObject.activeSelf)
                return enemies[e].gameObject;
        }
        GameObject newEnemy = Instantiate(enemyPref, transform);
        enemies.Add(newEnemy.transform);
        return newEnemy;
    }

    public void SetEnemy()
    {
        GameObject enemy = NewEnemy();
        try
        {
            enemy.transform.position = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 6.0f, 0.0f);
            enemy.GetComponent<Enemy>().speed = enemySpeed;
            enemy.GetComponent<Enemy>().manager = manager;
        }
        catch { }
        enemy.SetActive(true);
    }

    public void StopAll()
    {
        for (int b = 0; b < bullets.Count; b++)
        {
            bullets[b].gameObject.SetActive(false);
        }
        for (int e = 0; e < enemies.Count; e++)
        {
            enemies[e].gameObject.SetActive(false);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panel;
    [SerializeField]
    private Player player;
    private bool right = false, left = false, fire = false;
    [HideInInspector]
    public bool gameActive = false;
    public bool SetRight { set { right = value; } }
    public bool SetLeft { set { left = value; } }
    public bool SetFire { set { fire = value; } }
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] clip;
    [SerializeField]
    private float aspd = 10.0f, speed = 2.0f;
    private Coroutine co_Fire;
    public Pool pool;
    [SerializeField]
    private Text scoreTx, maxScore, gameScore;
    private int score;
    public int SetScore { set { score += value; gameScore.text = "Score: " + score.ToString(); source.PlayOneShot(clip[1]); } }

    public void StartGame()
    {
        score = 0;
        gameActive = true;
        StartCoroutine(CallEnemies());
    }

    private IEnumerator CallEnemies()
    {
        yield return new WaitForSeconds(2.0f);
        while (gameActive)
        {
            pool.SetEnemy();
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void Update()
    {
        if (left)
            player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x - speed * Time.deltaTime, -2, 2), -2.5f, 0.0f);
        if (right)
            player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x + speed * Time.deltaTime, -2, 2), -2.5f, 0.0f);
    }

    public void Shoot()
    {
        Debug.Log("Fire1");
        if (!fire)
        {
            fire = true;
            Debug.Log("Fire2");
            if (co_Fire != null)
                StopCoroutine(co_Fire);
            co_Fire = StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        Debug.Log("Fire3");
        while (fire)
        {
            source.PlayOneShot(clip[0]);
            pool.SetBullet(player.transform.position.x);
            yield return new WaitForSeconds(100.0f / aspd * Time.deltaTime);
        }
    }

    // Enables the actual UI panel
    public void SetView(int view)
    {
        for (int p = 0; p < panel.Length; p++)
        {
            panel[p].SetActive(false);
        }
        panel[view].SetActive(true);
    }

    public void GameOver()
    {
        source.PlayOneShot(clip[2]);
        scoreTx.text = score.ToString();
        int mScore = PlayerPrefs.GetInt("MaxScore");
        PlayerPrefs.SetInt("MaxScore", score > mScore ? score : mScore);
        maxScore.text = "Max Score: " + PlayerPrefs.GetInt("MaxScore").ToString();
        panel[0].SetActive(true);
        panel[1].SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}

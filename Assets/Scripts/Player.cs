using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Manager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {            
            manager.gameActive = false;
            anim.SetBool("Explosion", true);
            Debug.Log("Colisión!");
        }
    }

    public void EndGame()
    {
        manager.pool.StopAll();
        manager.GameOver();
        anim.SetBool("Explosion", false);
    }
}

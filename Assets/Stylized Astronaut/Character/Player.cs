using System;
using UnityEngine;
using System.Collections;
using Leap;

public class Player : MonoBehaviour
{
    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

    [SerializeField] private GameObject losseText;

    public Action DieEvent;

    public bool IsDead = false;

    void Update()
    {
    }

    public void OnObstacleCollision()
    {
        Die();
    }

    private void Die()
    {
        if (DieEvent != null)
            DieEvent.Invoke();

        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {


        if (collision.transform.tag != "Respawn")
        {
            Die();
            IsDead = true;
            losseText.SetActive(true);
        }


    }
}
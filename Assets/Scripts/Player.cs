using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    Rigidbody2D rb;
    public float moveSpeed, bounceForce;
   

    private void Awake()
    {
        HeartManager.Instance.UpdateHearts(HeartManager.Instance.CurrentHealth);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MoveTowardsCenter();
    }

    void MoveTowardsCenter()
    {
        Vector3 targetPosition = new Vector3(0, transform.position.y, transform.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 bounceDirection = collision.contacts[0].normal; //направление отталкивания
        rb.AddForce(bounceDirection * bounceForce * Time.fixedDeltaTime, ForceMode2D.Impulse);

        if (collision.gameObject.tag == "damage")
            HeartManager.Instance.CurrentHealth--;

        if (collision.gameObject.tag == "heal")
            HeartManager.Instance.CurrentHealth++;

        if (collision.gameObject.tag == "lose" || HeartManager.Instance.CurrentHealth <= 0)
            Die();


        ScoreManager.Instance.UpdateScore(1);
        HeartManager.Instance.UpdateHearts(HeartManager.Instance.CurrentHealth);
    }



    private void Die()
    {
        //GameManager.Instance.StartLevel();
    }

}

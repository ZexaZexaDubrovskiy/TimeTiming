using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    Rigidbody2D rb;
    public float moveSpeed, bounceForce, _oldX = 0;
    private bool _isMouseButtonDown, _isJump;


    private void Awake()
    {
        HeartManager.Instance.UpdateHearts(HeartManager.Instance.CurrentHealth);
        rb = GetComponent<Rigidbody2D>();
        _isJump = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _isMouseButtonDown = true;
        CheckPlayerPosition();
    }

    private void FixedUpdate()
    {
        if (_isMouseButtonDown && _isJump)
        {
            Movement();
            _isMouseButtonDown = _isJump = false;
        }

    }

    void Movement()
    {
        float horizontalDirection = transform.position.x >= 0 ? 1f : -1f;
        Vector3 direction = new Vector3(horizontalDirection, 0, 0);
        rb.velocity = direction * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isJump = true;
        Vector2 bounceDirection = collision.contacts[0].normal; //направление отталкивания
        rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

        if (collision.gameObject.tag == "damage")
            HeartManager.Instance.CurrentHealth--;

        if (collision.gameObject.tag == "heal")
            HeartManager.Instance.CurrentHealth++;

        if (collision.gameObject.tag == "dead" || HeartManager.Instance.CurrentHealth <= 0)
            Die();

        ScoreManager.Instance.UpdateScore(1);
        HeartManager.Instance.UpdateHearts(HeartManager.Instance.CurrentHealth);
    }

    private void CheckPlayerPosition()
    {
        if (transform.position.x >= 2 || transform.position.x <= -2)
            Die();
    }

    private void Die()
    {
        GameManager.Instance.StartLevel();
    }

}

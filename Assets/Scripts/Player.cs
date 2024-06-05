using UnityEngine;

public class Player : Singleton<Player>
{
    private Rigidbody2D _rb;
    private SoundManager _soundManager;
    private HeartManager _heartManager;
    private ScoreManager _scoreManager;
    private GameManager _gameManager;
    private TrailRenderer _trailRenderer;

    public float moveSpeed;
    public float bounceForce;
    private bool _isMouseDown;
    private bool _canJump;

    private void Awake()
    {
        _heartManager = HeartManager.Instance;
        _scoreManager = ScoreManager.Instance;
        _gameManager = GameManager.Instance;

        _heartManager.UpdateHearts(_heartManager.CurrentHealth);
        _rb = GetComponent<Rigidbody2D>();
        _soundManager = GetComponent<SoundManager>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _canJump = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
        }

        CheckPlayerOutOfBounds();
    }

    private void FixedUpdate()
    {
        if (_isMouseDown && _canJump)
        {
            JumpMovement();
            _isMouseDown = _canJump = false;
        }
    }

    void JumpMovement()
    {
        float horizontalDirection = transform.position.x >= 0 ? 1f : -1f;
        Vector3 direction = new Vector3(horizontalDirection, 0, 0);
        _rb.velocity = direction * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _canJump = true;
        Vector2 bounceDirection = collision.contacts[0].normal;
        _rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);

        HandleCollisionWithTags(collision);
        _scoreManager.UpdateScore(1);
        _heartManager.UpdateHearts(_heartManager.CurrentHealth);
    }

    private void HandleCollisionWithTags(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            _soundManager.PlaySound(0, pitchMin: 0.9f, pitchMax: 1.1f, volume: 0.45f);
        }
        else if (collision.gameObject.CompareTag("damage"))
        {
            _heartManager.CurrentHealth--;
            _soundManager.PlaySound(1, volume: 3f);
        }
        else if (collision.gameObject.CompareTag("heal"))
        {
            _heartManager.CurrentHealth++;
            _soundManager.PlaySound(2);
        }
        else if (collision.gameObject.CompareTag("dead") || _heartManager.CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void CheckPlayerOutOfBounds()
    {
        if (transform.position.x >= 2 || transform.position.x <= -2)
            Die();
    }

    private void Die()
    {
        _soundManager.PlaySound(3);
        _gameManager.StartLevel();
    }
}

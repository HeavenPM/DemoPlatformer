using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerHealth : MonoBehaviour
{
    public int Health => _health;

    [SerializeField] private int _health = 3;
    [SerializeField] private float _pushBackForce;

    private Renderer _renderer;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_health <= 0) EventManager.OnGameOver();
    }

    private void OnEnable()
    {
        EventManager.PlayerHit += EventHit;
        EventManager.PlayerHeal += EventHeal;
    }

    private void OnDisable()
    {
        EventManager.PlayerHit -= EventHit;
        EventManager.PlayerHeal -= EventHeal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DeathZone>()) EventManager.OnGameOver();
    }

    private void EventHit()
    {
        if (!GetComponent<PlayerAttack>().IsAttacking)
        {
            _health--;
            EventManager.OnHealthUpdated(_health);
            StartCoroutine(ChangeColorForDuration(0.5f));
            Vector2 pushBackDirection = (GetComponent<PlayerMovement>().IsLookingRight ?
                new Vector2(-1f, 1f).normalized :
                new Vector2(1f, 1f).normalized);
            _rigidbody2D.AddForce(pushBackDirection * _pushBackForce, ForceMode2D.Impulse);
        }
    }

    private void EventHeal()
    {
        if (_health < 3) _health++;
        EventManager.OnHealthUpdated(_health);
    }

    private IEnumerator ChangeColorForDuration(float duration)
    {
        Color originalColor = _renderer.material.color;
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(duration);
        _renderer.material.color = originalColor;
    }
}

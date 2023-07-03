using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerAttack : MonoBehaviour
{
    public bool IsAttacking => _isAttacking;

    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private TrailRenderer _trailRenderer;

    private bool _isAttacking = false;
    private bool _canAttack = true;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && _canAttack)
        {
            StartCoroutine(Attack());
        }

        if (_isAttacking)
        {
            Vector2 attackDirection = Vector2.left;

            if (GetComponent<PlayerMovement>().IsLookingRight)
            {
                attackDirection = Vector2.right;
            }           
            _rigidbody2D.AddForce(attackDirection * _attackForce * Time.deltaTime);
        }
    }

    private IEnumerator Attack()
    {
        _canAttack = false;
        _isAttacking = true;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        _rigidbody2D.freezeRotation = true;
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_attackDuration);
        _trailRenderer.emitting = false;
        _rigidbody2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        _rigidbody2D.freezeRotation = true;
        _isAttacking = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}

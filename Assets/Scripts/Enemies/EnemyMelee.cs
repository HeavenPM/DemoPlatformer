using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMelee : Enemy
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _pushBackForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private string[] _animations;

    private float _nextAttackTime;
    private bool _isAlive = true;
    private bool _canMove = true;

    private Transform _player;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (_isAlive)
        {
            if (IsPlayerDetected())
            {
                if (!CanAttack())
                {
                    if(_canMove) MoveTowardsPlayer();
                }
                else
                {
                    Attack();
                    _nextAttackTime = Time.time + AttackCooldown;
                }
            }
            else
            {
                playAnimation("isIdle");
            }
        }
    }

    protected override void Attack()
    {
        playAnimation("isAttack");
        _canMove = false;
        EventManager.OnPlayerHit();
        Invoke("WaitAfterAttack", 1f);
    }

    private void WaitAfterAttack()
    {
        _canMove = true;
        Vector2 pushBackDirection = (_player.GetComponent<PlayerMovement>().IsLookingRight ?
                Vector2.right :
                Vector2.left);
        _rigidbody2D.AddForce(pushBackDirection * _pushBackForce, ForceMode2D.Impulse);
    }

    protected override void Die()
    {
        _isAlive = false;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        _rigidbody2D.freezeRotation = true;
        playAnimation("isDead");
        Invoke("CleanAfterDie", 1f);
    }

    private void CleanAfterDie()
    {
        Destroy(gameObject);
    }

    private bool CanAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        return distanceToPlayer < _attackRange && Time.time >= _nextAttackTime;
    }

    private bool IsPlayerDetected()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        return distanceToPlayer < DetectionRange;
    }

    private void MoveTowardsPlayer()
    {
        playAnimation("isWalk");
        Vector3 direction = _player.position - transform.position;
        direction.Normalize();

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        transform.Translate(direction * _movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckPlayerAttacking(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckPlayerAttacking(collision);
    }

    private void CheckPlayerAttacking(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerAttack>()
            && _player.GetComponent<PlayerAttack>().IsAttacking)
        {
            Die();
        }
    }

    private void playAnimation(string currentAnimation)
    {
        for (int i = 0; i < _animations.Length; i++)
        {
            if (_animations[i] != currentAnimation)
            {
                _animator.SetBool(_animations[i], false);
            }
            else
            {
                _animator.SetBool(_animations[i], true);
            }
        }
    }
}

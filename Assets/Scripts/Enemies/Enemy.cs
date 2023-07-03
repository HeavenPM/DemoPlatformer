using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class Enemy : MonoBehaviour
{
    public float DetectionRange => _detectionRange;
    public float AttackCooldown => _attackCooldown;

    [SerializeField] private float _detectionRange;
    [SerializeField] private float _attackCooldown;

    protected abstract void Attack();
    protected abstract void Die();
}

using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string[] _animations;
    [SerializeField] private bool _isMainMenu = false;

    private void Update()
    {
        if (!_isMainMenu) CheckAnimationState();    
    }

    private void CheckAnimationState()
    {
        if (GetComponent<PlayerAttack>().IsAttacking)
        {
            playAnimation("isAttack");
        }
        else
        {
            if (GetComponent<PlayerMovement>().IsMoving)
            {
                playAnimation("isRun");
            }
            else
            {
                playAnimation("isIdle");
            }
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

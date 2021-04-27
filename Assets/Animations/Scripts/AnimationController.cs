using UnityEngine;

public class AnimationController
{
    private Animator _playerAnimation;

    public AnimationController(Animator animator)
    {
        _playerAnimation = animator;
    }

    public void ChangeAnimationState(Vector2 motion)
    {
        _playerAnimation.SetFloat("x", motion.x);
        _playerAnimation.SetFloat("y", motion.y);
    }
}
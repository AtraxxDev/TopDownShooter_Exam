using UnityEngine;

public class PlayerAnimations : MonoBehaviour,IAnimation
{

    private static readonly int AnimShoot = Animator.StringToHash("shoot");
    private static readonly int AnimWalk = Animator.StringToHash("walk");
    private static readonly int AnimMissile = Animator.StringToHash("missile");
    private static readonly int AnimJump = Animator.StringToHash("jump");
    private static readonly int AnimDie = Animator.StringToHash("die");

    public Animator animator;



    public void PlayAnimation(PlayerAnimationState state, bool value = true)
    {
        switch (state)
        {
            case PlayerAnimationState.Walk:
                animator.SetBool(AnimWalk, value);
                break;

            case PlayerAnimationState.Shoot:
                animator.SetBool(AnimShoot,value);
                break;

            case PlayerAnimationState.Missile:
                animator.SetTrigger(AnimMissile);
                break;

            case PlayerAnimationState.Jump:
                animator.SetBool(AnimJump, value);
                break;

            case PlayerAnimationState.Die:
                animator.SetTrigger(AnimDie);
                break;

            default:
                break;
        }
    }

    public bool IsPlaying(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;
    }

}


public enum PlayerAnimationState
{
    Idle,
    Walk,
    Shoot,
    Missile,
    Jump,
    Die
}

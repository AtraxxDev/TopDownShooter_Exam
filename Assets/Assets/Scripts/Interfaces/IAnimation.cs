public interface IAnimation
{
    void PlayAnimation(PlayerAnimationState state, bool value = true);
    bool IsPlaying(string aniamtionName);
}
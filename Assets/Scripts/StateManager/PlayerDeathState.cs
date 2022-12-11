public class PlayerDeathState : IState
{
    public void OnEntryState(PlayerStateManager playerStateManager)
    {
        playerStateManager.playerAnimator.SetTrigger("Death");
    }
}

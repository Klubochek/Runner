using UnityEngine;

public class PlayerJumpingState : IState
{
    public void OnEntryState(PlayerStateManager playerStateManager)
    {
        Debug.Log("PlayerJumping");
        playerStateManager.playerAnimator.SetTrigger("Jumping");
    }
}

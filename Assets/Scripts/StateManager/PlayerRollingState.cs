using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollingState : IState
{
    public void OnEntryState(PlayerStateManager playerStateManager)
    {
        playerStateManager.playerAnimator.SetTrigger("Slide");
    }
}

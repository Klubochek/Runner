using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{

    public void OnEntryState(PlayerStateManager playerStateManager)
    {
        playerStateManager.playerAnimator.SetTrigger("Idle");
    }
}

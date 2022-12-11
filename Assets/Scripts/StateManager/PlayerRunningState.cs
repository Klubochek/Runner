using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : IState
{
    public void OnEntryState(PlayerStateManager playerStateManager)
    {
        Debug.Log("Run");
        playerStateManager.playerAnimator.SetTrigger("Run");
    }

}

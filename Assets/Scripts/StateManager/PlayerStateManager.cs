using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public IState currentState;
    private PlayerIdleState idleState = new PlayerIdleState();
    public Animator playerAnimator;

    public static PlayerStateManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentState = idleState;
        currentState.OnEntryState(this);
    }
    public void SwitchState(IState state)
    {
        currentState = state;
        currentState.OnEntryState(this);
    }

}

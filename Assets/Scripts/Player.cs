using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Score score;
    [SerializeField] private GameObject menuBox;
    public static Player Instance;
    public bool isJumping = false;
    public bool isRunning = false;
    [SerializeField] private PlayerController pc;

    private void Awake()
    {
        Instance = this;
    }
    public void RestartGame()
    {
        score.ResetScore();
        transform.position = new Vector3(0, 0, -40);
        PlayerIdle();
    }
    public void PlayerRun()
    {
        if (isRunning == false)
        {
            score.StartCountScore();
            PlayerStateManager.Instance.SwitchState(new PlayerRunningState());
            isRunning = true;
        }
    }
    public void PlayerIdle()
    {
        isRunning = false;
        PlayerStateManager.Instance.SwitchState(new PlayerIdleState());
    }
    public void PlayerRoll()
    {

        StartCoroutine(Roll());

    }
    public void PlayerJump()
    {
        Debug.Log("Player is jumping");
        PlayerStateManager.Instance.SwitchState(new PlayerJumpingState());

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && pc.isRolling == false)
        {
            Debug.Log("collision with obstacle");
            Death();
            score.StopCountScore();
            SwipeController.Instance.enabled = false;
            menuBox.SetActive(true);


        }
        else if (other.gameObject.CompareTag("LowObstacle"))
        {
            Debug.Log("collision with obstacle");
            Death();
            score.StopCountScore();
            SwipeController.Instance.enabled = false;
            menuBox.SetActive(true);

        }
    }
    private IEnumerator Roll()
    {

        PlayerStateManager.Instance.SwitchState(new PlayerRollingState());
        yield return new WaitForSeconds(0.5f);
        isRunning = false;
    }
    private void Death()
    {
        RoadGenerator.Instance.speed = 0;
        PlayerStateManager.Instance.SwitchState(new PlayerDeathState());
    }
}

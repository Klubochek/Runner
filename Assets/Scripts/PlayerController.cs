using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    private Coroutine movingCoroutine;
    private bool isJumping = false;
    public bool isRolling = false;
    private float pointFinish = 0;
    private int laneOffset = 5;
    bool isMoving = false;
    float jumpPower = 25;
    float jumpGravity = -60;
    float realGravity = -9.8f;
    private float pointStart;

    private void Start()
    {
        StartCoroutine(SwipeActivation());

    }

    private IEnumerator SwipeActivation()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("SwipeActivated");
        SwipeController.Instance.enabled = true;
        SwipeController.Instance.SwipeEvent += ChangePlayerPosition;
    }

    private void ChangePlayerPosition(bool[] swipes)
    {
        if (swipes[(int)SwipeController.Direction.Left] && pointFinish > -laneOffset)
        {
            MoveHorizontal(-laneOffset);
        }
        if (swipes[(int)SwipeController.Direction.Right] && pointFinish < laneOffset)
        {
            MoveHorizontal(laneOffset);
        }
        if (swipes[(int)SwipeController.Direction.Up] && isJumping == false)
        {
            Player.Instance.PlayerJump();
            Jump();
        }
        if (swipes[(int)SwipeController.Direction.Down] && isRolling == false)
        {

            StartCoroutine(Roll());
        }
    }
    private IEnumerator Roll()
    {
        isRolling = true;
        Player.Instance.PlayerRoll();
        yield return new WaitForSeconds(0.5f);
        isRolling = false;
    }


    private void MoveHorizontal(int speed)
    {
        pointStart = pointFinish;
        pointFinish += Mathf.Sign(speed) * laneOffset;
        if (isMoving)
        {
            StopCoroutine(movingCoroutine);
            isMoving = false;
        }
        movingCoroutine = StartCoroutine(MoveCoroutine(speed * 10));
    }
    IEnumerator MoveCoroutine(float speed)
    {
        isMoving = true;
        while (Mathf.Abs(pointStart - transform.position.x) < laneOffset)
        {
            Debug.Log("PlayerIsMoving");
            yield return new WaitForFixedUpdate();
            playerRb.velocity = new Vector3(speed, playerRb.velocity.y, 0);
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(pointStart, pointFinish), Mathf.Max(pointStart, pointFinish));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        playerRb.velocity = Vector3.zero;
        transform.position = new Vector3(pointFinish, transform.position.y, transform.position.z);
        isMoving = false;
    }
    
    private void Jump()
    {
        isJumping = true;
        playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, jumpGravity, 0);
        StartCoroutine(StopJumpCoroutine());
    }
    IEnumerator StopJumpCoroutine()
    {
        do
        {
            yield return new WaitForSeconds(0.02f);
        } while (playerRb.velocity.y != 0);
        isJumping = false;
        Physics.gravity = new Vector3(0, realGravity, 0);
    }
}

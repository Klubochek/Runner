using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 startPos = new Vector3(0, 9, -20);
    [SerializeField] private GameObject player;
    void Update()
    {
        transform.position = player.transform.position + startPos;
    }
}

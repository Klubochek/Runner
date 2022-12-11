using UnityEngine;

public class TileDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            Debug.Log("TileDestroyer1");
            RoadGenerator.Instance.ReinstallPrefab();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            Debug.Log("TileDestroyer2");
            RoadGenerator.Instance.ReinstallPrefab();
        }
    }
}

using UnityEngine;

public class CoinLine : MonoBehaviour
{
    [SerializeField] private GameObject[] coins;

    private void OnEnable()
    {
        foreach (GameObject go in coins)
        {
            go.SetActive(true);
        }
    }
}

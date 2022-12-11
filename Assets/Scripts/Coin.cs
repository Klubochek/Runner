using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Action OnPickUpCoin;
    private void Start()
    {
        OnPickUpCoin += Bank.instance.AddCoin;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            OnPickUpCoin?.Invoke();
        }
    }
}

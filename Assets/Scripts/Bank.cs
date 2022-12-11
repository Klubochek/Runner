using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private int balance = 0;
    public static Bank instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddCoin()
    {
        balance++;
        balanceText.text = $"Balance:{balance}";
    }
}

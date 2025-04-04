using System;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int coins = 0;

    public delegate void CoinsUpdate(int newAmount);

    public CoinsUpdate OnCoinsUpdated;

    public TMP_Text coinsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Evitar que se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HasEnoughCoins(int amount) => coins >= amount;


    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinsUpdated?.Invoke(coins);
        UpdateCoinDisplay();
    }

    public void SpendCoins(int amount)
    {
        if (coins < amount) return;
        coins -= amount;
        UpdateCoinDisplay();
        OnCoinsUpdated?.Invoke(coins);
    }

    private void UpdateCoinDisplay()
    {
        coinsText.text = ("Coins: " + coins.ToString());
        Debug.Log("Coins: " + coins);
    }
}

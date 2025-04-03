using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Instancia estática de la clase (Singleton)
    public static CoinManager Instance { get; private set; }

    public int coins = 0;

    public event Action OnCoinsChanged;

    // Método que se llama al iniciar el juego para asegurarse de que solo haya una instancia
    private void Awake()
    {
        // Verificar si ya existe una instancia
        if (Instance == null)
        {
            // Si no existe, asignar esta instancia
            Instance = this;
            DontDestroyOnLoad(gameObject); // Evitar que se destruya al cambiar de escena
        }
        else
        {
            // Si ya existe, destruir este objeto
            Destroy(gameObject);
        }
    }

    // Método para agregar monedas
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinDisplay();
    }

    // Mostrar monedas en consola (se puede mejorar mostrando en UI)
    private void UpdateCoinDisplay()
    {
        Debug.Log("Coins: " + coins);
    }
}

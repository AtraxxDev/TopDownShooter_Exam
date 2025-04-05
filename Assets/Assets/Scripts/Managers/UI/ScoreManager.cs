using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score = 0;

    public event Action OnScoreChenge;

    public TMP_Text scoreText;


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

    public void IncreaseScore()
    {
        score += 10;
        OnScoreChenge?.Invoke();
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = ("Score: " + score.ToString());
        Debug.Log("Score: " + score);
    }
}

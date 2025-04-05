using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private UpgradeCard[] upgradeCards;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] public WaveSystem waveSystem;
    [SerializeField] private TMP_Text missileCountText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text waveNumberText;
    [SerializeField] private TMP_Text waveNumberText_Gameplay;

    private void OnEnable()
    {
        waveSystem.OnWaveCompleted += OpenShop;

        playerHealth.OnPlayerDie += ShowGameOver;

        waveSystem.OnWaveStarted += CloseShop;

    }


    private void OnDisable()
    {
        waveSystem.OnWaveCompleted -= OpenShop;
        waveSystem.OnWaveStarted -= CloseShop;

        playerHealth.OnPlayerDie -= ShowGameOver;
    }

    private void Update()
    {
        missileCountText.text = $"Misiles: {playerAttack.currentMissiles} / {playerAttack.missileCapacity}";
    }

   

   
    public void OpenShop(int _)
    {
        shopPanel.SetActive(true);
        playerController.isOpenShop = true;
        foreach (var card in upgradeCards)
        {
            card.UpdateUI();
        }
    }

    public void CloseShop(int _)
    {
        shopPanel.SetActive(false);
        playerController.isOpenShop = false;
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        waveNumberText.text = $"Oleada alcanzada: {waveSystem.waveId}";
        AudioManager.Instance.StopMusic();
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);
    }
}

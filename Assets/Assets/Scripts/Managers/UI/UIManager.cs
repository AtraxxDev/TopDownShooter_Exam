using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel; // Panel de la tienda
    [SerializeField] private UpgradeCard[] upgradeCards; // Cartas de mejora
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerController playerController;


    [SerializeField] private WaveSystem waveSystem;


    private void OnEnable()
    {
        waveSystem.OnWaveCompleted += OpenShop;
        waveSystem.OnWaveStarted += CloseShop;


    }

    private void OnDisable()
    {
        waveSystem.OnWaveCompleted -= OpenShop;
        waveSystem.OnWaveStarted += CloseShop;

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
}

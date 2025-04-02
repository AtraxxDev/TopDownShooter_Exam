using UnityEngine;

[CreateAssetMenu(fileName = "StatsDataSO", menuName = "Scriptable Objects/StatsDataSO")]
public class StatsDataSO : ScriptableObject
{
    [Header("General Stats")]
    public float maxHealth = 100f;
    public float moveSpeed = 5f;
    public float attackRate = 0.2f;

    public float shieldCapacity = 50f;
    public float shieldRegenRate = 2f;

}

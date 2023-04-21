using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveProperty", menuName = "WaveProperty")]
public class WaveProperty : ScriptableObject, ISerializationCallbackReceiver
{
    [System.NonSerialized]
    public int NormalEnemiesCount, FastEnemiesCount, SlowEnemiesCount;


    public float TimebetweenSpawn, RewardMultiplier, HealthMultiplier;

    [SerializeField]
    private int m_normalEnemiesInitialCount, m_fastEnemiesInitialCount, m_slowEnemiesInitialCount;

    public void OnAfterDeserialize()
    {
        NormalEnemiesCount = m_normalEnemiesInitialCount;
        FastEnemiesCount = m_fastEnemiesInitialCount;
       SlowEnemiesCount = m_slowEnemiesInitialCount;
    }

    public void OnBeforeSerialize()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveProperty", menuName = "WaveProperty")]
public class WaveProperty : ScriptableObject, ISerializationCallbackReceiver
{
    [System.NonSerialized]
    public int normalEnemiesCount, fastEnemiesCount, slowEnemiesCount;


    public float timeBetweenSpawn, rewardMultiplier, healthMultiplier;

    [SerializeField]
    private int m_normalEnemiesInitialCount, m_fastEnemiesInitialCount, m_slowEnemiesInitialCount;

    public void OnAfterDeserialize()
    {
        normalEnemiesCount = m_normalEnemiesInitialCount;
        fastEnemiesCount = m_fastEnemiesInitialCount;
       slowEnemiesCount = m_slowEnemiesInitialCount;
    }

    public void OnBeforeSerialize()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> enemyPrefabsList;
    [SerializeField]
    public List<WaveProperty> waveList;

    [SerializeField]
    public PathFinder pathFinder;

    [SerializeField]
    public WorldCreator worldCreator;

    [SerializeField]
    private GameEventEnemyDied m_gameEventEnemyDied;

    private List<TileEntity> m_currentPath;

    private bool m_waveActive = false;
    private int m_currentEnemiesInWaveCount = 0;
    private float timer = 0;
    private GameLogic m_logic;
    private List<Enemy> m_currentAliveEnemies = new List<Enemy>();
    private GameLogic m_gameLogic;

    public void Awake()
    {
        m_gameLogic = FindFirstObjectByType<GameLogic>();
    }

    void Start()
    {
        m_logic = FindObjectOfType<GameLogic>();
    }

    public int GetNumberOfEnemiesInWave()
    {
        return waveList[0].normalEnemiesCount + waveList[0].fastEnemiesCount + waveList[0].slowEnemiesCount; ;
    }

    public void ActivateWave()
    {
        if (m_currentEnemiesInWaveCount == 0)
        {
            m_waveActive = !m_waveActive;
            if (m_waveActive)
            {
                m_currentEnemiesInWaveCount = GetNumberOfEnemiesInWave();
            }
        }
    }

    void FixedUpdate()
    {
        CheckForSpawn();
    }

    private void CheckForSpawn()
    {
        if (m_waveActive)
        {
            timer += Time.deltaTime;
            if (timer > waveList[0].timeBetweenSpawn)
            {
                if (m_currentEnemiesInWaveCount > 0)
                {
                    if (waveList[0].normalEnemiesCount > 0)
                    {
                        SpawnEnemy(enemyPrefabsList[0]);
                    }
                    else if (waveList[0].fastEnemiesCount > 0)
                    {
                        SpawnEnemy(enemyPrefabsList[1]);
                    }
                    else if (waveList[0].slowEnemiesCount > 0)
                    {
                        SpawnEnemy(enemyPrefabsList[2]);
                    }
                    timer = 0;
                }
                else
                {
                    timer = 0;
                }
            }
        }
    }

    private void SpawnEnemy(GameObject pEnemyPrefab)
    {
        m_currentEnemiesInWaveCount--;
        m_logic.UpdateEnemyCount(m_currentEnemiesInWaveCount);
        WaveProperty waveInformation = waveList[0];
        var type = pEnemyPrefab.GetComponentInChildren<Enemy>();
        if (type.GetType() == typeof(NormalEnemy))
        {
            waveInformation.normalEnemiesCount--;
        }
        if (type.GetType() == typeof(FastEnemy))
        {
            waveInformation.fastEnemiesCount--;
        }
        if (type.GetType() == typeof(SlowEnemy))
        {
            waveInformation.slowEnemiesCount--;
        }
        m_currentPath = pathFinder.GeneratePath(); //Get last path if not changed.
        GameObject enemyGameObject = Instantiate(pEnemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Enemy enemyEntity = enemyGameObject.GetComponentInChildren<Enemy>();
        enemyEntity.path = m_currentPath;
        enemyEntity.SetGameObject(enemyGameObject);
        m_currentAliveEnemies.Add(enemyEntity);
    }

    public int GetNumberOfWaves()
    {
        return waveList.Count;
    }

    public void NotifyDeath(Enemy pDeadEnemy)
    {

        //if (m_currentAliveEnemies.Contains(pDeadEnemy))
        //{
        //    m_currentAliveEnemies.Remove(pDeadEnemy);
        //}
        //if (m_currentAliveEnemies.Count == 0 && m_currentEnemiesInWaveCount == 0)
        //{
        //    //Wave ended
        //    waveList.RemoveAt(0);
        //    m_waveActive = false;
        //    m_gameLogic.WaveCleared();
        //    Debug.Log("Wave cleared");
        //}
    }



}

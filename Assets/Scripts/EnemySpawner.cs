using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> m_enemyPrefabs;

    [SerializeField]
    public PathFinder m_pathFinder;

    [SerializeField]
    public WorldCreator m_worldCreator;

    private List<GameObject> m_gameTiles;
    private List<TileEntity> m_currentPath;

    private bool m_waveActive = false;
    private int m_waveCount = 0, m_waveMax = 10;
    private float m_secondsBetweenSpawn = 2.0f, timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        m_gameTiles = m_worldCreator.GetGameObjectsTiles();
    }

    public int GetNumberOfEnemiesInWave()
    {
        return m_waveMax;
    }

    public void ActivateWave()
    {
        if (m_waveCount == 0)
        {
            Debug.Log("Activating wave");
            m_waveActive = !m_waveActive;
            if (m_waveActive)
            {
                m_waveCount = m_waveMax;
            }
        }
    }

    void FixedUpdate()
    {
        CheckForSpawn();
    }

    private void CheckForSpawn()
    {
        if(m_waveActive)
        {
            timer += Time.deltaTime;
            if (m_waveCount > 0)
            {
                if(timer > m_secondsBetweenSpawn)
                { 
                    SpawnEnemy(m_enemyPrefabs[0]);
                    timer = 0;
                }
            } else
            {
                timer = 0;
            }
        }
    }

    private void SpawnEnemy(GameObject pEnemyPrefab)
    {
        Debug.Log("Spawning enemy");
        m_waveCount--;
        m_currentPath = m_pathFinder.GeneratePath(); //Get last path if not changed.
        GameObject enemyGameObject = Instantiate(pEnemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        EnemyEntity enemyEntity = enemyGameObject.GetComponentInChildren<EnemyEntity>();
        enemyEntity.secondsPerTile = 2.0f;
        enemyEntity.path = m_currentPath;
        enemyEntity.health = 5;
        enemyEntity.SetGameObject(enemyGameObject);
        enemyEntity.SetAllowedToMove(true);
    }

    public void SetSecondsBetweenSpawn(float pSeconds)
    {
        m_secondsBetweenSpawn = pSeconds;
    }

    

}

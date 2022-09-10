using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject m_enemyPrefab;

    [SerializeField]
    public PathFinder m_pathFinder;

    [SerializeField]
    public WorldCreator m_worldCreator;

    private List<GameObject> m_gameTiles;
    private List<TileEntity> m_currentPath;


    // Start is called before the first frame update
    void Start()
    {
        m_gameTiles = m_worldCreator.GetGameObjectsTiles();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_currentPath = m_pathFinder.GeneratePath();
            GameObject enemyGameObject = Instantiate(m_enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            EnemyEntity enemyEntity = enemyGameObject.GetComponent<EnemyEntity>();
            enemyEntity.secondPerTile = 2.0f;
            enemyEntity.path = m_currentPath;
            enemyEntity.SetGameObject(enemyGameObject);
            enemyEntity.SetAllowedToMove(true);
        }
    }
}

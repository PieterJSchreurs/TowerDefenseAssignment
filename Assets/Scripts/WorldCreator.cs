using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    [SerializeField]
    public int m_rows, m_columns;
    [SerializeField]
    public GameObject m_tilePrefab, m_cameraObject;
    [SerializeField]
    public float m_offsetBetweenTiles;
    [SerializeField]
    private PathFinder m_pathFinder;

    private TileEntity[,] m_tileEntitiesWorldArray;
    private List<GameObject> m_gameObjectTiles = new List<GameObject>();


    // Start is called before the first frame update
    void Awake()
    {
        CenterCamera();
        m_tileEntitiesWorldArray = new TileEntity[m_columns, m_rows];
        CreateWorldTiles();
        for (int i = 0; i < m_tileEntitiesWorldArray.GetLength(0); i++)
        {
            for (int j = 0; j < m_tileEntitiesWorldArray.GetLength(1); j++)
            {
                AddNeighbours(m_tileEntitiesWorldArray, m_tileEntitiesWorldArray[i, j]);
            }
        }
    }

    private void CenterCamera()
    {
        if(m_cameraObject!= null)
        {
            m_cameraObject.transform.position = new Vector3((m_rows/2) * m_tilePrefab.transform.lossyScale.x + (m_rows/2 * m_offsetBetweenTiles), 20, (m_columns / 2) * m_tilePrefab.transform.lossyScale.x + (m_columns/2 * m_offsetBetweenTiles));      
        }
    }

    public TileEntity[,] GetWorldArray()
    {
        return m_tileEntitiesWorldArray;
    }

    private void CreateWorldTiles()
    {
        float tileWidth = m_tilePrefab.transform.lossyScale.x;

        for (int i = 0; i < m_columns; i++)
        {

            for (int j = 0; j < m_rows; j++)
            {
                GameObject tileGameObject = Instantiate(m_tilePrefab, new Vector3(i * tileWidth + (i * m_offsetBetweenTiles), 0, j * tileWidth + (j * m_offsetBetweenTiles)), Quaternion.identity, gameObject.transform);
                TileEntity tileEntity = tileGameObject.GetComponent<TileEntity>();
                tileEntity.m_xCoordinate = i;
                tileEntity.m_yCoordinate = j;
                tileEntity.SetTileStatus(TILESTATUS.OPEN);
                m_gameObjectTiles.Add(tileGameObject);

                if (i == 0 && j == 0)
                {
                    tileEntity.SetTileStatus(TILESTATUS.START);
                }
                if (i == m_columns - 1 && j == m_rows - 1)
                {
                    tileEntity.SetTileStatus(TILESTATUS.END);
                }

                m_tileEntitiesWorldArray[i, j] = tileEntity;
            }
        }
    }

    public void ClearAllTiles()
    {
        for (int i = 0; i < m_tileEntitiesWorldArray.GetLength(0); i++)
        {
            for (int j = 0; j < m_tileEntitiesWorldArray.GetLength(1); j++)
            {
                m_tileEntitiesWorldArray[i, j].m_visited = false;
                m_tileEntitiesWorldArray[i, j].m_parent = null;
                m_tileEntitiesWorldArray[i, j].m_distance = 99;
                if (m_tileEntitiesWorldArray[i, j].GetTileStatus() == TILESTATUS.WALKINGPATH)
                {
                    m_tileEntitiesWorldArray[i, j].SetTileStatus(TILESTATUS.OPEN);
                }
            }
        }
    }

    private void AddNeighbours(TileEntity[,] pTileList, TileEntity pTargetEntity)
    {
        if (pTargetEntity != null)
        {
            if (pTargetEntity.m_xCoordinate > 0)
            {
                pTargetEntity.m_neighbours.Add(pTileList[pTargetEntity.m_xCoordinate - 1, pTargetEntity.m_yCoordinate]);
            }
            if (pTargetEntity.m_xCoordinate < m_columns - 1)
            {
                pTargetEntity.m_neighbours.Add(pTileList[pTargetEntity.m_xCoordinate + 1, pTargetEntity.m_yCoordinate]);
            }
            if (pTargetEntity.m_yCoordinate > 0)
            {
                pTargetEntity.m_neighbours.Add(pTileList[pTargetEntity.m_xCoordinate, pTargetEntity.m_yCoordinate - 1]);
            }
            if (pTargetEntity.m_yCoordinate < m_rows - 1)
            {
                pTargetEntity.m_neighbours.Add(pTileList[pTargetEntity.m_xCoordinate, pTargetEntity.m_yCoordinate + 1]);
            }
        }
    }

    public TileEntity GetBeginNode()
    {
        TileEntity startNode = m_tileEntitiesWorldArray[0, 0];
        return startNode;
    }

    public TileEntity GetEndNode()
    {
        TileEntity endNode = m_tileEntitiesWorldArray[m_tileEntitiesWorldArray.GetLength(0) - 1, m_tileEntitiesWorldArray.GetLength(1) - 1];
        return endNode;
    }

    public List<GameObject> GetGameObjectsTiles()
    {
        return m_gameObjectTiles;
    }
}
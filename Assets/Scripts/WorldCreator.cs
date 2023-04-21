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
    private TileEntity m_selectedTileEntity;
    private ResourceManager m_resourceManager;
    [SerializeField]
    private GameObject m_singleTargetTower, m_debuffTower, m_multiShotTower;

    [SerializeField]
    public TileState tileStateOpen, tileStateOccupied, tileStatePath, tileStateSelected, tileStateStart, tileStateEnd;



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
        m_resourceManager = FindAnyObjectByType<ResourceManager>();
    }

    public TileEntity[,] GetWorldArray()
    {
        return m_tileEntitiesWorldArray;
    }

    public void ClearAllWalkingTiles()
    {
        for (int i = 0; i < m_tileEntitiesWorldArray.GetLength(0); i++)
        {
            for (int j = 0; j < m_tileEntitiesWorldArray.GetLength(1); j++)
            {
                m_tileEntitiesWorldArray[i, j].m_visited = false;
                m_tileEntitiesWorldArray[i, j].m_parent = null;
                m_tileEntitiesWorldArray[i, j].m_distance = 99;
                if (m_tileEntitiesWorldArray[i, j].m_tileState == tileStatePath)
                {
                    m_tileEntitiesWorldArray[i, j].SetTileStatus(tileStateOpen);
                }
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

    public void SelectTileEntity(TileEntity pTileEntity)
    {
        if (m_selectedTileEntity != null)
        {
            if (m_selectedTileEntity.GetTowerEntity() != null)
            {
                m_selectedTileEntity.SetTileStatus(tileStateOccupied);
            }
            else
            {
                m_selectedTileEntity.SetTileStatus(tileStateOpen);
            }
        }
        m_selectedTileEntity = pTileEntity;
        m_selectedTileEntity.SetTileStatus(tileStateSelected);

        //TODO: Change this to somewehre else later.
        if (m_selectedTileEntity.GetTowerEntity() != null)
        {

        }
    }

    public void ButtonTowerOneClicked()
    {
        if (m_selectedTileEntity.GetTowerEntity() == null)
        {
            BuildTower(m_singleTargetTower);
        }
        else if (m_selectedTileEntity.GetTowerEntity().GetType() == typeof(SingleTargetTower))
        {
            UpgradeTower(m_selectedTileEntity.GetTowerEntity());
        }
    }

    public void ButtonTowerTwoClicked()
    {
        if (m_selectedTileEntity.GetTowerEntity() == null)
        {
            BuildTower(m_debuffTower);
        }
        else if (m_selectedTileEntity.GetTowerEntity().GetType() == typeof(DebuffTower))
        {
            UpgradeTower(m_selectedTileEntity.GetTowerEntity());
        }
    }

    public void ButtonTowerThreeClicked()
    {

        if (m_selectedTileEntity.GetTowerEntity() == null)
        {
            BuildTower(m_multiShotTower);
        }
        else if(m_selectedTileEntity.GetTowerEntity().GetType() == typeof(MultiShotTower))
        {
            UpgradeTower(m_selectedTileEntity.GetTowerEntity());
        }
    }

    public void BuildTower(GameObject pTowerGameObject)
    {
        Tower tower = pTowerGameObject.GetComponent<Tower>();
        if (m_resourceManager.CanAfford(tower.Cost))
        {
            if (m_selectedTileEntity != null && m_selectedTileEntity.m_tileState.CanBuildOnTile)
            {
                m_selectedTileEntity.SetTileStatus(tileStateOccupied);
                if (m_pathFinder.CanGeneratePath())
                {
                    m_resourceManager.BuyUpgrade(tower.Cost);
                    m_selectedTileEntity.BuildTower(pTowerGameObject);
                }
                else
                {
                    m_selectedTileEntity.SetTileStatus(tileStateOpen);
                    //TODO: Can not create path message
                }

            }
        }
    }

    public void UpgradeTower(Tower pTower)
    {
        if (m_resourceManager.CanAfford(pTower.GetUpgradeCost()))
        {
            m_resourceManager.BuyUpgrade(pTower.GetUpgradeCost());
            pTower.Upgrade();
        }
    }

    private void CenterCamera()
    {
        if (m_cameraObject != null)
        {
            m_cameraObject.transform.position = new Vector3((m_rows / 2) * m_tilePrefab.transform.lossyScale.x + (m_rows / 2 * m_offsetBetweenTiles), 20, (m_columns / 2) * m_tilePrefab.transform.lossyScale.x + (m_columns / 2 * m_offsetBetweenTiles));
        }
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
                tileEntity.SetTileStatus(tileStateOpen);
                m_gameObjectTiles.Add(tileGameObject);

                if (i == 0 && j == 0)
                {
                    tileEntity.SetTileStatus(tileStateStart);
                }
                if (i == m_columns - 1 && j == m_rows - 1)
                {
                    tileEntity.SetTileStatus(tileStateEnd);
                }

                m_tileEntitiesWorldArray[i, j] = tileEntity;
            }
        }
    }

    private void AddNeighbours(TileEntity[,] pTileList, TileEntity pTargetEntity)
    {
        if (pTargetEntity != null)
        {
            if (pTargetEntity.m_xCoordinate > 0)
            {
                pTargetEntity.m_neighbourTiles.Add(pTileList[pTargetEntity.m_xCoordinate - 1, pTargetEntity.m_yCoordinate]);
            }
            if (pTargetEntity.m_xCoordinate < m_columns - 1)
            {
                pTargetEntity.m_neighbourTiles.Add(pTileList[pTargetEntity.m_xCoordinate + 1, pTargetEntity.m_yCoordinate]);
            }
            if (pTargetEntity.m_yCoordinate > 0)
            {
                pTargetEntity.m_neighbourTiles.Add(pTileList[pTargetEntity.m_xCoordinate, pTargetEntity.m_yCoordinate - 1]);
            }
            if (pTargetEntity.m_yCoordinate < m_rows - 1)
            {
                pTargetEntity.m_neighbourTiles.Add(pTileList[pTargetEntity.m_xCoordinate, pTargetEntity.m_yCoordinate + 1]);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    [SerializeField]
    public int rows, columns;
    [SerializeField]
    public GameObject tilePrefabGameObject, cameraObject;
    [SerializeField]
    public float offsetBetweenTiles;
    [SerializeField]
    private PathFinder m_pathFinder;

    private TileEntity[,] m_tileEntitiesWorldArray;
    private List<GameObject> m_gameObjectTiles = new List<GameObject>();
    private TileEntity m_selectedTileEntity;
    private ResourceController m_resourceManager;
    [SerializeField]
    private GameObject m_singleTargetTower, m_debuffTower, m_multiShotTower;

    [SerializeField]
    public TileState tileStateOpen, tileStateOccupied, tileStatePath, tileStateSelected, tileStateStart, tileStateEnd;



    // Start is called before the first frame update
    void Awake()
    {
        CenterCamera();
        m_tileEntitiesWorldArray = new TileEntity[columns, rows];
        CreateWorldTiles();
        for (int i = 0; i < m_tileEntitiesWorldArray.GetLength(0); i++)
        {
            for (int j = 0; j < m_tileEntitiesWorldArray.GetLength(1); j++)
            {
                AddNeighbours(m_tileEntitiesWorldArray, m_tileEntitiesWorldArray[i, j]);
            }
        }
        m_resourceManager = FindAnyObjectByType<ResourceController>();
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
                m_tileEntitiesWorldArray[i, j].visited = false;
                m_tileEntitiesWorldArray[i, j].parentTile = null;
                m_tileEntitiesWorldArray[i, j].distance = 99;
                if (m_tileEntitiesWorldArray[i, j].tileState == tileStatePath)
                {
                    m_tileEntitiesWorldArray[i, j].SetTileStatus(tileStateOpen);
                }
            }
        }
    }

    public TileEntity GetBeginNode()
    {
        if (m_tileEntitiesWorldArray != null && m_tileEntitiesWorldArray.Length > 0)
        {
            TileEntity startNode = m_tileEntitiesWorldArray[0, 0];
            return startNode;
        }
        return new TileEntity();
    }

    public TileEntity GetEndNode()
    {
        if (m_tileEntitiesWorldArray != null && m_tileEntitiesWorldArray.Length > 0)
        {
            TileEntity endNode = m_tileEntitiesWorldArray[m_tileEntitiesWorldArray.GetLength(0) - 1, m_tileEntitiesWorldArray.GetLength(1) - 1];
            return endNode;
        }
        return new TileEntity();
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
        else if (m_selectedTileEntity.GetTowerEntity().GetType() == typeof(MultiShotTower))
        {
            UpgradeTower(m_selectedTileEntity.GetTowerEntity());
        }
    }

    public void BuildTower(GameObject pTowerGameObject)
    {
        Tower tower = pTowerGameObject.GetComponent<Tower>();
        if (m_resourceManager.CanAfford(tower.cost))
        {
            if (m_selectedTileEntity != null && m_selectedTileEntity.tileState.canBuildOnTile)
            {
                m_selectedTileEntity.SetTileStatus(tileStateOccupied);
                if (m_pathFinder.CanGeneratePath())
                {
                    m_resourceManager.BuyUpgrade(tower.cost);
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
        if (cameraObject != null)
        {
            cameraObject.transform.position = new Vector3((rows / 2) * tilePrefabGameObject.transform.lossyScale.x + (rows / 2 * offsetBetweenTiles), 20, (columns / 2) * tilePrefabGameObject.transform.lossyScale.x + (columns / 2 * offsetBetweenTiles));
        }
    }


    private void CreateWorldTiles()
    {
        float tileWidth = tilePrefabGameObject.transform.lossyScale.x;

        for (int i = 0; i < columns; i++)
        {

            for (int j = 0; j < rows; j++)
            {
                GameObject tileGameObject = Instantiate(tilePrefabGameObject, new Vector3(i * tileWidth + (i * offsetBetweenTiles), 0, j * tileWidth + (j * offsetBetweenTiles)), Quaternion.identity, gameObject.transform);
                TileEntity tileEntity = tileGameObject.GetComponent<TileEntity>();
                tileEntity.xCoordinate = i;
                tileEntity.yCoordinate = j;
                tileEntity.SetTileStatus(tileStateOpen);
                m_gameObjectTiles.Add(tileGameObject);

                if (i == 0 && j == 0)
                {
                    tileEntity.SetTileStatus(tileStateStart);
                }
                if (i == columns - 1 && j == rows - 1)
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
            if (pTargetEntity.xCoordinate > 0)
            {
                pTargetEntity.neighbourTiles.Add(pTileList[pTargetEntity.xCoordinate - 1, pTargetEntity.yCoordinate]);
            }
            if (pTargetEntity.xCoordinate < columns - 1)
            {
                pTargetEntity.neighbourTiles.Add(pTileList[pTargetEntity.xCoordinate + 1, pTargetEntity.yCoordinate]);
            }
            if (pTargetEntity.yCoordinate > 0)
            {
                pTargetEntity.neighbourTiles.Add(pTileList[pTargetEntity.xCoordinate, pTargetEntity.yCoordinate - 1]);
            }
            if (pTargetEntity.yCoordinate < rows - 1)
            {
                pTargetEntity.neighbourTiles.Add(pTileList[pTargetEntity.xCoordinate, pTargetEntity.yCoordinate + 1]);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TileEntity : MonoBehaviour
{
    [SerializeField]
    private Material m_tileMaterial;
    [SerializeField]
    public GameObject m_gameObject;
    [SerializeField]
    public TileState tileStateOpen, tileStateOccupied, tileStatePath, tileStateSelected, tileStateStart, tileStateEnd;

    [SerializeField]
    private GameObject m_singleTargetTower, m_debuffTower, m_multiShotTower;
    [SerializeField]
    private SelectedTowerScriptableObject m_selectedTowerScriptableObject;

    public TileState tileState;
    public bool visited = false;
    public TileEntity parentTile;
    public Renderer tileRenderer;
    public List<TileEntity> neighbourTiles = new List<TileEntity>();
    public float distance = 99;
    public int xCoordinate, yCoordinate;

    private WorldCreator m_worldCreator;
    private Tower m_currentBuiltTower;

    public void Awake()
    {
        tileRenderer = gameObject.GetComponent<Renderer>();
        m_worldCreator = FindAnyObjectByType<WorldCreator>();

    }

    private void OnMouseOver()
    {
        //TODO: Update this when leveling up.
        if (Input.GetMouseButtonDown(0))
        {
            m_worldCreator.SelectTileEntity(this);
            if (m_currentBuiltTower != null)
            {
                SelectTower(m_currentBuiltTower);
            }
            else
            {
                m_selectedTowerScriptableObject.selectedTower = null;
            }
        }
    }

    public void SelectTower(Tower pTower)
    {
        m_worldCreator.SelectTileEntity(this);
        m_selectedTowerScriptableObject.selectedTower = pTower;
    }

    public void BuildTower(GameObject pTower)
    {
        GameObject instantiatedTower = Instantiate(pTower, new Vector3(m_gameObject.transform.position.x, 0, m_gameObject.transform.position.z), Quaternion.identity);
        instantiatedTower.transform.SetParent(gameObject.transform);
        m_currentBuiltTower = instantiatedTower.GetComponent<Tower>();
        m_currentBuiltTower.SetTileEntity(this);
        SetTileStatus(tileStateOccupied);
    }

    public Tower GetTowerEntity()
    {
        return m_currentBuiltTower;
    }

    public void SetTileStatus(TileState pTileState)
    {
        tileState = pTileState;
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (tileRenderer != null && tileState != null)
        {
            tileRenderer.material.SetColor("_Color", tileState.tileColor);
        }
        else
        {
            throw new System.Exception("Renderer or tilestatus invalid");
        }
    }


    public List<TileEntity> GetUnvisitedNeighbours()
    {
        List<TileEntity> neighbours = new List<TileEntity>();
        if (neighbourTiles != null)
        {
            for (int i = 0; i < neighbourTiles.Count; i++)
            {
                if (!neighbourTiles[i].visited && (!neighbourTiles[i].tileState.cantMoveThroughTile))
                {
                    neighbours.Add(neighbourTiles[i]);
                }
            }
        }
        return neighbours;
    }

    public float CalculateDistance(TileEntity pGoalNode)
    {
        int xDiff = Mathf.Abs(pGoalNode.xCoordinate - xCoordinate);
        int yDiff = Mathf.Abs(pGoalNode.yCoordinate - yCoordinate);
        float distance = Mathf.Sqrt(Mathf.Pow(xDiff, 2) + Mathf.Pow(yDiff, 2));
        return distance;
    }
}

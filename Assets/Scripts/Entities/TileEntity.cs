using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Renderer))]
public class TileEntity : MonoBehaviour
{
    [SerializeField]
    private Material m_tileMaterial;
    [SerializeField]
    public GameObject m_gameObject;
    [SerializeField]
    private GameObject m_singleTargetTower, m_debuffTower, m_multiShotTower;

    public int m_xCoordinate, m_yCoordinate;
    public float m_distance = 99;

    public TileState m_tileState;

    public bool m_visited = false;
    public TileEntity m_parent;
    public Renderer m_renderer;
    public List<TileEntity> m_neighbourTiles = new List<TileEntity>();
    private WorldCreator m_worldCreator;
    private Tower m_currentBuiltTower;
    [SerializeField]
    private SelectedTowerScriptableObject m_selectedTowerScriptableObject;

    [SerializeField]
    public TileState tileStateOpen, tileStateOccupied, tileStatePath, tileStateSelected, tileStateStart, tileStateEnd;


    public void Awake()
    {
        m_renderer = gameObject.GetComponent<Renderer>();
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
                m_selectedTowerScriptableObject.SelectedTower = null;
            }
        }
    }

    public void SelectTower(Tower pTower)
    {
        m_worldCreator.SelectTileEntity(this);
        m_selectedTowerScriptableObject.SelectedTower = pTower;
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
        m_tileState = pTileState;
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (m_renderer != null && m_tileState != null)
        {
            m_renderer.material.SetColor("_Color", m_tileState.TileColor);
        }
        else
        {
            throw new System.Exception("Renderer or tilestatus invalid");
        }
    }


    public List<TileEntity> GetUnvisitedNeighbours()
    {
        List<TileEntity> neighbours = new List<TileEntity>();
        if (m_neighbourTiles != null)
        {
            for (int i = 0; i < m_neighbourTiles.Count; i++)
            {
                if (!m_neighbourTiles[i].m_visited && (!m_neighbourTiles[i].m_tileState.CantMoveThroughTile))
                {
                    neighbours.Add(m_neighbourTiles[i]);
                }
            }
        }
        return neighbours;
    }

    public float CalculateDistance(TileEntity pGoalNode)
    {
        int xDiff = Mathf.Abs(pGoalNode.m_xCoordinate - m_xCoordinate);
        int yDiff = Mathf.Abs(pGoalNode.m_yCoordinate - m_yCoordinate);
        float distance = Mathf.Sqrt(Mathf.Pow(xDiff, 2) + Mathf.Pow(yDiff, 2));
        return distance;
    }
}

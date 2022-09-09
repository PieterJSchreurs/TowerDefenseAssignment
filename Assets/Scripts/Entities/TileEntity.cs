using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEntity : MonoBehaviour
{
    [SerializeField]
    private Material m_tileMaterial;
    [SerializeField]
    public GameObject m_gameObject;

    private TILESTATUS m_tileStatus = TILESTATUS.OPEN;
    private Renderer m_renderer;

    public int m_xCoordinate;
    public int m_yCoordinate;
    public bool m_visited = false;
    public TileEntity m_parent;
    public List<TileEntity> m_neighbours = new List<TileEntity>();

    public void Awake()
    {
        m_gameObject = gameObject;
        m_renderer = m_gameObject.GetComponent<Renderer>();
    }

    public void SetTileStatus(TILESTATUS tileStatus)
    {
        m_tileStatus = tileStatus;
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (m_gameObject == null)
        {
            m_gameObject = GetComponent<GameObject>();
        }
        if (m_renderer == null)
        {
            m_renderer = m_gameObject.GetComponent<Renderer>();
        }
        switch (m_tileStatus)
        {
            case TILESTATUS.OPEN:
                m_renderer.material.SetColor("_Color", Color.white);
                break;
            case TILESTATUS.OCCUPIED:
                m_renderer.material.SetColor("_Color", Color.blue);
                break;
            case TILESTATUS.START:
                m_renderer.material.SetColor("_Color", Color.green);
                break;
            case TILESTATUS.END:
                m_renderer.material.SetColor("_Color", Color.red);
                break;
        }
    }

    public TILESTATUS GetTileStatus()
    {
        return m_tileStatus;
    }

    public List<TileEntity> GetUnvisitedNeighbours()
    {
        List<TileEntity> neighbours = new List<TileEntity>();
        if (m_neighbours != null)
        {
            for (int i = 0; i < m_neighbours.Count; i++)
            {
                if (!m_neighbours[i].m_visited && m_neighbours[i].GetTileStatus() != TILESTATUS.OCCUPIED)
                {
                    neighbours.Add(m_neighbours[i]);
                }
            }
        }
        return neighbours;
    }
}

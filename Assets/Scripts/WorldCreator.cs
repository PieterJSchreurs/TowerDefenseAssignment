using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCreator : MonoBehaviour
{
    [SerializeField]
    public int m_rows, m_columns;
    [SerializeField]
    public GameObject m_tilePrefab;
    [SerializeField]
    public float m_offsetBetweenTiles;

    private TILESTATUS[,] m_worldArray;
    private GameObject[] tileObjects;

    // Start is called before the first frame update
    void Start()
    {
        CreateWorldArray();
        CreateWorldTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateWorldArray()
    {
        m_worldArray = new TILESTATUS[m_columns, m_rows];
        for (int i = 0; i < m_columns; ++i)
        {
            for (int j = 0; j < m_rows; ++j)
            {
                m_worldArray[i, j] = TILESTATUS.OPEN;
            }
        }
    }

    public TILESTATUS[,] GetWorldArray()
    {
        return m_worldArray;
    }

    private void CreateWorldTiles()
    {
        float tileWidth = m_tilePrefab.transform.lossyScale.x;
        float halfWidthColumns = m_columns / 2;
        float halfWidthRows = m_rows / 2;
        for (float i = 0 - halfWidthColumns; i < halfWidthColumns; ++i)
        {
            for (float j = 0 - halfWidthRows; j < halfWidthRows; ++j)
            {
                Instantiate(m_tilePrefab, new Vector3(i * tileWidth + (i * m_offsetBetweenTiles), 0, j * tileWidth + (j * m_offsetBetweenTiles)), Quaternion.identity);
            }
        }
    }


}

public enum TILESTATUS { OPEN = 0, OCCUPIED = 1 }

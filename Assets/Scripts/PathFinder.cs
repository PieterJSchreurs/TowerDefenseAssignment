using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    private WorldCreator m_worldCreator;

    private TileEntity[,] m_tileWorld;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<TileEntity> GeneratePath()
    {
        TileEntity startNode = m_worldCreator.GetBeginNode();
        TileEntity endNode = m_worldCreator.GetEndNode();
        if (startNode == endNode)
        {
            Debug.Log("oopsie");
        }
        bool pathFound = FindPath(m_worldCreator.GetWorldArray(), startNode, endNode);
        List<TileEntity> coordinateList = new List<TileEntity>();

        if (pathFound)
        {
            Debug.Log("Found path");
            TileEntity selectedCoordinate = endNode;
            while (selectedCoordinate != startNode)
            {
                coordinateList.Add(selectedCoordinate);
                selectedCoordinate = selectedCoordinate.m_parent;
                if (selectedCoordinate != startNode)
                {
                    selectedCoordinate.SetTileStatus(TILESTATUS.OCCUPIED);
                }
            }
            coordinateList.Reverse();
        }
        else
        {
            Debug.Log("No path");
        }

        return coordinateList;
    }

    private bool FindPath(TileEntity[,] pTileEntities, TileEntity pStartNode, TileEntity pEndNode)
    {
        if (pStartNode == pEndNode) return true;

        pStartNode.m_visited = true;
        List<TileEntity> unvisitedNeighbours = pStartNode.GetUnvisitedNeighbours();

        for (int i = 0; i < unvisitedNeighbours.Count; i++)
        {
            unvisitedNeighbours[i].m_parent = pStartNode;
            if (FindPath(pTileEntities, unvisitedNeighbours[i], pEndNode)) return true;
        }
        return false;
    }
}

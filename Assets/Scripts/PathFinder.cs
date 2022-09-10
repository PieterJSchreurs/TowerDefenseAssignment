using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder : MonoBehaviour
{
    [SerializeField]
    private WorldCreator m_worldCreator;

    private PriorityQueue m_priorityQueue;
    private class PriorityQueue
    {
        List<TileEntity> m_priorityQueue = new List<TileEntity>();
        public void AddToQueue(TileEntity pTileEntity, int pPriority)
        {
            //m_priorityQueue.Add(pPriority, pTileEntity);
            if (pPriority > m_priorityQueue.Count)
            {
                m_priorityQueue.Add(pTileEntity);
            }
            else
            {
                m_priorityQueue.Insert(pPriority, pTileEntity);
            }
        }

        public void RemoveFromQueue(TileEntity pTileEntity)
        {
            m_priorityQueue.Remove(pTileEntity);
        }

        public void ChangePriority(TileEntity pTileEntity, int pPriority)
        {
            RemoveFromQueue(pTileEntity);
            AddToQueue(pTileEntity, pPriority);
        }

        public bool isEmpty()
        {
            if (m_priorityQueue.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsItem(TileEntity pTileEntity)
        {
            return m_priorityQueue.Contains(pTileEntity);
        }

        public TileEntity GetFirstInQueue()
        {
            return m_priorityQueue.FirstOrDefault();
        }

    }

    public List<TileEntity> GeneratePath()
    {
        TileEntity startNode = m_worldCreator.GetBeginNode();
        TileEntity endNode = m_worldCreator.GetEndNode();

        bool pathFound = FindPath(m_worldCreator.GetWorldArray(), startNode, endNode);
        List<TileEntity> coordinateList = new List<TileEntity>();

        if (pathFound && endNode.m_parent != null && endNode.m_visited)
        {
            TileEntity selectedCoordinate = endNode;
            while (selectedCoordinate != startNode)
            {
                coordinateList.Add(selectedCoordinate);
                selectedCoordinate = selectedCoordinate.m_parent;
                if (selectedCoordinate != startNode && selectedCoordinate != endNode)
                {
                    selectedCoordinate.SetTileStatus(TILESTATUS.WALKINGPATH);
                }
            }
            coordinateList.Add(selectedCoordinate);
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
        m_worldCreator.ClearAllTiles();

        pStartNode.m_visited = true;
        pStartNode.m_distance = 0;
        m_priorityQueue = new PriorityQueue();
        m_priorityQueue.AddToQueue(pStartNode, 0);
        TileEntity curNode;
        while (!m_priorityQueue.isEmpty())
        {
            curNode = m_priorityQueue.GetFirstInQueue();
            m_priorityQueue.RemoveFromQueue(curNode);
            curNode.m_visited = true;

            List<TileEntity> unvisitedNeighbours = curNode.GetUnvisitedNeighbours();
            foreach (TileEntity item in unvisitedNeighbours)
            {
                float minDistance = Mathf.Min(item.m_distance, curNode.m_distance + 1);
                if (minDistance != item.m_distance)
                {
                    item.m_distance = minDistance;
                    item.m_parent = curNode;

                    if (m_priorityQueue.ContainsItem(item))
                    {
                        m_priorityQueue.ChangePriority(item, (int)minDistance);
                    }
                    else
                    {
                        m_priorityQueue.AddToQueue(item, (int)minDistance);
                    }
                }
                if (curNode == pEndNode)
                {
                    return true;
                }
            }
        }
        return true;
    }
}



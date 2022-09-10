using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour, IMovable
{
    private float m_secondPerTile;
    private List<TileEntity> m_path;
    private int m_currentIndex = 0;
    private IEnumerator m_coroutine;
    private bool m_coroutineRunning = false;
    private bool m_allowedToMove = false;
    private GameObject m_myGameObject;

    public float secondPerTile
    {
        get { return m_secondPerTile; }
        set { m_secondPerTile = value; }
    }

    public List<TileEntity> path
    {
        get { return m_path; }
        set { m_path = value; }
    }

    public void Update()
    {
        if (m_currentIndex < m_path.Count - 1)
        {
            if (!m_coroutineRunning)
            {
                m_coroutine = MoveToNextTile(m_myGameObject, new Vector3(m_path[m_currentIndex + 1].m_gameObject.transform.position.x, 0, m_path[m_currentIndex + 1].m_gameObject.transform.position.z), 0.5f);
                StartCoroutine(m_coroutine);
            }
        } else
        {
            if(!m_coroutineRunning)
            {
                Destroy(m_myGameObject);
            }
        }
    }

    public void MoveNext(int pCurrentIndex, float pTimeItTakes)
    {

    }

    public IEnumerator MoveToNextTile(GameObject pObjectToMove, Vector3 pEnd, float pSeconds)
    {
        m_coroutineRunning = true;
        float elapsedTime = 0;
        Vector3 startingPos = pObjectToMove.transform.position;
        while (elapsedTime < pSeconds)
        {
            pObjectToMove.transform.position = Vector3.Lerp(startingPos, pEnd, (elapsedTime / pSeconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_currentIndex++;
        pObjectToMove.transform.position = pEnd;
        m_coroutineRunning = false;
    }

    public void SetAllowedToMove(bool pAllowedToMove)
    {
        m_allowedToMove = pAllowedToMove;
    }

    public void SetGameObject(GameObject pMyGameObject)
    {
        m_myGameObject = pMyGameObject;
    }
}
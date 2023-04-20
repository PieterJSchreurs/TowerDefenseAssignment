using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    public Healthbar healthBar;
    public abstract float MovementSpeed { get; set; }
    public abstract float Health { get; set; }
    public abstract int KillReward { get; set; }

    private float m_secondPerTile;
    private List<TileEntity> m_path;
    private int m_currentIndex = 0;
    private IEnumerator m_coroutine;
    private bool m_coroutineRunning = false;
    private GameObject m_myGameObject;
    private ResourceManager m_resourceManager;
    private EnemySpawner m_enemySpawner;
    private STATUS m_status;

    void Awake()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(Health);
        }
        m_enemySpawner = FindFirstObjectByType<EnemySpawner>();
        m_resourceManager = FindFirstObjectByType<ResourceManager>();
    }

    public void Die()
    {
        m_resourceManager.AddResources(KillReward);
        m_enemySpawner.NotifyDeath(this);
        Destroy(m_myGameObject);
    }

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

    public void TakeDamage(float pDamage)
    {
        Health -= pDamage;
        if (healthBar != null)
        {
            healthBar.SetHealth(Health);
        }
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Update()
    {
        if (m_currentIndex < m_path.Count - 1)
        {
            if (!m_coroutineRunning)
            {
                float effectiveMovementSpeed = MovementSpeed;
                if(m_status == STATUS.SLOWED)
                {
                    effectiveMovementSpeed = effectiveMovementSpeed / 2;
                }
                m_coroutine = MoveToNextTile(m_myGameObject, new Vector3(m_path[m_currentIndex + 1].m_gameObject.transform.position.x, 0, m_path[m_currentIndex + 1].m_gameObject.transform.position.z), (1.0f / effectiveMovementSpeed));
                StartCoroutine(m_coroutine);
            }
        }
        else
        {
            //Reached end
            if (!m_coroutineRunning)
            {
                Destroy(m_myGameObject);
                //TODO: notify
                GameLogic logic = FindObjectOfType<GameLogic>();
                logic.LostLive();
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

    public void SetGameObject(GameObject pMyGameObject)
    {
        m_myGameObject = pMyGameObject;
    }
    public void AffectedByStatus(STATUS pStatus)
    {
        m_status = pStatus;
    }

    public enum STATUS
    {
        NORMAL = 0,
        SLOWED = 1
    }
}

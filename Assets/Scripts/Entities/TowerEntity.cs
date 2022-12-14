using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TowerEntity : MonoBehaviour
{
    [SerializeField]
    private float m_range, m_shootingSpeed;

    [SerializeField]
    private SphereCollider m_sphereColider;

    [SerializeField]
    private int m_cost, m_damage;

    [SerializeField]
    private GameObject m_debugSphere;

    [SerializeField]
    private Renderer m_capsuleRenderer;

    [SerializeField]
    private Material m_standardMaterial, m_cooldownMaterial;

    private bool m_canFire = true;
    private float m_attackTimer = 0.0f;
    private EnemyEntity m_enemyTarget;
    private List<GameObject> m_targetList = new List<GameObject>();

    private float lerp = 0.0f;

    private void Awake()
    {
        if (m_sphereColider != null)
        {
            m_sphereColider.radius = m_range / 2;
            m_debugSphere.transform.localScale = new Vector3(m_range, m_range, m_range);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            m_targetList.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && m_targetList.Contains(other.gameObject))
        {
            m_targetList.Remove(other.gameObject);
        }
        if (other.gameObject.GetComponent<EnemyEntity>() == m_enemyTarget)
        {
            m_enemyTarget = null;
        }
    }

    private void Update()
    {
        if(m_canFire)
        {
            m_capsuleRenderer.material.Lerp(m_standardMaterial, m_cooldownMaterial, 0);
        } else
        {
            m_capsuleRenderer.material.Lerp(m_standardMaterial, m_cooldownMaterial, m_attackTimer);
        }
        
        if (m_targetList.Count > 0 && m_canFire)
        {
            Attack();
        }
        if (!m_canFire)
        {
            m_attackTimer += Time.deltaTime;
            if (m_attackTimer >= m_shootingSpeed)
            {
                m_canFire = true;
                lerp = 0.0f;
            }
        }
        if (m_targetList.FirstOrDefault() == null && m_targetList.Count > 0)
        {
            m_targetList.RemoveAt(0);
        }
        if (m_enemyTarget == null && m_targetList.Count > 0)
        {
            m_enemyTarget = m_targetList[0].GetComponent<EnemyEntity>();
        }
    }

    private void Attack()
    {
        if (m_enemyTarget != null)
        {
            m_attackTimer = 0.0f;
            m_enemyTarget.TakeDamage(m_damage);
            m_canFire = false;
        }
    }
}

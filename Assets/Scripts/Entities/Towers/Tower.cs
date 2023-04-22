using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class Tower : MonoBehaviour
{
    public abstract float range { get; set; }
    public abstract float shootingSpeed { get; set; }

    public abstract int cost { get; set; }
    public abstract float damage { get; set; }

    public abstract int level { get; set; }

    //public abstract TextMeshPro TextCost { get; set; }

    [SerializeField]
    private SphereCollider m_sphereColider;

    [SerializeField]
    private GameObject m_debugSphere;

    [SerializeField]
    private Renderer m_capsuleRenderer;

    [SerializeField]
    private Material m_standardMaterial, m_cooldownMaterial;

    [SerializeField]
    private SelectedTowerScriptableObject m_selectedTowerScriptableObject;

    public TowerUpgrade towerUpgrade;

    private bool m_canFire = true;
    private float m_attackTimer = 0.0f;
    private protected Enemy m_enemyTarget;
    private protected List<Enemy> m_targetList = new List<Enemy>();
    private TileEntity m_myTileEntity;
    

    private void Awake()
    {
        if (m_sphereColider != null)
        {
            m_sphereColider.radius = range / 2;
            m_debugSphere.transform.localScale = new Vector3(range, range, range);
        }
    }

    private void ResizeRange()
    {
        if (m_sphereColider != null)
        {
            float calculation = range + (level * towerUpgrade.rangeIncrease);
            m_sphereColider.radius = calculation / 2;
            m_debugSphere.transform.localScale = new Vector3(calculation, calculation, calculation);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_myTileEntity.SelectTower(this);
        }
    }

    public virtual void OnTriggerEnter(Collider pOther)
    {
        if (pOther.tag == "Enemy")
        {
            m_targetList.Add(pOther.gameObject.GetComponent<Enemy>());
        }
    }

    public virtual void OnTriggerExit(Collider pOther)
    {
        if (pOther.tag == "Enemy" && m_targetList.Contains(pOther.gameObject.GetComponent<Enemy>()))
        {
            m_targetList.Remove(pOther.gameObject.GetComponent<Enemy>());
        }
        if (pOther.gameObject.GetComponent<Enemy>() == m_enemyTarget)
        {
            m_enemyTarget = null;
        }
    }

    private void Update()
    {
        if (m_canFire)
        {
            m_capsuleRenderer.material.Lerp(m_standardMaterial, m_cooldownMaterial, 0);
        }
        else
        {
            m_capsuleRenderer.material.Lerp(m_standardMaterial, m_cooldownMaterial, m_attackTimer);
        }
        if (m_targetList.FirstOrDefault() == null && m_targetList.Count > 0)
        {
            m_targetList.RemoveAt(0);
        }
        if (m_enemyTarget == null && m_targetList.Count > 0)
        {
            if (m_targetList[0] != null)
            {
                m_enemyTarget = m_targetList[0].GetComponent<Enemy>();
            }
        }
        if (m_targetList.Count > 0)
        {
            var lookPos = m_targetList.FirstOrDefault().transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
            if (m_canFire)
            {
                Attack();
            }
        }
        if (!m_canFire)
        {
            m_attackTimer += Time.deltaTime;
            if (m_attackTimer >= shootingSpeed * (Math.Pow(towerUpgrade.shootingSpeedIncrease, level)))
            {
                m_canFire = true;
            }
        }
    }

    public virtual void Attack()
    {
        if (m_enemyTarget != null)
        {
            m_enemyTarget.TakeDamage(damage * (level + towerUpgrade.damageIncrease));
            LineRendererController lineRendererController = new LineRendererController();
            lineRendererController.SetupLine(this.transform, m_enemyTarget.transform);
        }
        m_attackTimer = 0.0f;
        m_canFire = false;
    }

    public void Upgrade()
    {
        level = level + 1;
        ResizeRange();
    }

    public virtual float GetUpgradeCost()
    {
        return (towerUpgrade.upgradeCost * level) + cost;
    }

    public void SetTileEntity(TileEntity pTileEntity)
    {
        m_myTileEntity = pTileEntity;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class EnemyDiedEvent : UnityEvent<Enemy> { }
public class GameEventEnemyDiedListener : MonoBehaviour
{
    [SerializeField]
    private GameEventEnemyDied m_gameEventEnemyDied;

    public EnemyDiedEvent Response;
    private void OnEnable()
    {
        m_gameEventEnemyDied.RegisterListener(this); 
    }

    private void OnDisable()
    {
        m_gameEventEnemyDied.UnregisterListener(this);
    }

    public void OnEventRaised(Enemy pEnemy)
    { Response.Invoke(pEnemy); }
}

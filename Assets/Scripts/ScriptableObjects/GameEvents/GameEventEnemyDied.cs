using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventEnemyDied", menuName = "Events/GameEventEnemyDied")]
public class GameEventEnemyDied : ScriptableObject
{
    private List<GameEventEnemyDiedListener> listeners =
       new List<GameEventEnemyDiedListener>();

    public void Raise(Enemy pEnemy)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(pEnemy);
    }

    public void RegisterListener(GameEventEnemyDiedListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(GameEventEnemyDiedListener listener)
    { listeners.Remove(listener); }
}

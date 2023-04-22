using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedTower", menuName ="SelectedTower")]
public class SelectedTowerScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    public Tower selectedTower;

    public void OnAfterDeserialize()
    {
        selectedTower = null;
    }

    public void OnBeforeSerialize()
    {

    }
}

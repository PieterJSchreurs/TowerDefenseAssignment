using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedTower", menuName ="SelectedTower")]
public class SelectedTowerScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    public Tower SelectedTower;

    public void OnAfterDeserialize()
    {
        SelectedTower = null;
    }

    public void OnBeforeSerialize()
    {

    }
}

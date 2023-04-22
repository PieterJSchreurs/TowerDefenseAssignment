using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceValue", menuName = "ResourceValue")]
public class ResourceValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    [System.NonSerialized]
    public float runTimeValue;

    public void OnAfterDeserialize()
    {
        runTimeValue = initialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceValue", menuName = "ResourceValue")]
public class ResourceValue : ScriptableObject, ISerializationCallbackReceiver
{
    public int InitialValue;
    [System.NonSerialized]
    public int RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}



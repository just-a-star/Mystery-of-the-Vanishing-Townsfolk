using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool initialValue;

    public bool defaultValue;

    public void OnAfterDeserialize()
    {
        defaultValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}

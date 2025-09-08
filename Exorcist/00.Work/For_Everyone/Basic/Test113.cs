using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Test113 : SerializedMonoBehaviour
{
    [OdinSerialize] private Dictionary<string, int> dic;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/BossRoomDataSO")]
public class BossRoomSO : ScriptableObject
{
    [FormerlySerializedAs("bossRoomIndex")] public int sceneIndex;
    public bool isOpen;
}

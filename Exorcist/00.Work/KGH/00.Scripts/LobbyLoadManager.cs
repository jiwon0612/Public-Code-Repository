using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LobbyLoadManager : MonoBehaviour
{
    [SerializeField] GameSaveManagerSO _gameSaveManagerSO;
    [SerializeField] private List<BossRoomSO> _bossRoomDatas;

    private void OnEnable()
    {
        var saveData = _gameSaveManagerSO.LoadGameData();

        _bossRoomDatas[0].isOpen = true;
        for (var i = 0; i < 2; i++)
        {
            if (saveData.clearedBosses[i])
            {
                _bossRoomDatas[i + 1].isOpen = true;
            }
            else
            {
                _bossRoomDatas[i + 1].isOpen = false;
            }
        }
    }
}

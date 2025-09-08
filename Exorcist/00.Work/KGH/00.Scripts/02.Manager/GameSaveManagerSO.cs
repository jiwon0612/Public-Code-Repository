using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Json/SaveManager")]
public class GameSaveManagerSO : JsonManagerSO
{
    public SlotData[] slotDatas = new SlotData[3];
    private int lastPlayedSlot;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        var defaultSlot = new SlotData()
        {
            isSlotUsed = false,
            lastPlayDate = "0000-00-00",
            openDateTime = DateTime.MinValue.ToString("G"),
            lastSaveDate = DateTime.MinValue.ToString("t"),
            playTime = "00:00:00",
            clearedBosses = new bool[3]
        };

        var slots = LoadJson<Slots>(PrefsKeyType.SaveFile,
            new Slots { slotData0 = defaultSlot, slotData1 = defaultSlot, slotData2 = defaultSlot });

        slotDatas[0] = slots.slotData0;
        slotDatas[1] = slots.slotData1;
        slotDatas[2] = slots.slotData2;

        for (int i = 0; i < slotDatas.Length; i++)
        {
            if (string.IsNullOrEmpty(slotDatas[i].lastPlayDate))
            {
                Debug.Log(i);
                slotDatas[i] = defaultSlot;
            }
        }
        
        slots.slotData0 = slotDatas[0];
        slots.slotData1 = slotDatas[1];
        slots.slotData2 = slotDatas[2];

        SaveJson(slots, PrefsKeyType.SaveFile);
    }

    public void SaveGameData()
    {
        var openDateTime = DateTime.Parse(slotDatas[lastPlayedSlot].openDateTime);
        var playTimeSpan = DateTime.Now - openDateTime;
        playTimeSpan += TimeSpan.Parse(slotDatas[lastPlayedSlot].playTime);

        slotDatas[lastPlayedSlot].lastPlayDate = DateTime.Now.ToString("yyyy-MM-dd");
        slotDatas[lastPlayedSlot].playTime = playTimeSpan.ToString(@"hh\:mm\:ss");
        slotDatas[lastPlayedSlot].lastSaveDate = DateTime.Now.ToString("t");

        SaveJson(
            new Slots { slotData0 = slotDatas[0], slotData1 = slotDatas[1], slotData2 = slotDatas[2] },
            PrefsKeyType.SaveFile);
        Debug.Log("saved");
    }

    public void SaveGameData(int clearedBoss)
    {
        slotDatas[lastPlayedSlot].clearedBosses[clearedBoss] = true;
        SaveGameData();
    }

    public SlotData LoadGameData() => slotDatas[lastPlayedSlot];

    public SlotData LoadGameData(int i)
    {
        if (i > 2)
        {
            Debug.LogError("Out of index!!!1");
            return default;
        }

        var returnData = slotDatas[i];
        return returnData;
    }

    public void OpenGame(int i)
    {
        slotDatas[i].isSlotUsed = true;

        slotDatas[i].openDateTime = DateTime.Now.ToString("G");
        slotDatas[i].lastSaveDate = DateTime.Now.ToString("t");
        lastPlayedSlot = i;
        SaveGameData();
    }
}

[Serializable]
public struct SlotData
{
    public bool isSlotUsed;
    public string lastPlayDate;
    public string openDateTime;
    public string lastSaveDate;
    public string playTime;
    public bool[] clearedBosses;
}

[Serializable]
public struct Slots
{
    public SlotData slotData0;
    public SlotData slotData1;
    public SlotData slotData2;
}
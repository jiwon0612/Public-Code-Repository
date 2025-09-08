using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameSaveManagerSO _gameSaveManagerSO;
    [SerializeField] TransitionUI _transitionUI;
    
    [SerializeField] private BossRoomSO _bossRoom1Data;
    [SerializeField] private BossRoomSO _bossRoom2Data;
    [SerializeField] private BossRoomSO _bossRoom3Data;


    public void BossRoom1Clear()
    {
        _bossRoom2Data.isOpen = true;
        _gameSaveManagerSO.SaveGameData(0);
        _transitionUI.TransitionStart(()=>SceneManager.LoadScene(1));
    } 
    public void BossRoom2Clear()
    {
        _bossRoom3Data.isOpen = true;
        _gameSaveManagerSO.SaveGameData(1);
        _transitionUI.TransitionStart(()=>SceneManager.LoadScene(1));
    } 
    public void BossRoom3Clear()
    {
        _gameSaveManagerSO.SaveGameData(2);
        _transitionUI.TransitionStart(()=>SceneManager.LoadScene(1));
    }
}

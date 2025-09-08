using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class LoadFileUI : UIToolkitParents
{
    // [SerializeField] private UISoundSO uiSoundSO;
    [SerializeField] private Sprite openedBossDoorIcon;
    [SerializeField] private Sprite closedBossDoorIcon;
    [SerializeField] private GameSaveManagerSO gameSaveManager;
    [SerializeField] private UIInputReader inputReader;
    [SerializeField] private TitleUI _titleUI;
    [SerializeField] private TransitionUI _transitionUI;
    
    private VisualElement _container;
    private List<Button> _slots;
    private Button _backBtn;

    public bool IsOpen { get; private set; } = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        _slots = Root.Query<Button>("Slot").ToList();
        _container = Root.Q<VisualElement>("Container");
        _backBtn = Root.Q<Button>("BackButton");
        _backBtn.RegisterCallback<ClickEvent>(click => DisableUI());

        for (int i = 0; i < _slots.Count; i++)
        {
            var slot = _slots[i];
            slot.RegisterCallback<ClickEvent>(SlotClicked);
            // slot.RegisterCallback<MouseOverEvent>((e)=> EazySoundManager.PlayUISound(uiSoundSO.hover));
            var slotData = gameSaveManager.LoadGameData(i);
            
            slot.Q<Label>("LastDateText").text = slotData.lastPlayDate;
            slot.Q<Label>("PlayTimeText").text = slotData.playTime;

            if (!gameSaveManager.slotDatas[i].isSlotUsed) continue;

            var bosses = slot.Query<VisualElement>("Boss").ToList();
            var openedDoor = new StyleBackground(openedBossDoorIcon);
            var closedDoor = new StyleBackground(closedBossDoorIcon);
            for(int j = 0; j < bosses.Count; j++)
            {
                bosses[j].style.backgroundImage = slotData.clearedBosses[j] ? openedDoor : closedDoor;
            }
        }

        inputReader.OnUIOpenClose += DisableUI;
    }

    private void OnDisable()
    {
        foreach (var slot in _slots)
        {
            slot.UnregisterCallback<ClickEvent>(SlotClicked);
        }
        inputReader.OnUIOpenClose -= DisableUI;
    }

    private void SlotClicked(ClickEvent evt)
    {
        var indexOfSlot = _slots.IndexOf(evt.target as Button);
        gameSaveManager.OpenGame(indexOfSlot);
        _transitionUI.TransitionStart(()=>SceneManager.LoadScene(1));
    }

    public void EnableUI()
    {
        IsOpen = true;
        _container.AddToClassList("on");
        foreach (var btn in Root.Query<Button>().ToList())
        {
            btn.pickingMode = PickingMode.Position;
        }
    }

    public void DisableUI()
    {
        if (IsOpen)
        {
            StartCoroutine(IsOpenFalseDelay());
            _container.RemoveFromClassList("on");
            foreach (var btn in Root.Query<Button>().ToList())
            {
                btn.pickingMode = PickingMode.Ignore;
            }

            _titleUI.TitleOpen(true);
        }
    }

    IEnumerator IsOpenFalseDelay()
    {
        yield return null;
        IsOpen = false;
    }
}
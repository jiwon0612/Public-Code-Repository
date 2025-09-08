using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ToTitleUI : UIToolkitParents
{
    [SerializeField] private GameSaveManagerSO gameSaveManager;
    [SerializeField] private TransitionUI transitionUI;
    [SerializeField] private int titleScene;
    
    private Button _toTitleButton;
    private Label _lastSaveTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        _toTitleButton = Root.Q<Button>("ToTitleButton");
        _lastSaveTime = Root.Q<Label>("LastSaveTime");
        
        _toTitleButton.RegisterCallback<ClickEvent>(ToTitleButtonClicked);
        _toTitleButton.RegisterCallback<MouseOverEvent>(ShowLastSaveTime);
        _toTitleButton.RegisterCallback<MouseOutEvent>(evt => _lastSaveTime.RemoveFromClassList("on"));
    }
    private void OnDisable()
    {
        _toTitleButton.UnregisterCallback<ClickEvent>(ToTitleButtonClicked);
        _toTitleButton.UnregisterCallback<MouseOverEvent>(ShowLastSaveTime);
    }
    private void ShowLastSaveTime(MouseOverEvent evt)
    {
        _lastSaveTime.AddToClassList("on");
        _lastSaveTime.text = $"마지막 저장 시각 : {gameSaveManager.LoadGameData().lastSaveDate}";
    }

    private void ToTitleButtonClicked(ClickEvent clickEvent)
    {
        transitionUI.TransitionStart(() => SceneManager.LoadScene(titleScene));
    }
}

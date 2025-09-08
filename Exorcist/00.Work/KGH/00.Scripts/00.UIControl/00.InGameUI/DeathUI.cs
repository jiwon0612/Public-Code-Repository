using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathUI : UIToolkitParents
{
    [SerializeField] private TransitionUI _transitionUI;
    [SerializeField] private int lobbySceneIndex;

    [SerializeField] private float _deathUIFadeDuration = 0.5f;
    
    private VisualElement _deathPanel;
    private VisualElement _deathLabel;
    private VisualElement _buttonPanel;
    
    private Button _retryButton;
    private Button _lobbyButton;

    protected override void OnEnable()
    {
        base.OnEnable();
        _deathPanel = Root.Q<VisualElement>("Contents");
        _deathLabel = Root.Q<VisualElement>("DeathLabel");
        _buttonPanel = Root.Q<VisualElement>("ButtonPanel");
        
        _retryButton = Root.Q<Button>("RetryButton");
        _lobbyButton = Root.Q<Button>("LobbyButton");
        
        _retryButton.RegisterCallback<ClickEvent>(click => RetryButtonClicked());
        _lobbyButton.RegisterCallback<ClickEvent>(click => LobbyButtonClicked());
    }

    public void ShowDeathUI() => StartCoroutine(ShowDeathUIRoutine());
    
    IEnumerator ShowDeathUIRoutine()
    {
        _deathPanel.AddToClassList("show");
        yield return new WaitForSeconds(_deathUIFadeDuration);
        _deathLabel.RemoveFromClassList("shrink");
        _deathLabel.pickingMode = PickingMode.Position;
        yield return new WaitForSeconds(_deathUIFadeDuration * 2);
        _buttonPanel.AddToClassList("enable");
        _retryButton.pickingMode = PickingMode.Position;
        _lobbyButton.pickingMode = PickingMode.Position;
    }
    
    void RetryButtonClicked()
    {
        Debug.Log("RetryButtonClicked");
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        _transitionUI.TransitionStart(()=>SceneManager.LoadScene(currentScene));
    }
    void LobbyButtonClicked()
    {
        Debug.Log("LobbyButtonClicked");
        _transitionUI.TransitionStart(()=>SceneManager.LoadScene(lobbySceneIndex));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    private MainMenu mainData;
    private Button button;
    private Image image;
    [SerializeField] private int stageNum;

    private void Awake()
    {
        mainData = GetComponentInParent<MainMenu>();
        button = GetComponent<Button>();
        image = transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnNextBtn);
        SetLock();
    }

    private void SetLock()
    {
        image.enabled = !mainData.stageData.isStages[stageNum - 1];
    }

    public void OnNextBtn()
    {
        Debug.Log(stageNum);
        SceneManager.LoadScene(stageNum + 2);
    }
}

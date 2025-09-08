using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

[Serializable]
public struct StageComp
{
    public Image image;
    public Button button;
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<StageComp> stages = new List<StageComp>();
    public StageDataSO stageData;

    private void Awake()
    {
        SetStage();
    }

    private void SetStage()
    {
        int num = 0;
        foreach (Transform item in transform)
        {
            if (item.TryGetComponent(out Image image))
            {
                if (item.TryGetComponent(out Button button))
                {
                    StageComp stage;

                    stage.image = image;
                    stage.button = button;
                    stages.Add(stage);
                    stage.image.raycastTarget = stageData.isStages[num];
                    stage.button.enabled = stageData.isStages[num];
                    num++;
                }
            }
        }
    }

}

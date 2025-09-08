using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHolder : MonoBehaviour
{
    [SerializeField] private ToolDataSO toolData;
    private List<Tool> toolList;

    private int currentToolNum = 0;

    private void Awake()
    {
        toolList = new List<Tool>();
        MakeTool();
        InputManager.Instance.inputR.onChangeTool += ChangeToolKey;
    }

    private void OnDestroy()
    {
    }
    public void MakeTool()
    {
        bool b = false;
        foreach (Tool item in toolData.dataLists)
        {
            GameObject g = Instantiate(item.gameObject ,transform);
            g.gameObject.SetActive(false);
            Tool gComp = g.GetComponent<Tool>();
            toolList.Add(gComp);
            if (!b && gComp.isUse)
            {
                b = true;
                gComp.EnterTool();
                g.gameObject.SetActive(true);
            }
        }

    }

    public void ChangeToolKey(int value)
    {
        if (value == 0)
        {
            ChangeUp();
            return;
        }
        if (value == 1)
        {
            ChangeDown();
            return;
        }

    }

    private void ChangeUp()
    {
        if (currentToolNum + 1 > toolList.Count - 1)
        {
            toolList[currentToolNum].ExitTool();
            toolList[currentToolNum].gameObject.SetActive(false);
            currentToolNum = 0;
            toolList[currentToolNum].EnterTool();
            toolList[currentToolNum].gameObject.SetActive(true);
            return;
        }

        toolList[currentToolNum].ExitTool();
        toolList[currentToolNum].gameObject.SetActive(false);
        currentToolNum++;
        toolList[currentToolNum].EnterTool();
        toolList[currentToolNum].gameObject.SetActive(true);
        return;
    }

    private void ChangeDown()
    {
        if(currentToolNum -1 < 0)
        {
            toolList[currentToolNum].ExitTool();
            toolList[currentToolNum].gameObject.SetActive(false);
            currentToolNum = toolList.Count - 1;
            toolList[currentToolNum].EnterTool();
            toolList[currentToolNum].gameObject.SetActive(true);
            return;
        }
        toolList[currentToolNum].ExitTool();
        toolList[currentToolNum].gameObject.SetActive(false);
        currentToolNum --;
        toolList[currentToolNum].EnterTool();
        toolList[currentToolNum].gameObject.SetActive(true);
        return;

    }
}

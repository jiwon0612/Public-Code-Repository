using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private void Awake()
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(4.5f, 1.125f, 1), 0.25f);
    }

    public void OnClickBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickEndBtn()
    {
        Debug.Log("³ª°£´Ù");
        Application.Quit();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(new Vector3(4,1,1), 0.25f);
    }
}

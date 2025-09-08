using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private InputReader inputR;

    private bool isMenu = false;

    private void Awake()
    {
        menu.SetActive(isMenu);
    }

    private void OnEnable()
    {
        inputR.OnPressESC += OnPressESC;
    }

    private void OnDisable()
    {
        inputR.OnPressESC -= OnPressESC;
        
    }

    public void OnPressESC()
    {
        isMenu = !isMenu;
        Time.timeScale = isMenu ? 0 : 1;
        menu.SetActive(isMenu);
    }

    public void ExteStage()
    {
        SceneManager.LoadScene(1);
    }
}

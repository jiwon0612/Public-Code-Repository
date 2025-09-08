using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapUI : MonoBehaviour
{
    private List<GameObject> icons;

    private void Awake()
    {
        icons = new List<GameObject>();

        ResetItem();
    }

    private void Start()
    {
        ChangedIcon(false);
    }

    private void OnEnable()
    {
        PlayerManager.Instance.OnChangedMapEvent += ChangedIcon;
    }

    private void OnDisable()
    {
        
    }

    private void ResetItem()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var ga = transform.GetChild(i).gameObject;
            icons.Add(ga);
            ga.SetActive(false);
        }
    }

    public void ChangedIcon(bool value)
    {
        int count = Convert.ToInt32(value);
        int nextCount = Convert.ToInt32(!value);
        icons[count].SetActive(true);
        icons[nextCount].SetActive(false);
    }
}

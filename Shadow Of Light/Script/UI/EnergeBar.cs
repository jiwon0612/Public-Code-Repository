using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeBar : MonoBehaviour
{
    [SerializeField]private List<GameObject> icons;


    private void Awake()
    {
        icons = new List<GameObject>();
        ResetItem();
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

    public void AddIcon(int value)
    {
        icons[value - 1].SetActive(true);
    }

    public void UseIcon(int value)
    {

        icons[value].SetActive(false);
        
    }
}

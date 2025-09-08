using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CamManager : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cam;


    private static CamManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static CamManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }


    private void Start()
    {
        //cam.Follow = PlayerManager.Instance.Player.transform;
        
    }


    public void CamShake(float duration, float power)
    {
        cam.transform.DOShakePosition(duration , power);
    }

    public void CamMove(Vector2 position , float duration)
    {
        cam.transform.DOMove(position, duration);
    }

    
}

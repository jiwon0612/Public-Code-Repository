using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using System;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private List<GameObject> shadowMap = new List<GameObject>();
    [SerializeField] private List<GameObject> LightMap = new List<GameObject>();
    //private TilemapCollider2D shadowGroundcollider;
    //private TilemapCollider2D shadowWallcollider;
    //private TilemapCollider2D lightGroundcollider;
    //private TilemapCollider2D lightWallcollider;

    [SerializeField] private Volume volume;
    [SerializeField] private float instensityTime;
    [HideInInspector] public bool isLight = false;
    private bool isChangeing;

    public Action<bool> OnChangedMapEvent;

    private void Awake()
    {
        //shadowGroundcollider = map[Convert.ToInt32(!isLight)].gameObject.GetComponent<TilemapCollider2D>();
        //shadowWallcollider = map[Convert.ToInt32(!isLight)+2].gameObject.GetComponent<TilemapCollider2D>();
        //lightGroundcollider = map[Convert.ToInt32(isLight)].gameObject.GetComponent<TilemapCollider2D>();
        //lightWallcollider = map[Convert.ToInt32(isLight) + 2].gameObject.GetComponent<TilemapCollider2D>();

    }

    private void Start()
    {
        //shadowGroundcollider.enabled = false;
        //shadowWallcollider.enabled = false;
        //lightGroundcollider.enabled = true;
        //lightWallcollider.enabled = true;

        OnOffMaf(!isLight);
    }

    private void OnDisable()
    {
        OnChangedMapEvent = null;
    }

    private void OnOffMaf(bool infor)
    {
        for (int i = 0; i < shadowMap.Count; i++)
        {
            shadowMap[i].SetActive(infor);
        }
        for (int i = 0; i < LightMap.Count; i++)
        {
            LightMap[i].SetActive(!infor);
        }
    }

    public void StartChangeMap()
    {
        StartCoroutine(ChangeMap());
        OnChangedMapEvent?.Invoke(isLight);
    }
    private IEnumerator ChangeMap()
    {
        if (!isChangeing)
        {
            isChangeing = true;
            ColorCurves color;
            ChromaticAberration chromatic;


            if (volume.profile.TryGet(out color))
            {
                color.active = !isLight;
            }

            if (volume.profile.TryGet(out chromatic))
            {
                DOTween.To(() => 0f, x => chromatic.intensity.value = x, 1, instensityTime).SetEase(Ease.Linear);
                OnOffMaf(isLight);
                isLight = !isLight;
            }

            yield return new WaitForSecondsRealtime(instensityTime);

            if (volume.profile.TryGet(out chromatic))
            {
                chromatic.intensity.value = 0;
            }

            isChangeing = false;
        }

    }
}

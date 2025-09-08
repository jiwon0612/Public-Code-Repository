using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    private static StageManager instance = null;

    private GameObject cam1;
    private GameObject mainCam;

    private string currntScene;
    public static StageManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        
    }

    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCam = InputManager.Instance.mainCam;
        cam1 = InputManager.Instance.cam1;
        if (currntScene == scene.name)
        {
            return;
        }
        else
        {
            currntScene = scene.name;
            Debug.Log($"뭔가 다름{currntScene}");
            StartCoroutine(moveCam());
            return;
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private IEnumerator moveCam()
    {
        if (cam1 != null && mainCam != null)
        {
            Debug.Log("코루틴 실행");
            cam1.SetActive(true);
            mainCam.SetActive(false);
            yield return new WaitForSeconds(1f);
            cam1.SetActive(false);
            mainCam.SetActive(true);

        }
    }

}

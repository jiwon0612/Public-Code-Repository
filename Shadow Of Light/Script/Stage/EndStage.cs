using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStage : MonoBehaviour
{
    [SerializeField] private RestZoon rest;
    [SerializeField] private StageDataSO stagedata;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stagedata.isStages[rest.sceneNum - 2] = true;

            SceneManager.LoadScene(1);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUIController : MonoBehaviour
{
    [SerializeField] private GameObject loadingObj;
    [SerializeField] private HealthBar loadingBar;

    [SerializeField] private GameObject blackScreen;
    [SerializeField] private Animator blackScreenAnim;

    public void Start()
    {
        LoadSceneManager.Instance.LoadingObj = loadingObj;
        LoadSceneManager.Instance.OnLoadingSingleProgress += HandleLoadingSingleProgress;
        LoadSceneManager.Instance.OnLoadingAdditiveProgress += HandleLoadingAdditiveProgress;
        LoadSceneManager.Instance.OnAdditiveSceneAlreadyLoaded += Instance_OnAdditiveSceneAlreadyLoaded;
        loadingBar.Init(1f);

        DataPersistenceManager.Instance.LoadOptionData();
        if(blackScreen != null)
            blackScreen.SetActive(true);
    }

    private void OnDisable()
    {
        LoadSceneManager.Instance.OnLoadingAdditiveProgress -= HandleLoadingAdditiveProgress;
        LoadSceneManager.Instance.OnLoadingSingleProgress -= HandleLoadingSingleProgress;
        LoadSceneManager.Instance.OnAdditiveSceneAlreadyLoaded -= Instance_OnAdditiveSceneAlreadyLoaded;
    }

    private void HandleLoadingAdditiveProgress(float obj)
    {
        if(obj == 1 && blackScreen.activeInHierarchy)
        {
            blackScreenAnim?.SetTrigger("FadeOut");
        }
    }
    private void Instance_OnAdditiveSceneAlreadyLoaded()
    {
        blackScreenAnim?.SetTrigger("FadeOut");
    }


    private void HandleLoadingSingleProgress(float progress)
    {
        loadingBar.UpdateHealthBar(1f - progress);
    }
}

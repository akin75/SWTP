using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanva;
    [SerializeField] private Image loadingBarFill;
    [SerializeField] private Animator transition;



    public void LoadScene(int sceneId)
    {   transition.SetTrigger("GameStart"); 
        StartCoroutine(LoadSceneAsync(sceneId));
    }


    IEnumerator LoadSceneAsync(int sceneId)
    {
       
       
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingCanva.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progressValue);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }

}

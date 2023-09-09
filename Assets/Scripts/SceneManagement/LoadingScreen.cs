/* created by: SWT-P_SS_23_akin75 */
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


/// <summary>
/// Load a scene with a sceneid and start a coroutine
/// </summary>
/// <param name="sceneId">SceneId to load</param>
    public void LoadScene(int sceneId)
    {
        transition.SetTrigger("GameStart"); 
        StartCoroutine(LoadSceneAsync(sceneId));
    }



    /// <summary>
    /// Load the scene in asynchronity
    /// </summary>
    /// <param name="sceneId">sceneId to load</param>
    /// <returns>Nothing. IEnumerator function</returns>
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingCanva.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }

}

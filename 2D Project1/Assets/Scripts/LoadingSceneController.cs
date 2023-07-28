using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    private static string nextScene;

    [SerializeField]
    private Image LoadingBar;

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
        Debug.Log("·Îµù");
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation asncOperation = SceneManager.LoadSceneAsync(nextScene);
        asncOperation.allowSceneActivation = false;

        float timer = 0f;

        while(!asncOperation.isDone)
        {
            yield return null;

            if(asncOperation.progress < 0.9f)
            {
                LoadingBar.fillAmount = asncOperation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                LoadingBar.fillAmount = Mathf.Lerp(0.7f, 1f, timer);
                if(LoadingBar.fillAmount >= 1f)
                {
                    asncOperation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}

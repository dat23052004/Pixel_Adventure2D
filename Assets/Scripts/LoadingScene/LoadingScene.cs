using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    private void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    void Update()
    {
        // Press the space key to start coroutine
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ApplicationVariables.loadingSceneName);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            loadingText.text = "Loading " + Mathf.RoundToInt(asyncLoad.progress * 100);
            yield return null;
        }
        loadingText.text = "Loading...100%";
        yield return new WaitForSeconds(2);
        asyncLoad.allowSceneActivation = true;
    }
}

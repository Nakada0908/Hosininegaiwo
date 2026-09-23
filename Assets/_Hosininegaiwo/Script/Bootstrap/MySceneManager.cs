using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;

    private string currentLoadScene = "";
    private bool isLoading = false;

    private List<string> baseScenes = new List<string>
    {
        "Bootstrap",
    };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        if (!baseScenes.Contains(activeSceneName))
        {
            currentLoadScene = activeSceneName;
        }
    }

    public void InitializeGame(string firstSceneName)
    {
        if (isLoading)
        {
            return;
        }

        if (SceneManager.GetSceneByName(firstSceneName).isLoaded)
        {
            return;
        }

        InitializeRoutine(firstSceneName).Forget(Debug.LogException);
    }

    public void ChangeScene(string sceneName)
    {
        if (isLoading)
        {
            return;
        }

        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            return;
        }

        TransitionRoutine(sceneName).Forget(Debug.LogException);
    }

    private async UniTask InitializeRoutine(string firstSceneName)
    {
        isLoading = true;

        await SceneManager.LoadSceneAsync(firstSceneName, LoadSceneMode.Additive);
        currentLoadScene = firstSceneName;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLoadScene));

        isLoading = false;
    }

    private async UniTask TransitionRoutine(string nextSceneName)
    {
        isLoading = true;
        string preSceneName = currentLoadScene;

        //次のシーンを読み込み、完了を待つ
        await SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        //新しいシーンをアクティブにする
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));
        //前のシーンをアンロードし、完了を待つ
        await SceneManager.UnloadSceneAsync(preSceneName);
        currentLoadScene = nextSceneName;

        isLoading = false;
    }
}

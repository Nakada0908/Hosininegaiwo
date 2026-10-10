using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance { get; private set; }

    private string currentLoadScene = "";
    private bool isLoading = false;
    private List<string> baseScenes = new List<string>
    {
        "Bootstrap",
    };

    [SerializeField] private CanvasGroup fadeCanvas;

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

        FadeOut().Forget(Debug.LogException);
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

        await FadeOut();

        isLoading = false;
    }

    private async UniTask TransitionRoutine(string nextSceneName)
    {
        isLoading = true;

        await FadeIn();

        string preSceneName = currentLoadScene;

        //次のシーンを読み込み、完了を待つ
        await SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        //新しいシーンをアクティブにする
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName));
        //前のシーンをアンロードし、完了を待つ
        await SceneManager.UnloadSceneAsync(preSceneName);
        currentLoadScene = nextSceneName;

        await FadeOut();

        isLoading = false;
    }

    private async UniTask FadeIn()
    {
        while (fadeCanvas.alpha < 1f)
        {
            //割合でフェードインする、そしてタイムスケールに依存しないようにする
            fadeCanvas.alpha = Mathf.MoveTowards(fadeCanvas.alpha, 1f, Time.unscaledDeltaTime * 3f);
            await UniTask.NextFrame();
        }
    }

    private async UniTask FadeOut()
    {
        while (fadeCanvas.alpha > 0f)
        {
            fadeCanvas.alpha = Mathf.MoveTowards(fadeCanvas.alpha, 0f, Time.unscaledDeltaTime * 3f);
            await UniTask.NextFrame();
        }
    }
}
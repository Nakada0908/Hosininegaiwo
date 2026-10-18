using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Naninovel;
using Naninovel.UI;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance { get; private set; }

    private string currentLoadScene = "";
    public bool isLoading { get; private set; } = false;
    private List<string> baseScenes = new List<string>
    {
        "Bootstrap",
    };

    private float defaultTimeScale = 1;
    private float stopTimeScale = 0f;
    [SerializeField] private CanvasGroup fadeCanvas;

    //ポーズ中かどうかを判定するプロパティ
    public bool isGamePause { get; private set; }

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

    public async UniTask InitializeGame(string firstSceneName)
    {
        if (isLoading)
        {
            return;
        }

        if (SceneManager.GetSceneByName(firstSceneName).isLoaded)
        {
            return;
        }

        await InitializeRoutine(firstSceneName);
    }

    public async UniTask ChangeScene(string sceneName)
    {
        if (isLoading)
        {
            return;
        }

        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            return;
        }

        await TransitionRoutine(sceneName);
    }

    private async UniTask InitializeRoutine(string firstSceneName)
    {
        isLoading = true;

        try
        {
            ChangeTimeScale();

            await SceneManager.LoadSceneAsync(firstSceneName, LoadSceneMode.Additive);
            currentLoadScene = firstSceneName;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLoadScene));

            await FadeOut();
        }
        finally
        {
            isLoading = false;

            ChangeTimeScale();
        }
    }

    private async UniTask TransitionRoutine(string nextSceneName)
    {
        isLoading = true;

        try
        {
            ChangeTimeScale();

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
        }
        finally
        {
            isLoading = false;

            ChangeTimeScale();
        }
    }

    //ゲーム側から呼び出すことで、ゲームのポーズ状態を切り替える
    public void SetGamePause(bool paused)
    {
        isGamePause = paused;
        ChangeTimeScale();
    }

    private void ChangeTimeScale()
    {
        Time.timeScale = (isGamePause||isLoading) 
            ? stopTimeScale 
            : defaultTimeScale;
    }

    private async UniTask FadeIn()//表示する方
    {
        while (fadeCanvas.alpha < 1f)
        {
            //割合でフェードインする、そしてタイムスケールに依存しないようにする
            fadeCanvas.alpha = Mathf.MoveTowards(fadeCanvas.alpha, 1f, Time.unscaledDeltaTime * 3f);
            await UniTask.NextFrame();
        }
    }

    private async UniTask FadeOut()//消す方
    {
        while (fadeCanvas.alpha > 0f)
        {
            fadeCanvas.alpha = Mathf.MoveTowards(fadeCanvas.alpha, 0f, Time.unscaledDeltaTime * 3f);
            await UniTask.NextFrame();
        }
    }
}
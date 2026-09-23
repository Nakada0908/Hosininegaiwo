using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSEBinder : MonoBehaviour
{
    [SerializeField] private AudioClip clickSE;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        //sceneLoadedを受け取れなかった場合に備え、自分のシーンも登録する
        BindScene(gameObject.scene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindScene(scene);
    }

    private void BindScene(Scene scene)
    {
        //読み込んだシーンのオブジェクトを取得する
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            //切り替えで非表示にしているCanvasの中のボタンも対象にする
            foreach (Button button in root.GetComponentsInChildren<Button>(true))
            {
                button.onClick.AddListener(PlayClickSE);
            }
        }
    }

    private void PlayClickSE()
    {
        SoundManager.Instance.PlaySE(clickSE);
    }
}

using UnityEngine;

public class TitleSettings : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;
    [SerializeField] private string nextSceneName;

    private void Start()
    {
        if (bgm != null)
        {
            SoundManager.Instance.PlayBGM(bgm);
        }
    }

    public void OnClickStartButton()
    {
        MySceneManager.Instance.ChangeScene(nextSceneName);
    }
}

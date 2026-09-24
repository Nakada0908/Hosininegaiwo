using UnityEngine;

public class EndingSettings : MonoBehaviour
{
    [SerializeField] private string titleSceneName = "Title";

    public void OnClickReturnTitle()
    {
        MySceneManager.Instance.ChangeScene(titleSceneName);
    }
}
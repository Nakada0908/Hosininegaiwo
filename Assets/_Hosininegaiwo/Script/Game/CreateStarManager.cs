using UnityEngine;

public class CreateStarManager : MonoBehaviour
{
    [HideInInspector] public bool isSecondRound = false;

    [SerializeField] private GameObject firstStarPrefab;
    [SerializeField] private GameObject secondStarPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float spawnInterval = 1f;

    private bool isSceneChenge;

    private void Start()
    {
        isSceneChenge = false;

        //初回の星生成
        CreateStar();
        //一定間隔で星を生成する
        InvokeRepeating(nameof(CreateStar), spawnInterval, spawnInterval);
    }

    void Update()
    {
        //スコアが10に達したら星の生成を停止
        if (ScoreManeger.Instance.score >= 10)
        {
            CancelInvoke(nameof(CreateStar));
            if (!isSceneChenge)
            {
                isSceneChenge = true;
                MySceneManager.Instance.ChangeScene("Novel");
            }
        }
    }

    private void CreateStar()
    {
        //生成する星のプレハブを選択
        GameObject starPrefab = isSecondRound ? secondStarPrefab : firstStarPrefab;

        //生成位置をランダムに決定
        float x = Random.Range(0.7f, 1.1f);

        //カメラをもとにワールド座標に変換して生成
        Vector3 position = mainCamera.ViewportToWorldPoint(
            new Vector3(x, 1.1f, 0f));

        //カメラと同じZ座標にいるからずらす
        position.z = 0f;

        Instantiate(starPrefab, position, Quaternion.identity);
    }
}

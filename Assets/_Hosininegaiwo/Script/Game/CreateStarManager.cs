using UnityEngine;
using Naninovel;

public class CreateStarManager : MonoBehaviour
{
    [SerializeField] private GameObject firstStarPrefab;
    [SerializeField] private GameObject secondStarPrefab;
    [SerializeField] private GameObject secretStarPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float spawnInterval = 1f;

    [SerializeField, ScriptAssetRef] private string secondStory;
    [SerializeField, ScriptAssetRef] private string finalStory;

    private bool[] randomStar;
    private int starCreateCount = 0;

    private bool isSceneChenge;

    private void Start()
    {
        isSceneChenge = false;

        //ランダムに星を生成するための配列を初期化
        randomStar = new bool[10];
        randomStar[Random.Range(0, randomStar.Length)] = true;

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
            //周回数に応じて,naniの指定
            if (!isSceneChenge)
            {
                isSceneChenge = true;

                CancelInvoke(nameof(CreateStar));

                //.nani指定を変えてからシーンを変える
                NovelGameStarter.nextScript = GameRoundCounter.isSecondRound ? finalStory : secondStory;
                MySceneManager.Instance.ChangeScene("Novel");
            }
        }
    }

    private void CreateStar()
    {
        //生成する星のプレハブを選択
        GameObject starPrefab = GameRoundCounter.isSecondRound ? secondStarPrefab : firstStarPrefab;

        if (GameRoundCounter.isSecondRound
            && starCreateCount < randomStar.Length
            && randomStar[starCreateCount])
        {
            starPrefab = secretStarPrefab;
        }

        //生成位置をランダムに決定
        float x = Random.Range(0.7f, 1.1f);

        //カメラをもとにワールド座標に変換して生成
        Vector3 position = mainCamera.ViewportToWorldPoint(
            new Vector3(x, 1.1f, 0f));

        //カメラと同じZ座標にいるからずらす
        position.z = 0f;

        Instantiate(starPrefab, position, Quaternion.identity);
        starCreateCount++;
    }
}

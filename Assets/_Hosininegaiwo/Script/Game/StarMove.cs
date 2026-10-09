using UnityEngine;

public class StarMove : MonoBehaviour
{
    [SerializeField] private Vector2 direction = new Vector2(-1, -1);
    [SerializeField] private float starSpeed = 3f;
    [SerializeField] private int hitMaxCount = 1;

    private int hitCount = 0;

    private void Start()
    {
        //画面外に残り続けないため
        Destroy(gameObject, 10f); 
    }

    private void Update()
    {
        transform.position += (Vector3)(direction.normalized * starSpeed * Time.deltaTime);
    }

    public void Hit()
    {
        hitCount++;

        if(hitCount >= hitMaxCount)
        {
            //特殊な星の場合、TalkSecretメソッドを呼び出す
            if (GetComponent<SecretStar>() != null)
            {
                GetComponent<SecretStar>().TalkSecret();
            }

            ScoreManeger.Instance.AddScore();
            Destroy(gameObject);
        }
    }
}

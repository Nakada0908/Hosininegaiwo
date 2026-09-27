using UnityEngine;

public class StarMove : MonoBehaviour
{
    [SerializeField] private Vector2 direction = new Vector2(-1, -1);
    [SerializeField] private float starSpeed = 3f;
    [SerializeField] private int hitMaxCount = 1;

    private int hitCount = 0;

    private void Start()
    {
        //‰æ–ÊŠO‚ÉŽc‚è‘±‚¯‚È‚¢‚½‚ß
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
            ScoreManeger.Instance.AddScore();
            Destroy(gameObject);
        }
    }
}

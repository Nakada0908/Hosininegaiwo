using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class ScoreManeger : MonoBehaviour
{
    public static ScoreManeger Instance { get; private set; }

    public int score { get; private set; }
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

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
        score = 0;
        scoreText.text = score + "/10";
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score + "/10";
    }

#if UNITY_EDITOR|| DEVELOPMENT_BUILD
    private void Update()
    {
        if (Keyboard.current.numpad8Key.wasPressedThisFrame)
        {
            AddScore();
        }
    }
#endif
}
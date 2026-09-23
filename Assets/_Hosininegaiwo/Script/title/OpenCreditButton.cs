using UnityEngine;

public class OpenCreditButton : MonoBehaviour
{
    [SerializeField] private Canvas creditCanvas;

    private void Start()
    {
        creditCanvas.gameObject.SetActive(false);
    }

    public void OpenCredits()
    {
        creditCanvas.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        creditCanvas.gameObject.SetActive(false);
    }
}

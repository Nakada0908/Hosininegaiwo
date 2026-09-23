using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource seAudioSource;
    [SerializeField] private AudioSource voiceAudioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        //Ç∑Ç≈Ç…çƒê∂íÜÇÃBGMÇ™Ç†ÇÈèÍçáÇÕí‚é~ÇµÇƒÇ©ÇÁêVÇµÇ¢BGMÇçƒê∂Ç∑ÇÈ
        bgmAudioSource.Stop();
        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void PlaySE(AudioClip clip)
    {
        seAudioSource.PlayOneShot(clip);
    }

    public void PlayVoice(AudioClip clip)
    {
        voiceAudioSource.PlayOneShot(clip);
    }
}
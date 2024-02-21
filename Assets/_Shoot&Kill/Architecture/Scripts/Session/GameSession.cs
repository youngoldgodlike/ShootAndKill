using _Shoot_Kill.Architecture.Scripts;
using UnityEngine;
using UnityEngine.Audio;

public class GameSession : Singleton<GameSession>
{
    public bool UiIsActive { get; set; }
    
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        SetVolume("Master", PlayerPrefs.GetFloat($"MasterVolume", 0.5f));
        SetVolume("Sounds", PlayerPrefs.GetFloat($"SoundsVolume", 0.5f));
        SetVolume("Music", PlayerPrefs.GetFloat($"MusicVolume", 0.5f));
    }
    
    private void SetVolume( string tag, float param)
    {
        var value = Mathf.Lerp(-40f, 0f, param);

        if (param != 0)
            _audioMixer.SetFloat(tag, value);
        else
            _audioMixer.SetFloat(tag, -80);
    }
}

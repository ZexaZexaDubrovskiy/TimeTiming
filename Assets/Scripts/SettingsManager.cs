using TMPro;
using UnityEngine;

public class SettingsManager : Singleton<SettingsManager>
{
    private bool _playSound = true;
    private bool _playMusic = true;
    [SerializeField] private TextMeshProUGUI _soundText;
    [SerializeField] private TextMeshProUGUI _musicText;

    public bool ToggleSound()
    {
        _playSound = !_playSound;
        UpdateSoundText();
        return _playSound;
    }
    public bool ToggleMusic()
    {
        _playMusic = !_playMusic;
        UpdateMusicText();
        return _playMusic;
    }

    private void UpdateSoundText() => _soundText.text = _playSound ? "Sound On" : "Sound Off";
    private void UpdateMusicText() => _musicText.text = _playMusic ? "Music On" : "Music Off";
}
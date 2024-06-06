using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject menuWindow;
    public GameObject settingsWindow;
    private SoundManager _soundManager;
    private SettingsManager _settingsManager;
    private Spawner _spawner;
    private ScoreManager _scoreManager;
    private HeartManager _heartManager;
    private Player _player;

    private void Start()
    {
        InitializeManagers();
        StartLevel();
    }

    private void InitializeManagers()
    {
        _soundManager = GetComponent<SoundManager>();
        _settingsManager = SettingsManager.Instance;
        _spawner = Spawner.Instance;
        _scoreManager = ScoreManager.Instance;
        _heartManager = HeartManager.Instance;
        _player = Player.Instance;
    }

    private void Update()
    {
        _soundManager.PlaySound(0, playIfNotAlreadyPlaying: true);
        
        
    }

    public void StartLevel()
    {
        _spawner.AllDestroyItem();
        _scoreManager.ResetScore();
        _heartManager.ResetHeart();
        _player.transform.position = Vector2.zero;

        _soundManager.PlaySound(0, stopCurrent: true);

        _spawner.StartSpawnWalls();
        _spawner.ResetBackground();
    }

    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
        menuWindow.SetActive(false);
    }

    public void OpenMenu()
    {
        Time.timeScale = 0.0f;
        menuWindow.SetActive(true);
    }

    public void CloseSettings() => settingsWindow.SetActive(false);
    public void OpenSettings() => settingsWindow.SetActive(true);

    public void OffOnMusic()
    {
        bool offOnMusic = _settingsManager.ToggleMusic();
        _soundManager.PlaySound(0, stopCurrent: true, volume: offOnMusic ? 1f : 0f);
    }

    public void OffOnSound()
    {
        bool offOnSound = _settingsManager.ToggleSound();
        _player.GetComponent<AudioSource>().enabled = offOnSound;
    }

}
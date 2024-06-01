using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : Singleton<HeartManager>
{
    [SerializeField] private Image[] _hearts;
    private int _currentHealth;
    public int CurrentHealth { 
        get => _currentHealth; 
        set => _currentHealth = Mathf.Clamp(value, 0, _hearts.Length);
    }

    public void UpdateHearts(int currentHealth)
    {
        CurrentHealth = currentHealth;

        for (int i = 0; i < _hearts.Length; i++)
            _hearts[i].enabled = i < CurrentHealth;
    }

    public void ResetHeart()
    {
        CurrentHealth = _hearts.Length;
        UpdateHearts(CurrentHealth);
    }

}

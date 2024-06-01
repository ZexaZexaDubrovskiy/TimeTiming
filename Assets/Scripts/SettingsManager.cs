using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class SettingsManager : Singleton<SettingsManager>
{

    //TODO переписать весь класс
    public Button soungButton;
    [SerializeField] private Sprite[] _spiteSounds;
    private bool _isSoundOn;


    public void SoundsOnOff()
    {
        if (_isSoundOn)
        {
            soungButton.image.sprite = _spiteSounds[0];
        } else
        {
            soungButton.image.sprite = _spiteSounds[1];
        }

        _isSoundOn = !_isSoundOn;

    }


}

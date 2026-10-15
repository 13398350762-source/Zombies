using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;
    public Button btnExit;
    protected override void Init()
    {
        togMusic.onValueChanged.AddListener((value) =>{
            GameDataManager.Instance.SetMusicIsOpen(value);
        });

        togSound.onValueChanged.AddListener((value) => {
            GameDataManager.Instance.SetSoundIsOpen(value);
        });

        sliderMusic.onValueChanged.AddListener((value) =>{
            GameDataManager.Instance.SetMusicValue(value);
        });

        sliderSound.onValueChanged.AddListener((value) => {
            GameDataManager.Instance.SetSoundValue(value);
        });

        btnExit.onClick.AddListener(() =>{
            UIManager.Instance.HidePanel<SettingPanel>();
            GameDataManager.Instance.SaveMusicData();
        });

        sliderMusic.value = GameDataManager.Instance.musicData.musicValue;
        sliderSound.value = GameDataManager.Instance.musicData.soundValue;
        togMusic.isOn = GameDataManager.Instance.musicData.isMusicOpen;
        togSound.isOn = GameDataManager.Instance.musicData.isSoundOpen;
    }

}

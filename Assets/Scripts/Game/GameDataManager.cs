using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    private static GameDataManager instance=new GameDataManager();
    public static GameDataManager Instance=>instance;
    private MusicData musicdata;
    public MusicData musicData => musicdata;
    public List<RoleInfo> roleList;
    public List<SceneInfo> sceneList;
    private PlayerData playerdata;
    public PlayerData playerData => playerdata;
    public List<ZombieInfo> zombieList;
    public List<TowerInfo> towerList;
    public RoleInfo nowRole;
    public SceneInfo nowScene;

    private GameDataManager() 
    {
        musicdata = JsonManager.Instance.Load<MusicData>("MusicData");

        roleList = JsonManager.Instance.Load<List<RoleInfo>>("RoleData.json");

        sceneList = JsonManager.Instance.Load<List<SceneInfo>>("SceneData.json");

        zombieList = JsonManager.Instance.Load<List<ZombieInfo>>("ZombieData.json");

        playerdata = JsonManager.Instance.Load<PlayerData>("PlayerData.json");

        towerList = JsonManager.Instance.Load<List<TowerInfo>>("TowerData.json");
    }

    public void SetMusicValue(float value)
    {
        if (value >= 0 && value <= 1)
        {
            musicData.musicValue = value;
            BKMusic.Instance.SetMusicValue(value);
        }
    }

    public void SetMusicIsOpen(bool isOpen)
    {
        musicData.isMusicOpen = isOpen;
        BKMusic.Instance.SetMusicIsOpen(isOpen);
    }

    public void SetSoundValue(float value)
    {
        if (value >= 0 && value <= 1)
        {
            musicData.soundValue = value;
        }
    }

    public void SetSoundIsOpen(bool isOpen)
    {
        musicData.isSoundOpen = isOpen;
    }

    public void SaveMusicData()
    {
        JsonManager.Instance.Save(musicdata, "MusicData");
    }

    public void SavePlayerData()
    {
        JsonManager.Instance.Save(playerdata, "PlayerData");
    }

    public void SoundPlay(string res)
    {
        GameObject obj = new GameObject();
        AudioSource a=obj.AddComponent<AudioSource>();
        a.clip=Resources.Load<AudioClip>(res);
        a.volume = musicdata.soundValue;
        a.mute = !musicdata.isSoundOpen;
        a.Play();

        GameObject.Destroy(obj,1f);
    }

}

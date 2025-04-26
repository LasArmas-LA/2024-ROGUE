using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // AudioClipを管理するClass
    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip audioClip;
    }

    [SerializeField]
    private SoundData[] soundDatas;
    //別名(name)をキーとした管理用Dictionary
    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource audioSource_BGM;
    [SerializeField]
    private AudioSource audioSource_SE;

    private void Awake()
    {
        //soundDictionaryにセット
        foreach (var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }
    }

    public void PlaySE(AudioClip clip)
    {
        if (audioSource_SE == null) return; //再生できませんでした
        audioSource_SE.PlayOneShot(clip);
    }

    public void PlaySE(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData)) //管理用Dictionary から、別名で探索
        {
            PlaySE(soundData.audioClip); //見つかったら、再生
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{name}");
        }
    }
}

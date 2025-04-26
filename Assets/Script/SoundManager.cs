using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // AudioClip���Ǘ�����Class
    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip audioClip;
    }

    [SerializeField]
    private SoundData[] soundDatas;
    //�ʖ�(name)���L�[�Ƃ����Ǘ��pDictionary
    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioSource audioSource_BGM;
    [SerializeField]
    private AudioSource audioSource_SE;

    private void Awake()
    {
        //soundDictionary�ɃZ�b�g
        foreach (var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }
    }

    public void PlaySE(AudioClip clip)
    {
        if (audioSource_SE == null) return; //�Đ��ł��܂���ł���
        audioSource_SE.PlayOneShot(clip);
    }

    public void PlaySE(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData)) //�Ǘ��pDictionary ����A�ʖ��ŒT��
        {
            PlaySE(soundData.audioClip); //����������A�Đ�
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{name}");
        }
    }
}

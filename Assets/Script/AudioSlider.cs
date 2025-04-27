using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup group;

    [SerializeField]
    [Tooltip("�X���C�_�[�̍ŏ��f�V�x���l")]
    private float minDB = -80;
    [SerializeField]
    [Tooltip("�X���C�_�[�̍ő�f�V�x���l")]
    private float maxDB = 3;
    [SerializeField]
    [Tooltip("����`�Ȑ��ɑΉ����邽�߂̃X���C�_�[�̃X�P�[���l")]
    [Range(0.1f, 1f)]
    private float linearity = 0.5f;

    [SerializeField]
    [HideInInspector]
    private Slider _slider;

    private void Awake()
    {
        if (!_slider) _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        LoadValue();
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void Start()
    {
        OnValueChanged(_slider.value);
    }

    private void LoadValue()
    {
        group.audioMixer.GetFloat(group.name, out var decibels);
        var normalized = math.saturate(math.remap(minDB, maxDB, 0, 1, decibels));
        var nonlinear = math.pow(normalized, 1f / linearity);
        var exponential = math.pow(10f, nonlinear);
        var remapped = math.remap(1, 10, 0, 1, exponential);
        _slider.value = remapped;
    }

    private void OnValueChanged(float sliderValue)
    {
        var remapped = math.remap(0, 1, 1, 10, sliderValue);
        var logarithmic = math.saturate(math.log10(remapped));
        var nonlinear = math.pow(logarithmic, linearity);
        var decibels = math.remap(0f, 1f, minDB, maxDB, nonlinear);
        group.audioMixer.SetFloat(group.name, decibels);
    }

    private void OnValidate()
    {
        if (!_slider) _slider = GetComponent<Slider>();
        _slider.minValue = 0;
        _slider.maxValue = 1;
    }
}

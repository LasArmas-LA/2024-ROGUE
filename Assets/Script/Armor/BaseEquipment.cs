using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "parts�̐���")]
public class BaseEquipment : ScriptableObject
{
    public string equipmentName;        // ����̖��O
    public EquipmentType equipmentType; // �����̎��
    public int HP;                      // �q�b�g�|�C���g
    public int ATK;                     // �U����
    public int DEF;                     // �h���
    public int ROT;                     // ��𗦁i�H�j
    public Sprite sprite = null;        //�摜

    public enum EquipmentType
    {
        RightHand, // �E��i���j
        LeftHand,  // ����i����j
        Head,      // ��
        Body,      // ��
        Feet       // ��
    }
}




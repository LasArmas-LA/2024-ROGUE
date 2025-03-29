using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DhiaSkillList : MonoBehaviour
{
    [System.Serializable]
    public class AtackSkillStatus
    {
        //ID
        [CustomLabel("ID")]
        public string id;

        //���O
        [CustomLabel("���O")]
        public string name;

        //�З�
        [CustomLabel("�З�")]
        public int power;

        //MP�����(�g�����s��)
        [CustomLabel("MP�����")]
        public int useMp;

        //������
        [CustomLabel("������(%)")]
        public int hitRate;


        //����
        public enum eAtkBuffType
        {
            _ATK,
            _BUFF,
            _DEBUFF
        }
        //����
        [CustomLabel("����")]
        public eAtkBuffType atkBuffType;

        //�^�[�Q�b�g�I���̗L��
        public enum eCharSlectType
        {
            //�����S�̌���
            _ALLCHARA,
            //�����̑Ώۂ�I�΂���
            _PICKCHARA,

            //�G�S�̌���
            _ALLENEMY,
            //�G�̑Ώۂ�I�΂���
            _PICKENEMY,

            //�������ʗp(�g�����s��)
            _RIRI,
            _DHIA,
            
            _ENEMY1,
            _ENEMY2,
            _ENEMY3,

        }
        //����
        [CustomLabel("�^�[�Q�b�g")]
        public eCharSlectType charSlectType;

        //�␳�l�̗L��
        public enum eCorrectionType
        {
            //�␳�l����
            _NO,
            //�␳�l����
            _ATK,
            _DEF,
            _HP,
            _HITRATE
        }

        [CustomLabel("�␳�l�̗L��")]
        public eCorrectionType correctionType;

        //���ʎ����^�[����
        [CustomLabel("���ʎ����^�[����")]
        public int effectTurn;

        //���ʂ̏����^�[����
        [CustomLabel("���ʂ̏����^�[����")]
        public int waitTurn;

        //�U����
        [CustomLabel("�U����")]
        public int attackCount;

        //�␳�l�p
        [CustomLabel("�␳�l(%)")]
        public int correctionValue;

        //SE
        [CustomLabel("SE")]
        public AudioClip seClip;
    }

    [System.Serializable]
�@�@public class DefenseSkillStatus
    {
        //ID
        [CustomLabel("ID")]
        public string id;

        //���O
        [CustomLabel("���O")]
        public string name;

        //�З�
        [CustomLabel("�З�")]
        public int power;

        //MP�����(�g�����s��)
        [CustomLabel("MP�����")]
        public int useMp;

        //������
        [CustomLabel("������(%)")]
        public int hitRate;


        //����
        public enum eDefBuffType
        {
            _DEF,
            _BUFF,
            _DEBUFF
        }
        //����
        [CustomLabel("����")]
        public eDefBuffType defBuffType;

        //�^�[�Q�b�g�I���̗L��
        public enum eCharSlectType
        {
            //�����S�̌���
            _ALLCHARA,
            //�����̑Ώۂ�I�΂���
            _PICKCHARA,

            //�G�S�̌���
            _ALLENEMY,
            //�G�̑Ώۂ�I�΂���
            _PICKENEMY,

            //�������ʗp(�g�����s��)
            _RIRI,
            _DHIA,

            _ENEMY1,
            _ENEMY2,
            _ENEMY3,

        }
        //����
        [CustomLabel("�^�[�Q�b�g")]
        public eCharSlectType charSlectType;

        //�␳�l�̗L��
        public enum eCorrectionType
        {
            //�␳�l����
            _NO,
            //�␳�l����
            _ATK,
            _DEF,
            _HP,
            _HITRATE
        }

        [CustomLabel("�␳�l�̗L��")]
        public eCorrectionType correctionType;

        //���ʎ����^�[����
        [CustomLabel("���ʎ����^�[����")]
        public int effectTurn;

        //���ʂ̏����^�[����
        [CustomLabel("���ʂ̏����^�[����")]
        public int waitTurn;

        //�U����
        [CustomLabel("�U����")]
        public int attackCount;

        //�␳�l�p
        [CustomLabel("�␳�l(%)")]
        public int correctionValue;

        //SE
        [CustomLabel("SE")]
        public AudioClip seClip;
    }

    //���X�g�̎���
    [SerializeField]
    public List<AtackSkillStatus>   atkSkillList = new List<AtackSkillStatus>();
    [SerializeField]
    public List<DefenseSkillStatus> defSkillList = new List<DefenseSkillStatus>();
}

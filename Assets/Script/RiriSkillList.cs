using System.Collections.Generic;
using UnityEngine;

public class RiriSkillList : MonoBehaviour
{
    [System.Serializable]
    public class SkillStatus
    {
        //ID
        [CustomLabel("ID")]
        public string id;

        //���O
        [CustomLabel("���O")]
        public string name;

        //MP�����(�g�����s��)
        [CustomLabel("MP�����")]
        public int useMp;

        //������
        [CustomLabel("������(%)")]
        public int hitRate;


        //����
        public enum eAtkDefType
        {
            _ATK,
            _DEF,
            _BUFF,
            _DEBUFF
        }
        //����
        [CustomLabel("����")]
        public eAtkDefType atkDefType;

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
    List<SkillStatus> skillList = new List<SkillStatus>();
}

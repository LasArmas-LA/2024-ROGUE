using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rabbit : EnemyManager
{
    [SerializeField]
    Status enemyStatus = null;

    public float rabbitMaxhp = 0;
    public float rabbitMaxmp = 0;

    public float rabbitHp = 0;
    public float rabbitMp = 0;
    public int rabbitPower = 0;
    public int rabbitDef = 0;


    [Header("�N���X�Q��")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;


    [SerializeField]
    EnemyManager enemySys = null;

    //�U���͂̕␳�l
    float powerValue = 0f;

    //�������̃A�j���[�V����
    public Animator rabbitAnim = null;

    float timerRabbit = 0;
    public bool timerFlag = false;



    public override void InitRabbit()
    {
        Debug.Log("������");
        deathFlag = false;
        
        this.gameObject.transform.localScale =  new Vector3(1,1,1);

        rabbitMaxhp = enemyStatus.MAXHP;
        rabbitMaxmp = enemyStatus.MAXMP;
        rabbitPower = enemyStatus.ATK;
        rabbitDef = enemyStatus.DEF;

        hp = maxhp;
        mp = maxmp;
    }

    void Update()
    {
        if (timerFlag)
        {
            timerRabbit += Time.deltaTime;

            if(timerRabbit >= 1.0f)
            {
                rabbitAnim.SetBool("Attack", false);
                rabbitAnim.SetBool("Damage2", false);
            }
            if (timerRabbit >= 3.5f)
            {
                riri.ririAnim.SetBool("R_TakeDamage", false);
                dhia.dhiaAnim.SetBool("D_TakeDamage", false);

                timerRabbit = 0;
                timerFlag = false;
            }
        }
    }


    //�_���[�W�e�L�X�g�\���p
    [SerializeField]
    TextMeshProUGUI[] damageText = null;
    [SerializeField]
    GameObject[] damageTextObj = null;


    public override void SkilRabbit()
    {
        int skilRnd = 0;
        int slectNo = 0;

        timerFlag = true;
        rabbitAnim.SetBool("Attack", true);
        for (int i = 0; i < 1; i++)
        {
            //0����100�̗���
            skilRnd = UnityEngine.Random.Range(1, 100);
        }

        //�X�L��1
        if(skilRnd <= 100)
        {
            
            float ririDamage = DamageCalculation(rabbitPower, riri.def);
            float dhiaDamage = DamageCalculation(rabbitPower, dhia.def);

            //�_���[�W��0��������Ă鎞��0�_���[�W�ɏ�������
            if(ririDamage <= 0)
            {
                ririDamage = 0;
            }
            if (dhiaDamage <= 0)
            {
                dhiaDamage = 0;
            }

            //�����[�̕���HP������
            if (riri.hp > dhia.hp)
            {
                slectNo = 0;
            }
            //�f�B�A�̕���HP������
            else
            {
                slectNo = 1;
            }

            //�f�B�A������ł��鎞�U���Ώۂ������[�ɏ㏑��
            if (dhia.deathFlag)
            {
                slectNo = 0;
            }

            //�U���Ώۃ����[
            if (slectNo == 0)
            {
                Invoke("RiriDamage", 1.2f);

                //�e�L�X�g�̕\������
                damageTextObj[0].SetActive(true);

                //70%�y��
                if (dhia.ririDefenseFlag)
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A�������[��������I�f�B�A��" + ( ririDamage * 0.3f) + "�̃_���[�W!";
                    dhia.hp -= (ririDamage * 0.3f);
                    damageText[0].text = (ririDamage * 0.3f).ToString();
                }
                else
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�����[��" + (ririDamage) + "�̃_���[�W!";
                    riri.hp -= (ririDamage);
                    damageText[0].text = (ririDamage).ToString();
                }


                encountSys.HpMoveWait("Riri");
            }
            //�U���Ώۃf�B�A
            else if (slectNo == 1)
            {
                Invoke("DhiaDamage", 1.2f);

                //�e�L�X�g�̕\������
                damageTextObj[1].SetActive(true);

                if (dhia.defenseFlag)
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A��" + (dhiaDamage * 0.5f) + "�̃_���[�W!";
                    dhia.hp -= (dhiaDamage * 0.5f);
                    damageText[1].text = (dhiaDamage * 0.5f).ToString();
                }
                else
                {
                    encountSys.windowsMes.text = "�E�T�M�̂��������I�f�B�A��" + (dhiaDamage) + "�̃_���[�W!";
                    dhia.hp -= (dhiaDamage);
                    damageText[1].text = (dhiaDamage).ToString();
                }

                encountSys.HpMoveWait("Dhia");
            }

            StartCoroutine("DamageInit");
        }

        //�X�L��2
        if (skilRnd >= 710)
        {
            encountSys.windowsMes.text = "�E�T�M�͂ɂ񂶂�V�`���[�����񂾁I\n�E�T�M�̍U���͂�15%�A�b�v�����I";
            powerValue += 0.15f;
        }

    }

    void RiriDamage()
    {
        riri.ririAnim.SetBool("R_TakeDamage", true);
    }
    void DhiaDamage()
    {
        dhia.dhiaAnim.SetBool("D_TakeDamage", true);
    }

    //�_���[�W�v�Z�p
    int DamageCalculation(int attack,int defense)
    {
        //�V�[�h�l�̕ύX
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //�f�̃_���[�W�v�Z
        int damage = ((attack + (attack * (int)powerValue))/2) - (defense / 4);

        //�_���[�W�U���̌v�Z
        int width = damage / 16 + 1;

        //�_���[�W�U���l�����������v�Z
        damage = UnityEngine.Random.Range(damage - width, damage + width);

        //�Ăяo�����Ƀ_���[�W����Ԃ�
        return damage;
    }

    IEnumerator DamageInit()
    {
        yield return new WaitForSeconds(0.5f);

        damageTextObj[0].SetActive(false);
        damageTextObj[1].SetActive(false);

        damageText[0].text = "0";
        damageText[1].text = "0";
    }

}
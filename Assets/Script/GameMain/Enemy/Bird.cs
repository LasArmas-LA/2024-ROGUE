using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bird : EnemyManager
{

    public float birdMaxhp = 0;
    public float birdMaxmp = 0;

    public float birdHp = 0;
    public float birdMp = 0;
    public int birdPower = 0;
    public int birdDef = 0;

    [SerializeField]
    Status enemyStatus = null;

    [Header("�N���X�Q��")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;

    [SerializeField]
    EnemyManager enemySys = null;

    //�A�j���[�V�����Ǘ��p
    [SerializeField]
    public Animator birdAnim = null;
    float timerBird = 0;
    public bool timerFlag = false;


    //�U���͂̕␳�l
    float powerValue = 0f;


    public override void InitBird()
    {
        Debug.Log("������");

        deathFlag = false;

        this.gameObject.transform.localScale = new Vector3(1,1,1);

        birdMaxhp = enemyStatus.MAXHP;
        birdMaxmp = enemyStatus.MAXMP;
        birdPower = enemyStatus.ATK;
        birdDef = enemyStatus.DEF;


        hp = maxhp;
        mp = maxmp;
    }

    void Update()
    {
        if (timerFlag)
        {
            timerBird += Time.deltaTime;

            if (timerBird >= 3.5f)
            {
                birdAnim.SetBool("Eb_Attack1", false);
                birdAnim.SetBool("Eb_Damage2", false);
                riri.ririAnim.SetBool("R_TakeDamage", false);
                dhia.dhiaAnim.SetBool("D_TakeDamage", false);

                timerBird = 0;
                timerFlag = false;
            }
        }
    }

    //�_���[�W�e�L�X�g�\���p
    [SerializeField]
    TextMeshProUGUI[] damageText = null;
    [SerializeField]
    GameObject[] damageTextObj = null;

    public override void SkilBird()
    {
        timerFlag = true;
        birdAnim.SetBool("Eb_Attack1",true);
        int skilRnd = 0;
        int slectNo = 0;
        for (int i = 0; i < 1; i++)
        {
            //0����100�̗���
            skilRnd = UnityEngine.Random.Range(1, 100);
        }

        //�X�L��1
        if (skilRnd <= 100)
        {
            int ririDamage = DamageCalculation(birdPower, riri.def);
            float dhiaDamage = DamageCalculation(birdPower, dhia.def);

            //�_���[�W��0��������Ă鎞��0�_���[�W�ɏ�������
            if (ririDamage <= 0)
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
                slectNo = 1;
            }
            //�f�B�A�̕���HP������
            else
            {
                slectNo = 0;
            }

            //�f�B�A������ł��鎞�U���Ώۂ������[�ɏ㏑��
            if (dhia.deathFlag)
            {
                slectNo = 0;
            }

            //�U���Ώۃ����[
            if (slectNo == 0)
            {
                Invoke("RiriDamage", 1f);
                timerFlag = true;

                //�e�L�X�g�̕\������
                damageTextObj[0].SetActive(true);

                //70%�y��
                if (dhia.ririDefenseFlag)
                {
                    encountSys.windowsMes.text = "�ӂ��낤�̂��������I�f�B�A�������[��������I�f�B�A��" + (ririDamage * 0.3f) + "�̃_���[�W!";
                    dhia.hp -= (ririDamage * 0.3f);
                    damageText[0].text = (ririDamage * 0.3f).ToString();
                }
                else
                {
                    encountSys.windowsMes.text = "�ӂ��낤�̂��������I�����[��" + (ririDamage) + "�̃_���[�W!";
                    riri.hp -= (ririDamage);
                    damageText[0].text = (ririDamage).ToString();
                }
                encountSys.HpMoveWait("Riri");
            }
            //�U���Ώۃf�B�A
            else if (slectNo == 1)
            {
                Invoke("DhiaDamage", 1f);

                //�e�L�X�g�̕\������
                damageTextObj[1].SetActive(true);

                timerFlag = true;

                if (dhia.defenseFlag)
                {
                    encountSys.windowsMes.text = "�ӂ��낤�̂��������I�f�B�A��" + (dhiaDamage * 0.5f) + "�̃_���[�W!";
                    dhia.hp -= (dhiaDamage * 0.5f);
                    damageText[1].text = (dhiaDamage * 0.5f).ToString();
                }
                else
                {
                    encountSys.windowsMes.text = "�ӂ��낤�̂��������I�f�B�A��" + (dhiaDamage) + "�̃_���[�W!";
                    dhia.hp -= (dhiaDamage);
                    damageText[1].text = (dhiaDamage).ToString();
                }
                encountSys.HpMoveWait("Dhia");
            }
        }

        //�X�L��2
        if (skilRnd >= 1000)
        {

        }

        StartCoroutine("DamageInit");
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
    int DamageCalculation(int attack, int defense)
    {
        //�V�[�h�l�̕ύX
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        //�f�̃_���[�W�v�Z
        int damage = ((attack + (attack * (int)powerValue)) / 2) - (defense / 4);

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
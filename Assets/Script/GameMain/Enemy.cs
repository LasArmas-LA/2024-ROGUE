using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Status[] enemyStatus = null;

    public float maxhp = 0;
    public float maxmp = 0;

    public float hp = 0;
    public float mp = 0;
    public float power = 0;

    public bool deathFlag = false;

    [SerializeField]
    GameObject enemyMain = null;

    [Header("�N���X�Q��")]
    [SerializeField]
    Riri riri = null;
    [SerializeField]
    Dhia dhia = null;

    [SerializeField]
    TestEncount encountSys = null;

    int rnd = 0;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        Debug.Log(enemyStatus.Length);
        rnd = Random.Range(0, enemyStatus.Length);

        maxhp = enemyStatus[rnd].MAXHP;
        maxmp = enemyStatus[rnd].MAXMP;
        power = enemyStatus[rnd].ATK;
        hp = maxhp;
        mp = maxmp;
        enemyMain.transform.localScale = new Vector3(1, 1, 1);
        deathFlag = false;
    }


    void Update()
    {
        if(hp <= 0)
        {
            deathFlag = true;
            enemyMain.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void Skil()
    {
        Debug.Log("�G�l�~�[");

        int rnd = 0;
        for (int i = 0; i < 1; i++)
        {
            rnd = UnityEngine.Random.Range(0, 2);
        }
        //�f�B�A������ł��鎞�U���Ώۂ������[�ɏ㏑��
        if (dhia.deathFlag)
        {
            rnd = 0;
        }

        //�U���Ώۃ����[
        if (rnd == 0)
        {
            //70%�y��
            if (dhia.ririDefenseFlag)
            {
                encountSys.windowsMes.text = "�Ă��̂��������I�f�B�A�������[��������I�f�B�A��" + power * 0.3f + "�̃_���[�W!";
                dhia.hp -= (power * 0.3f);
            }
            else
            {
                encountSys.windowsMes.text = "�Ă��̂��������I�����[��" + power + "�̃_���[�W!";
                riri.hp -= power;
            }

        }
        //�U���Ώۃf�B�A
        else if (rnd == 1)
        {
            if (dhia.defenseFlag)
            {
                encountSys.windowsMes.text = "�Ă��̂��������I�f�B�A��" + power * 0.5f + "�̃_���[�W!";
                dhia.hp -= (power * 0.5f);
            }
            else
            {
                encountSys.windowsMes.text = "�Ă��̂��������I�f�B�A��" + power + "�̃_���[�W!";
                dhia.hp -= power;
            }
        }
    }
}

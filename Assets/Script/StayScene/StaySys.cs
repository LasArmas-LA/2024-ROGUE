using UnityEngine;
using UnityEngine.SceneManagement;

public class StaySys : MonoBehaviour
{
    //�����[�̃X�e�[�^�X
    [SerializeField, Header("�����[�̃X�e�[�^�X�Ǘ��p")]
    Status ririStatus = null;

    //�f�B�A�̃X�e�[�^�X
    [SerializeField, Header("�f�B�A�̃X�e�[�^�X�Ǘ��p")]
    Status dhiaStatus = null;

    [SerializeField]
    GameObject stayWin = null;

    //�t�F�[�h�A�j���[�V�����p
    [SerializeField]
    Animator fadeAnim = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //HP�񕜃{�^���������ꂽ���̏���
    public void HpHeel()
    {
        //HP��50%��
        //�����[�̉񕜗ʂ��}�b�N�XHP�𒴂��Ă��܂���
        if (ririStatus.HP + ririStatus.MAXHP * 0.5f >= ririStatus.MAXHP)
        {
            ririStatus.HP = ririStatus.MAXHP;
        }
        //�����[��MaxHP�𒴂��Ȃ���
        else
        {
            ririStatus.HP += (ririStatus.MAXHP * 0.5f);
        }

        //�f�B�A�̉񕜗ʂ��}�b�N�XHP�𒴂��Ă��܂���
        if (dhiaStatus.HP + dhiaStatus.MAXHP * 0.5f >= dhiaStatus.MAXHP)
        {
            dhiaStatus.HP = dhiaStatus.MAXHP;
        }
        //�f�B�A��MaxHP�𒴂��Ȃ���
        else
        {
            dhiaStatus.HP += (dhiaStatus.MAXHP * 0.5f);
        }
        stayWin.SetActive(false);
        fadeAnim.SetBool("FadeIn", true);
        Invoke("StageChenge",1f);
    }

    void StageChenge()
    {
        SceneManager.LoadScene("LoadScene");

    }
}

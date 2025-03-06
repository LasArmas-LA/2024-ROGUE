using UnityEngine;
using UnityEngine.SceneManagement;

public class StaySys : MonoBehaviour
{
    //リリーのステータス
    [SerializeField, Header("リリーのステータス管理用")]
    Status ririStatus = null;

    //ディアのステータス
    [SerializeField, Header("ディアのステータス管理用")]
    Status dhiaStatus = null;

    [SerializeField]
    GameObject stayWin = null;

    //フェードアニメーション用
    [SerializeField]
    Animator fadeAnim = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //HP回復ボタンを押された時の処理
    public void HpHeel()
    {
        //HPを50%回復
        //リリーの回復量がマックスHPを超えてしまう時
        if (ririStatus.HP + ririStatus.MAXHP * 0.5f >= ririStatus.MAXHP)
        {
            ririStatus.HP = ririStatus.MAXHP;
        }
        //リリーのMaxHPを超えない時
        else
        {
            ririStatus.HP += (ririStatus.MAXHP * 0.5f);
        }

        //ディアの回復量がマックスHPを超えてしまう時
        if (dhiaStatus.HP + dhiaStatus.MAXHP * 0.5f >= dhiaStatus.MAXHP)
        {
            dhiaStatus.HP = dhiaStatus.MAXHP;
        }
        //ディアのMaxHPを超えない時
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

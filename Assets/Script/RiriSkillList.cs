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

        //名前
        [CustomLabel("名前")]
        public string name;

        //MP消費量(使うか不明)
        [CustomLabel("MP消費量")]
        public int useMp;

        //命中率
        [CustomLabel("命中率(%)")]
        public int hitRate;


        //分類
        public enum eAtkDefType
        {
            _ATK,
            _DEF,
            _BUFF,
            _DEBUFF
        }
        //実体
        [CustomLabel("分類")]
        public eAtkDefType atkDefType;

        //ターゲット選択の有無
        public enum eCharSlectType
        {
            //味方全体効果
            _ALLCHARA,
            //味方の対象を選ばせる
            _PICKCHARA,

            //敵全体効果
            _ALLENEMY,
            //敵の対象を選ばせる
            _PICKENEMY,

            //強制効果用(使うか不明)
            _RIRI,
            _DHIA,

            _ENEMY1,
            _ENEMY2,
            _ENEMY3,

        }
        //実体
        [CustomLabel("ターゲット")]
        public eCharSlectType charSlectType;

        //補正値の有無
        public enum eCorrectionType
        {
            //補正値無し
            _NO,
            //補正値あり
            _ATK,
            _DEF,
            _HP,
            _HITRATE
        }

        [CustomLabel("補正値の有無")]
        public eCorrectionType correctionType;

        //効果持続ターン数
        [CustomLabel("効果持続ターン数")]
        public int effectTurn;

        //効果の準備ターン数
        [CustomLabel("効果の準備ターン数")]
        public int waitTurn;

        //攻撃回数
        [CustomLabel("攻撃回数")]
        public int attackCount;

        //補正値用
        [CustomLabel("補正値(%)")]
        public int correctionValue;

        //SE
        [CustomLabel("SE")]
        public AudioClip seClip;
    }

    //リストの実体
    [SerializeField]
    List<SkillStatus> skillList = new List<SkillStatus>();
}

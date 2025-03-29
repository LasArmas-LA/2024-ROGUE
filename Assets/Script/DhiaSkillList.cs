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

        //名前
        [CustomLabel("名前")]
        public string name;

        //威力
        [CustomLabel("威力")]
        public int power;

        //MP消費量(使うか不明)
        [CustomLabel("MP消費量")]
        public int useMp;

        //命中率
        [CustomLabel("命中率(%)")]
        public int hitRate;


        //分類
        public enum eAtkBuffType
        {
            _ATK,
            _BUFF,
            _DEBUFF
        }
        //実体
        [CustomLabel("分類")]
        public eAtkBuffType atkBuffType;

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

    [System.Serializable]
　　public class DefenseSkillStatus
    {
        //ID
        [CustomLabel("ID")]
        public string id;

        //名前
        [CustomLabel("名前")]
        public string name;

        //威力
        [CustomLabel("威力")]
        public int power;

        //MP消費量(使うか不明)
        [CustomLabel("MP消費量")]
        public int useMp;

        //命中率
        [CustomLabel("命中率(%)")]
        public int hitRate;


        //分類
        public enum eDefBuffType
        {
            _DEF,
            _BUFF,
            _DEBUFF
        }
        //実体
        [CustomLabel("分類")]
        public eDefBuffType defBuffType;

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
    public List<AtackSkillStatus>   atkSkillList = new List<AtackSkillStatus>();
    [SerializeField]
    public List<DefenseSkillStatus> defSkillList = new List<DefenseSkillStatus>();
}

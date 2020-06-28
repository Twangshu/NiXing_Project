﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerStatus : MonoBehaviour
{
    public static playerStatus instance;

    //血条与蓝条系统
    private int curBlood = 100;
    private int curBlue = 100;
    private int maxBlood = 100;
    private int maxBlue = 100;

    public Slider bloodSlider;
    public Slider blueSlider;

    /* 玩家行为所需信息
 * 攻击、技能等
 * 持续时间 *holdTime 
 * 冷却时间 *restTime
 * 状态 is* 表明是否处于某种状态
 */

    /* 持续时间指的是角色处于某种状态的时间长度，通常被设置为与相应动画的持续时间相等，以保证视觉上的一致性
     * 冷却时间指的是角色进行某个动作后，到能再次进行该动作的时间长度，通常要比持续时间长
     * 处于这两个时间时系统不响应输入指令
     */
    //普通攻击信息
    bool isAttack;
    private int attackPower = 10;
    public float attackRestTime = 0.4f;
    public float attackHoldTime = 0.4f;
    float lastAttackTime;

    //技能信息
    bool isSkill;
    public float skillRestTime = 0.4f;
    public float skillHoldTime = 0.4f;
    public float skillSpeed = 3f;
    private int skillBlue = 30;
    Vector3 skillDir;
    float lastSkillTime;

    //被攻击信息
    bool isAttacked; //硬直状态
    float lastAttackedTime;
    public float stiffTime = 0.4f;

    //冲刺信息
    bool isDash;
    float lastDashTime;
    public float dashRestTime = 1.0f;
    public float dashSpeed = 10.0f;
    public float dashHoldTime = 0.2f;

    //移动信息
    bool isMove;
    public float moveSpeed = 3.0f;
    Vector2 lastDir;
    //角色朝向
    public enum Face { up, down, left, right };
    Face face;

    bool isDead;

    //攻击力系统（装备）



    //受到攻击或治疗
    public void UpdateBlood(int change)
    {
        curBlood += change;
        if (curBlood <= 0)
        {
            curBlood = 0;
            IsDead = true;
        }
        else if (curBlood > maxBlood) curBlood = maxBlood;
        Debug.LogFormat("player blood update to: {0}", curBlood);

    }

    public void UpdateBlue(int change)
    {
        curBlue += change;
        if (curBlue <= 0) curBlue = 0;
        else if (curBlue > maxBlue) curBlue = maxBlue;
        Debug.LogFormat("player blood update to: {0}", curBlue);

    }




    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public float LastAttackTime { get => lastAttackTime; set => lastAttackTime = value; }
    public bool IsSkill { get => isSkill; set => isSkill = value; }
    public float LastSkillTime { get => lastSkillTime; set => lastSkillTime = value; }
    public bool IsAttacked { get => isAttacked; set => isAttacked = value; }
    public float LastAttackedTime { get => lastAttackedTime; set => lastAttackedTime = value; }
    public bool IsDash { get => isDash; set => isDash = value; }
    public float LastDashTime { get => lastDashTime; set => lastDashTime = value; }
    public bool IsMove { get => isMove; set => isMove = value; }
    public Vector2 LastDir { get => lastDir; set => lastDir = value; }
    public Face FaceTo { get => face; set => face = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public Vector3 SkillDir { get => skillDir; set => skillDir = value; }
    public int CurBlood { get => curBlood; }
    public int CurBlue { get => curBlue; }
    public int AttackPower { get => attackPower; set => attackPower = value; }
    public int SkillBlue { get => skillBlue; set => skillBlue = value; }

    public void CheckFace(Vector2 f)
    {
        if (f == Vector2.up) face = Face.up;
        else if (f == Vector2.down) face = Face.down;
        else if (f == Vector2.left) face = Face.left;
        else if (f == Vector2.right) face = Face.right;
        else { }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //bloodSlider.value = curBlood;
        //blueSlider.value = curBlue;

        LastAttackTime = 0.0f;
        LastAttackedTime = 0.0f;
        LastDashTime = 0.0f;
        SkillDir = new Vector3(0, 0, 0);
        LastDir = new Vector2(1, 0);
        IsSkill = false;
        IsAttacked = false;
        IsAttack = false;
        IsMove = false;
        IsDash = false;
        IsDead = false;
    }
    // Update is called once per frame
    void Update()
    {
        //bloodSlider.value = curBlood;
        //blueSlider.value = curBlue;
        //if (Input.GetKeyDown(KeyCode.Q))
        //{   //如果有剩余血药剂
        //    //if (InventoryMG.GetGoods(0)) { 
        //    upBlood();
        //    //}
        //    Debug.Log("您按下了Q键");
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{   //如果有剩余血药剂
        //    //if (InventoryMG.GetGoods(1)) { 
        //    upBlue();
        //    //}
        //    Debug.Log("您按下了E键");
        //}
    }
}
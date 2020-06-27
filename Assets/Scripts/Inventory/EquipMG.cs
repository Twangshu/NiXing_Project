﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipMG : MonoBehaviour
{

    static EquipMG instance;
    public Inventory EquipPanel;
    public GameObject EqIdel;
    public GameObject EquipSlot;

    public GameObject AttributeSlot;
    public GameObject AttributeGird;
    public int attributeCount = 5;

    private List<GameObject> equipments = new List<GameObject>();
    private List<GameObject> attributes = new List<GameObject>();

    //记录五种属性的加成值
    private int[] attributevalues = { 0, 0, 0, 0, 0 };

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void OnEnable()
    {
        initEquip();
        reflashEquipPanel();
        reflashAttributePanel();
    }

    private void initEquip()
    {
        for (int i = 0; i < instance.EqIdel.transform.childCount; i++)
        {
            equipments.Add(Instantiate(instance.EquipSlot, instance.EqIdel.transform.GetChild(i).transform));
        }
        for (int i = 0; i < attributeCount; i++)
        {
            attributes.Add(Instantiate(instance.AttributeSlot, instance.AttributeGird.transform));
            attributes[i].GetComponent<AttributeSlot>().attributeslotid = i;
            attributes[i].GetComponent<AttributeSlot>().initBasePanel();
        }
    }

    public static void reflashEquipPanel()
    {
        for (int i = 0; i < instance.EqIdel.transform.childCount; i++)
        {
            Destroy(instance.EqIdel.transform.GetChild(i).transform.GetChild(0).gameObject);
        }
        instance.equipments.Clear();
        for (int i = 0; i < instance.EquipPanel.itemlist.Count; i++)
        {
            instance.equipments.Add(Instantiate(instance.EquipSlot));
            instance.equipments[i].transform.SetParent(instance.EqIdel.transform.GetChild(i).transform);
            instance.equipments[i].transform.position = instance.EqIdel.transform.GetChild(i).transform.position;

            instance.equipments[i].GetComponent<EquipSlot>().Eqslotid = i;
            instance.equipments[i].GetComponent<EquipSlot>().setEqslot(instance.EquipPanel.itemlist[i]);
        }
        reflashAttributePanel();
    }

    public static void reflashAttributePanel()
    {
        for (int i = 0; i < instance.attributevalues.Length; i++)
        {
            instance.attributevalues[i] = 0;
        }
        for (int i = 0; i < instance.EquipPanel.itemlist.Count; i++)
        {
            if (instance.EquipPanel.itemlist[i] != null)
            {
                int tempattrid = EquipToAttribute(instance.EquipPanel.itemlist[i].itemid);
                int tempattrivalue = instance.EquipPanel.itemlist[i].itemattribute;
                instance.attributevalues[tempattrid] += tempattrivalue;
            }
        }
        for (int i = 0; i < instance.attributes.Count; i++)
        {
            if (instance.attributevalues[i] != 0)
            {
                instance.attributes[i].GetComponent<AttributeSlot>().setAttributeSlot(instance.attributevalues[i], 1);
            }
            else
            {
                instance.attributes[i].GetComponent<AttributeSlot>().setAttributeSlot(instance.attributevalues[i], 0);
            }
        }
    }


    /// <summary>
    /// 装备id映射到属性id上，比如 equipid==3 是剑， 映射到 attributeid为0的“攻击”属性上
    /// </summary>
    /// <param name="equipid"></param>
    /// <returns></returns>
    private static int EquipToAttribute(int equipid)
    {
        switch (equipid)
        {
            case 3:
                return 0;
            case 1:
                return 1;
            case 2:
                return 1;
            case 6:
                return 2;
            case 5:
                return 3;
            case 4:
                return 4;
            default:
                return -1;
        }
    }


    public static item GetEquipFromBag(item thisitem)
    {
        item saveitem = instance.EquipPanel.itemlist[thisitem.itemid - 1];
        if (thisitem != null)
        {
            instance.EquipPanel.itemlist[thisitem.itemid-1] = thisitem;
        }
        reflashEquipPanel();
        return saveitem;
    }


    public static void PutEquipToBag(int equipid, int thisitemid)
    {
        int tempattid = EquipToAttribute(thisitemid);
        instance.attributes[tempattid].GetComponent<AttributeSlot>().setAttributeSlot(0, 0);
        instance.EquipPanel.itemlist[equipid] = null;
        reflashEquipPanel();
    }
}
//3-01-TankManager  TotalChange 7
using System;
using UnityEngine;

[Serializable]
public class TankManager {
    public Color m_PlayerColor;            //声明一个公共Color结构m_PlayerColor;
    public Transform m_SpawnPoint;         //声明一个公共的Transform类对象m_SpawnPoint，储存坦克的出生位置;
    [HideInInspector] public int m_PlayerNumber;             //玩家编号，使用了该属性的变量不显示；        
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;          
    [HideInInspector] public int m_Wins;
    private GameObject m_CanvasGameObject;
    private TankMovement m_Movement; //（3-01），声明一个私有TankMovement脚本组件m_Movement; 


    //坦克的初始化设置
    public void Setup() {
        m_Movement = m_Instance.GetComponent<TankMovement>(); //（3-02），将坦克的TankMovement脚本组件返回给 m_Movement
        m_Movement.m_PlayerNumber = m_PlayerNumber; //（3-03），给脚本组件设置玩家编号
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;  //获取坦克的Canvas子对象；
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
        //设置玩家名称并着色；
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
        //创建MeshRenderer对象数组，获取坦克所有MeshRenderer子对象并返回给它；
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].material.color = m_PlayerColor;     //设置坦克的颜色；
        }
    }

    //重置坦克
    public void Reset() {
        m_Instance.transform.position = m_SpawnPoint.position;     //使坦克回到出生点；
        m_Instance.transform.rotation = m_SpawnPoint.rotation;     //重置坦克角度；
        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

    //禁用坦克控制
    public void DisableControl() {
        m_Movement.enabled = false;              //（3-04），停用m_Movement脚本；
        m_CanvasGameObject.SetActive(false);    //（3-05），停用坦克的UI显示；
    }

    //启用坦克控制
    public void EnableControl() {
        m_Movement.enabled = true;              //（3-06），启用m_Movement脚本；
        m_CanvasGameObject.SetActive(true);     //（3-07），启用坦克的UI显示；
    }
}

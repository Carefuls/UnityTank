//2-01-TankMovement
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1; //设定玩家变量，通过TankManager调用并传参         
    public float m_Speed = 12f;  //设定坦克移动速度           
    public float m_TurnSpeed = 180f;  //设定坦克旋转速度    
    private string m_MovementAxisName;     //玩家移动键位名称；
    private string m_TurnAxisName;         //玩家转向键位名称；
    private Rigidbody m_Rigidbody;         //声明一个私有 rigidbody 刚体组件 m_Rigidbody；
    private float m_MovementInputValue;    //玩家移动键位输入返回值；
    private float m_TurnInputValue;        //玩家转向键位输入返回值；

    //脚本加载的时候执行的代码
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();    //将游戏对象 Tank 附加的 rigidbody 刚体组件返回给 m_Rigidbody；
    }

    //当物体被激活的时候执行的代码 
    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;            //关闭刚体m_Rigidbody的isKinematic，即刚体m_Rigidbody将受物理的影响；
        m_MovementInputValue = 0f;                  //初始化m_MovementInputValue；
        m_TurnInputValue = 0f;                      //初始化m_TurnInputValue；
    }

    //当物体被停用的时候执行的代码 
    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;             //启用刚体m_Rigidbody的isKinematic，即刚体m_Rigidbody将不受物理的影响；
    }

    //场景加载好，开始的时候执行一次 （用来初始化数据），一定会在Update系列函数前面
    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;      //为不同玩家设置不同的键位；
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
    }

    // 每一帧执行一次
    private void Update()
    {
        // 存储玩家的键位（以此来匹配引擎声音）
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);          //获取玩家移动输入
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);   //获取玩家转向输入
    }

    //固定的刷新率 0.02s 执行一次，主要用来执行物理模拟的脚本
    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
    }

    //坦克前后移动函数，根据用户的输入来移动坦克
    private void Move()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        //创建一个 Vector3 类对象 movement =坦克的位置矢量*玩家移动键位输入返回值*设置的m_Speed*增量时间；
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        //使用Rigidbody.MovePosition()函数令坦克向(坦克的位置矢量+movement矢量)方向移动；
    }

    //坦克转向函数
    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        //创建一个 float 对象 turn = 玩家转向键位输入返回值 * 设置的转向速度m_TurnSpeed*增量时间；
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //创建一个Quaternion类对象(用于表示旋转的四元数)，接收一个旋转；
        //Quaternion.Euler (float x, float y, float z)，表示围绕 z 轴旋转 z 度、围绕 x 轴旋转 x 度、围绕 y 轴旋转 y 度；
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        //使用 Rigidbody.rotation 更改刚体的旋转会在下一个物理模拟步骤后更新变换，以令坦克转向；
    }
}
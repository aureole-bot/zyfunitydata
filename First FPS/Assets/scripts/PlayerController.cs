using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //只是专注移动不适用rigidboby组件
    //private Rigidbody rigidBody;
    private CharacterController controller;

    [Header("玩家位移")] [Tooltip("玩家移动速度")] public float moveSpeed;//
    private Vector3 movement;//接收键盘输入的位置移动
    private Vector3 dir;//玩家当前朝向
    
    [Header("玩家视角")]
    [Tooltip("水平灵敏度")] public float mouseSensitivityX = 500f;//水平灵敏度
    [Tooltip("竖直灵敏度")] public float mouseSensitivityY = 500f;//竖直灵敏度
    private float xRotation = 0f;  // 用于垂直旋转的累积值
    //public Transform cameraTransform; // 摄像机Transform
    private CinemachineVirtualCamera virtualCamera; // 虚拟摄像机引用
    
    [Header("重力以及跳跃设置")]
    [Tooltip("重力大小")] public float gravity = -20f;//-9.81f;
    private Vector3 velocity;//记录角色速度，这里变量记录了各个方向的速度，防止后面会使用,为了对应unity当中的设置y为竖直方向
    private bool isgrounded;//判断是否在地面
    [Tooltip("地面的图层")] public LayerMask groundLayer;//获取想要检测的图层，当前是想要检测地面层
    [Tooltip("跳跃的最大高度")] public float jumpHeight = 1f;
    
    [Header("角色奔跑或者下蹲")]
    private bool isruning = false;
    [Tooltip("奔跑速度")] public float runSpeed = 10f;
    [Tooltip("行走速度")] public float walkSpeed = 5f;
    
    public Animator animator;
    
    // Start is called before the first frame update
    private void Start()
    { 
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;//固定鼠标于屏幕中间
        Cursor.visible = false;//隐藏鼠标光标
        Transform cameraTransform = transform.Find("Virtual Camera"); //获取摄像机
        if (cameraTransform != null)  
        {  
            virtualCamera = cameraTransform.GetComponent<CinemachineVirtualCamera>();  
            // if (virtualCamera != null)  
            // {  
            //     Debug.Log("找到虚拟相机: " + virtualCamera.gameObject.name);  
            // }  
        }  
        else  
        {  
            Debug.LogError("未找到虚拟相机！");  
        }
        velocity = Vector3.zero;//初始化角色速度为零
        moveSpeed = walkSpeed;

        animator = transform.Find("Virtual Camera/gunstore/p2000").GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();
        PlayerRevolve();
        GravityAction();
        Run();
        fire();
        CuttingGun();
        Reload();
    }

    //角色移动
    private void PlayerMove()
    {
        //获得键盘上移动的输入
        float horizontal = Input.GetAxisRaw("Horizontal");//水平
        float vertical = Input.GetAxisRaw("Vertical");//竖直
        //这段代码是获取玩家坐标系，向玩家面朝方向移动，而不是全局坐标
        dir = transform.right * horizontal + transform.forward * vertical;
        dir.Normalize();//这行代码将dir向量归一化，使其长度变为1，确保无论输入值如何，移动方向的速度都是一致的。
        
        //Vector3 pos = this.transform.localPosition;
        //pos = pos + moveSpeed*dir*Time.fixedDeltaTime;
        //controller.Move(dir*moveSpeed*Time.deltaTime);//CharacterController组件中的Move方法参数是向量，为不是累加后的位置。
        
        // 计算水平移动
        Vector3 movement = moveSpeed * Time.deltaTime * dir;
        
        // // 处理重力
        // if (controller.isGrounded) {
        //     verticalVelocity = 0f;
        // } else {
        //     verticalVelocity += gravity * Time.deltaTime;
        // }
        // movement.y = verticalVelocity;
        
        controller.Move(movement);
        //用于标识，角色朝向和运动方向
        // Debug.DrawRay(transform.position, transform.forward * 2, Color.blue);
        // Debug.DrawRay(transform.position, dir * 2, Color.red);
    }
    //角色旋转
    private void PlayerRevolve()
    {
        //角色视角旋转
        // 鼠标输入（使用增量而非绝对位置）
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        // 垂直旋转（上下看）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // 限制抬头/低头角度
        
        // 应用垂直旋转到摄像机
        virtualCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // 水平旋转（左右转）作用于玩家
        transform.Rotate(Vector3.up * mouseX); // 绕世界坐标系Y轴旋转
    }
    //玩家重力设置，因为这里使用的是CharacterController组件，不是刚体组件，所以没有物理特性
    private void GravityAction()
    {
        // 始终应用垂直速度（统一通过Move处理）这里统一调用避免覆盖问题，这样避免第一下空格不跳
        controller.Move(velocity * Time.deltaTime);
        //判断角色是否在地面，用射线检测
        // float rayLength = 0.2f; // 适当增加检测范围
        // Vector3 rayStart = transform.position + Vector3.up * 1; // 从脚部稍上方发射
        //isgrounded = Physics.Raycast(transform.position, Vector3.down, 0.2f, groundLayer);
        isgrounded = controller.isGrounded;
        // 调试可视化（场景视图中显示红线）
        Debug.DrawRay(transform.position, Vector3.down*0.2f, Color.red);
        if (isgrounded)
        {
            //微微向下的力确保贴合地面
            // 保持轻微向下的力确保贴地
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
            //Debug.Log("在地面");
            if (Input.GetButtonDown("Jump"))
            {
                //Debug.Log("跳了一下");
                //通过想要跳跃的最大高度，反推应该具备的初速度
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            //速度的变化模仿正常重力作用
            //Debug.Log("不在地面");
            velocity.y += gravity * Time.deltaTime;
            // Vector3 movey = new Vector3(0f, velocity.y, 0f);
            // controller.Move(movey * Time.deltaTime);

        }
        controller.Move(velocity * Time.deltaTime);
    }
    //人物奔跑
    private void Run()
    {
        if (dir.x != 0||dir.y != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isruning = true;
                animator.SetBool("run", true);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isruning = false;
                animator.SetBool("run", false);
            }
        
            moveSpeed = isruning ? runSpeed : walkSpeed;
        }
        
    }
    //人物射击
    private void fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.CrossFade("Fire",0);
        }
    }
    //主副武器切换
    private void CuttingGun()
    {
        // // 获取鼠标滚轮的滚动输入  
        // float scroll = Input.GetAxis("Mouse ScrollWheel");  
        //
        // // 检查滚动方向  
        // if (scroll > 0f) // 鼠标向上滚动  
        // {  
        //     
        // }  
        // else if (scroll < 0f) // 鼠标向下滚动  
        // {  
        //     Debug.Log("鼠标向下滚动");  
        //     // 在这里添加您的逻辑，例如缩放物体等  
        // }  
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.transform.Find("Virtual Camera/gunstore/p2000").gameObject.SetActive(true);
            gameObject.transform.Find("Virtual Camera/gunstore/ak47").gameObject.SetActive(false);
            gameObject.transform.Find("Virtual Camera/gunstore/m4a4").gameObject.SetActive(false);
            animator = gameObject.transform.Find("Virtual Camera/gunstore/p2000").GetComponent<Animator>();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (GameManager.Instance.currgunitem != null)
            {
                gameObject.transform.Find("Virtual Camera/gunstore/p2000").gameObject.SetActive(false);
                gameObject.transform.Find("Virtual Camera/gunstore/ak47").gameObject.SetActive(false);
                gameObject.transform.Find("Virtual Camera/gunstore/m4a4").gameObject.SetActive(false);
                gameObject.transform.Find($"Virtual Camera/gunstore/{GameManager.Instance.currgunitem.name}")
                    .gameObject.SetActive(true);
                animator = gameObject.transform.Find($"Virtual Camera/gunstore/{GameManager.Instance.currgunitem.name}")
                    .GetComponent<Animator>();
            }
        }
    }
    //换弹
    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.CrossFade("ReloadNoAmmo", 0);
        }
    }
}

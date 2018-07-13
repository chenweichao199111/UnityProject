using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Controller1 : MonoBehaviour
{
    //状态：站立
    private const int HERO_IDLE = 0;
    //行走
    private const int HERO_WALK = 1;
    //跑
    private const int HERO_RUN = 2;
    //人物当前的状态
    private int gameState = 0;
    //记录触摸点的3D坐标
    private Vector3 point;

    private float time;

    void Start()
    {
        //初始设置人物状态：站立
        SetGameState(HERO_IDLE);
    }


    void Update()
    {
        //按下鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            //从摄像机的原点向鼠标点击的对象身上发射一条射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //当射线碰到对象时
            if (Physics.Raycast(ray, out hit))
            {
                //目前场景中只有地形
                //其实应当判断一下当前射线碰撞到的对象是否为地形。
                //得到在3D世界中点击的坐标
                point = hit.point;
                //设置主角面朝这个点击的坐标
                //这里设定的方向是鼠标选择的目标点在游戏世界点中的3D坐标。为了避免主人公的x
                //轴与Zzhou发生旋转（特殊情况）所以我们设定朝向的Y轴永远是主人公的y轴。
                //transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
                //用户是否连续点击按钮
                if (Time.realtimeSinceStartup - time <= 0.2f)
                {
                    //连续点击 进入奔跑状态
                    SetGameState(HERO_RUN);
                }
                else
                {
                    //点击一次只进入走路状态
                    SetGameState(HERO_WALK);
                }
                //记录点击鼠标的时间
                time = Time.realtimeSinceStartup;
            }
        }
    }

    void FixedUpdate()
    {
        switch (gameState)
        {
            case HERO_IDLE:
                break;
            case HERO_WALK:
                //移动主角一次移动的长度为0.05
                Move(10f);
                break;
            case HERO_RUN:
                //跑步是移动长度为0.1f
                Move(10f);
                break;

        }
    }

    void SetGameState(int state)
    {
        //动画效果
        switch (state)
        {

            case HERO_IDLE:
                point = transform.position;
                //animation.Play("idle");
                break;
            case HERO_WALK:
                //animation.Play("walk");
                break;
            case HERO_RUN:
                //animation.Play("run");
                break;

        }
        gameState = state;
    }

    void Move(float speed)
    {
        //主角没到达目标点时，一直向该点移动
        //在这里判断主人公当前位置是否到达目标位置，然后取得两点坐标差的绝对值。未到达目的

        if (Mathf.Abs(Vector3.Distance(point, transform.position)) > 0.001f)
        {
            //得到角色控制器
            ThirdPersonCharacter controller = GetComponent<ThirdPersonCharacter>();
            //设置移动
            //第一个参数：两个坐标点的差，参数2：需要移动的距离
            //Vector3 v = Vector3.ClampMagnitude(point - transform.position, speed);
            Vector3 v = point - transform.position;
            //控制移动
            controller.Move(v, false, false);
        }

        else
        {
            //到达目的地，继续站立状态
            SetGameState(HERO_IDLE);
        }
    }

}
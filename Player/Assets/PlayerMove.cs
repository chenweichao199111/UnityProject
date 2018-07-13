using UnityEngine;
using System.Collections;

/*
 *物体移动到鼠标点击位置
*/

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody mRigidbody;
    private PlayerDir dir;

    void Awake()
    {
        dir = this.GetComponent<PlayerDir>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //获取当前物体到目标物体的距离
        float distance = Vector3.Distance(dir.targetPosition, transform.position);

        //判断是否超出距离, velocity表示速度
        if (distance > 0.1f)
        {
            mRigidbody.velocity = transform.forward * speed * distance;
        }
        else
        {
            mRigidbody.velocity = transform.forward * 0;
        }
    }
}
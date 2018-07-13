using UnityEngine;

/*
 *物体移动到鼠标点击位置
*/

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
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

        //判断是否超出距离
        if (distance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, dir.targetPosition, Time.deltaTime * speed);
        }
        else
        {
            transform.position = dir.targetPosition;
        }
    }
}
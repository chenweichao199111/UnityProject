using UnityEngine;
using System.Collections;

public class FollowMouseMove : MonoBehaviour
{
    private bool isHover;
    protected Vector3 targetPosition;
    public float mSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 当左键按下;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit tempHit;
            // 发出射线
            if (Physics.Raycast(ray, out tempHit))
            {
                if (tempHit.collider != null && tempHit.collider.CompareTag("Ground"))
                {
                    isHover = true;
                    LookAtTarget(tempHit.point);
                }
            }
        }

        MoveTo(targetPosition);
    }

    void LookAtTarget(Vector3 hitPoint)
    {
        //获取触发目标物体的位置信息
        targetPosition = hitPoint;
        //将目标位置的y轴修改为当前物体的y轴
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

        //当前物体朝向目标位置
        this.transform.LookAt(targetPosition);
    }

    private void MoveTo(Vector3 tar)
    {
        if (isHover)
        {
            transform.position = Vector3.MoveTowards(transform.position, tar, Time.deltaTime * mSpeed);
            if (transform.position == tar)
            {
                isHover = false;
            }
        }
    }
}

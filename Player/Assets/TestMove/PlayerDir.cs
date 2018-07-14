using System.Collections;
using UnityEngine;

/*
 * 根据鼠标点击位置改变物体朝向
*/
public class PlayerDir : MonoBehaviour
{
    public GameObject effect;
    public Vector3 targetPosition;

    void Awake()
    {
        //初始化目标位置为当前物体的位置
        targetPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //判断鼠标左键是否按下
        if (Input.GetMouseButtonDown(0))
        {
            //获取由主摄像机位置到鼠标点击位置的一条射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            //判断射线是否成功发射且是否触发目标物体
            if (isCollider && hitInfo.collider.CompareTag("Ground"))
            {
                //参数为目标物体的位置信息
                ShowClickEffect(hitInfo.point);
                LookAtTarget(hitInfo.point);
            }
        }
    }

    void ShowClickEffect(Vector3 hitPoint)
    {
        if (effect != null)
        {
            hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.3f, hitPoint.z);
            GameObject.Instantiate(effect, hitPoint, Quaternion.identity);
        }
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
}
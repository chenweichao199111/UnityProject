using UnityEngine;

public class LerpFollowMove : MonoBehaviour {

    Vector3 targetPos;
    float y;

    private void Start()
    {
        y = transform.position.y;
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
                targetPos = hitInfo.point;
                targetPos.y = y;
            }
        }
    }

    private void LateUpdate()
    {
        if (targetPos != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos), 0.3f);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 5f);
        }
    }
}

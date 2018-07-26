
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public Path path;
    public float speed = 20.0f;
    public bool isLooping = true;
    private float curSpeed;
    private int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;

    private void Start()
    {
        pathLength = path.Length;
        curPathIndex = 0;
    }

    private void Update()
    {

        curSpeed = speed * Time.deltaTime;

        targetPoint = path.GetPoint(curPathIndex);
        if (Vector3.Distance(transform.position, targetPoint) < 0.01f)
        {
            if (curPathIndex < pathLength - 1)
            {
                curPathIndex++;
            }
            else if (isLooping)
            {
                curPathIndex = 0;
                path.ReversalPath();
            }
            else
            {
                return;
            }
        }
        if (curPathIndex >= pathLength)
        {
            return;
        }

        LookAtTarget(targetPoint);
        MoveTo(targetPoint);
    }

    protected void LookAtTarget(Vector3 varTargetPos)
    {
        Vector3 temp = varTargetPos;
        temp.y = transform.position.y;

        //当前物体朝向目标位置
        transform.LookAt(temp);
    }

    private void MoveTo(Vector3 tar)
    {
        transform.position = Vector3.MoveTowards(transform.position, tar, Time.deltaTime * speed);
    }

}

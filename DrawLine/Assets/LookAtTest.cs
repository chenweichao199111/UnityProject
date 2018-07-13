using UnityEngine;

public class LookAtTest : MonoBehaviour
{

    public Transform other;//Sphere游戏对象

    void Start()
    {
        //Cube的z轴指向（1，1，1）点
        //        transform.LookAt(new Vector3(1, 1, 1));
    }

    void Update()
    {
        //画线调试，由Cube的postion指向Sphere的postion
        Debug.DrawLine(transform.position, other.position, Color.cyan);
        //Cube的z轴指向Sphere，（指向了Sphere游戏对象的Transform组件的position值，也是一个Vector3类型的值） 
        transform.LookAt(other);
    }
}

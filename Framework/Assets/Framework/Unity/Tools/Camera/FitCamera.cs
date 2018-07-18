using UnityEngine;

namespace Framework.Unity.Tools
{
    public class FitCamera : MonoBehaviour
    {
        const float devHeight = 9.6f; // 设计的尺寸高度
        const float devWidth = 6.4f;  // 设计的尺寸宽度

        void Start()
        {
            float screenHeight = Screen.height; // 获取屏幕高度

            float orthographicSize = GetComponent<Camera>().orthographicSize; // 拿到相机的正交属性设置摄像机大小
            float aspectRatio = Screen.width * 1.0f / Screen.height; // 得到宽高比

            float cameraWidth = orthographicSize * 2 * aspectRatio;
            //
            if (cameraWidth < devWidth)
            {
                orthographicSize = devWidth / (2 * aspectRatio);  // 相机的大小等于尺寸宽度/2倍的宽高比
                GetComponent<Camera>().orthographicSize = orthographicSize;
            }

        }
    }
}

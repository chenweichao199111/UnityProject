using UnityEngine;

namespace Framework.Unity.Tools
{
    public static class CameraExtension
    {
        /// <summary>
        /// 判断世界坐标系内的物体,摄像机是否可见
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsVisibleOn3dCamera(this GameObject obj, Camera camera3d)
        {
            Vector3 pos = camera3d.WorldToViewportPoint(obj.transform.position);
            // Determine the visibility and the target alpha
            bool isVisible = (camera3d.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
            return isVisible;
        }

        /// <summary>
        ///判断世界坐标系内的一个点,摄像机是否可见
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsVisibleOn3dCamera(this Vector3 pos, Camera camera3d)
        {
            pos = camera3d.WorldToViewportPoint(pos);
            bool isVisible = (camera3d.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
            return isVisible;
        }

        public static Vector3 Get3dCameraForward(Camera camera3d)
        {
            return (camera3d.transform.rotation * Vector3.forward).normalized;
        }
    }
}
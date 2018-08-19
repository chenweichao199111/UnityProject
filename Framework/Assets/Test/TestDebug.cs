using UnityEngine;

public class TestDebug : MonoBehaviour
{
    #region Fields

    #endregion

    #region Properties

    #endregion

    #region Unity Messages
    //    void Awake()
    //    {
    //
    //    }
    //    void OnEnable()
    //    {
    //
    //    }
    //
    void Start()
    {
        Debug.Log("1234567890");
        Debuger.LogFormat("123{0}", 4);
    }
    //    
    //    void Update() 
    //    {
    //    
    //    }
    //
    //    void OnDisable()
    //    {
    //
    //    }
    //
    //    void OnDestroy()
    //    {
    //
    //    }

    #endregion

    #region Private Methods
    [ContextMenu("测试输出")]
    private void Test()
    {
        Start();
    }
    #endregion

    #region Protected & Public Methods

    #endregion
}
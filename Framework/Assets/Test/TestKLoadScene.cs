using Framework.Unity.Tools;
using UnityEngine;

public class TestKLoadScene : MonoBehaviour
{
    public string mSceneName = "2";

    public void ClickLoad()
    {
        SceneListener.Instance.LoadScene(mSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single, LoadFinish);
    }

    private void LoadFinish()
    {
        Debuger.Log("");
    }
}

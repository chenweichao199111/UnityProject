namespace Framework.Unity.Tools
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneChanger : MonoBehaviour
    {

        private void Update()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                nextSceneIndex++;
                if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                    nextSceneIndex = 0;
            }
            else if (Input.GetKeyUp(KeyCode.Backspace))
            {
                nextSceneIndex--;
                if (nextSceneIndex < 0)
                    nextSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
            }

            if (nextSceneIndex == currentSceneIndex)
            {
                return;
            }

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
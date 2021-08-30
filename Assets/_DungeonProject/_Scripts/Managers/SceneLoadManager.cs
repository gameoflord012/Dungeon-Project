using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField]
    private int[] sceneBuildIndexes;

    private void Start()
    {
        foreach (int buildIndex in sceneBuildIndexes)
        {
            if (SceneManager.GetSceneByBuildIndex(buildIndex).IsValid()) return;
            SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

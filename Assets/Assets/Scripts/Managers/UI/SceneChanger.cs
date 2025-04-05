using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void ChangeScene()
    {
        AudioManager.Instance.PlayMusic(true);
        SceneManager.LoadScene(sceneName);
    }
}

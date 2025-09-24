using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    // 单例实例
    public static sceneLoader Instance;

    private void Awake()
    {
        // 确保全局唯一
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 加载场景
    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName) != null)
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"加载场景：{sceneName}");
        }
        else
        {
            Debug.LogError($"场景 {sceneName} 不存在，请检查名称或添加到Build Settings！");
        }
    }
}
    
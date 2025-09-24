using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalEsc : MonoBehaviour
{
    private void Awake()
    {
        // 使该对象在场景切换时不被销毁，保证全局都能响应ESC键
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // 检测ESC键是否被按下
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapePressed();
        }
    }

    /// ESC键按下
    private void OnEscapePressed()
    {
        // 在编辑器中运行时输出日志，不执行退出
        #if UNITY_EDITOR
                Debug.Log("ESC键键被按下 - 编辑器模式下不执行退出操作");
        #else
                // 退出应用程序
                Debug.Log("ESC键被按下,退出应用");
                Application.Quit();
        #endif
    }
}

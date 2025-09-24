using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class followMouse : MonoBehaviour
{
    [Header("移动设置")]
    [Tooltip("移动速度")]
    public float moveSpeed = 8f;
    [Tooltip("到达目标点的误差范围")]
    public float stoppingDistance = 0.1f;
    [Tooltip("是否忽略UI点击")]
    public bool ignoreUIClicks = true;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving;
    private Camera mainCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // 配置Rigidbody2D确保移动正常
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        
        // 获取主相机引用
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("场景中没有标记为MainCamera的相机！");
            enabled = false;
        }
        
        // 初始化目标位置
        targetPosition = transform.position;
    }

    private void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 忽略UI点击（如果启用）
            if (ignoreUIClicks && IsPointerOverUI())
                return;

            // 计算点击的世界坐标
            Vector3 clickPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // 确保Z轴与玩家一致（2D游戏）
            targetPosition = new Vector2(clickPosition.x, clickPosition.y);
            isMoving = true;
            
            Debug.Log($"设置新目标位置: {targetPosition}");
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            // 计算到目标的方向和距离
            Vector2 currentPosition = transform.position;
            Vector2 direction = (targetPosition - currentPosition).normalized;
            float distance = Vector2.Distance(currentPosition, targetPosition);

            // 检查是否到达目标
            if (distance <= stoppingDistance)
            {
                rb.velocity = Vector2.zero;
                isMoving = false;
                Debug.Log("到达目标位置");
            }
            else
            {
                // 移动到目标
                rb.velocity = direction * moveSpeed;
            }
        }
        else
        {
            // 停止移动
            rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 检查鼠标是否在UI元素上
    /// </summary>
    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;
            
        return EventSystem.current.IsPointerOverGameObject();
    }

    // 绘制调试辅助线
    /*    private void OnDrawGizmos()
    {
        if (isMoving)
        {
            // 绘制到目标的路径
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, targetPosition);
            
            // 绘制目标位置标记
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetPosition, stoppingDistance);
        }
    }
    */
}

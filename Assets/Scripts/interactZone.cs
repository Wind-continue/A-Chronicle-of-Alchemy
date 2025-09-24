using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactZone : MonoBehaviour
{
    [Header("交互设置")]
    [Tooltip("触发交互的距离")]
    public float interactDistance = 20f;
    [Tooltip("交互按钮UI")]
    public GameObject interactButtonUI; // 仅保留交互按钮

    private GameObject player;
    private bool isInRange = false;
    private static interactZone activeZone = null;


    private void Start()
    {
        // 找到玩家（标签为"Player"）
        player = GameObject.FindGameObjectWithTag("Player");

        // 确保交互按钮初始隐藏
        if (interactButtonUI != null)
            interactButtonUI.SetActive(false);

        // 确保碰撞体为触发器（不阻碍玩家移动）
        Collider2D col = GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
        {
            col.isTrigger = true;
            Debug.LogWarning("交互区域碰撞体已自动设为触发器，避免阻碍玩家移动", this);
        }
    }
    private void Update()
    {
        if (player == null || interactButtonUI == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        isInRange = distance <= interactDistance;

        // 逻辑：若当前区域在范围内，且没有其他激活的区域，才显示按钮
        if (isInRange && activeZone == null)
        {
            interactButtonUI.SetActive(true);
            activeZone = this; // 标记当前区域为激活状态
        }
        // 若玩家离开当前区域，取消激活，允许其他区域显示
        else if (!isInRange && activeZone == this)
        {
            interactButtonUI.SetActive(false);
            activeZone = null; // 清空激活标记
        }
    }

    // 交互按钮点击事件（绑定到UI按钮）
    public void OnInteract()
    {
        if (isInRange)
        {
            Debug.Log("交互功能：暂未实现");
            // 此处可添加实际交互逻辑（如对话、拾取等）
        }
    }

    // 绘制Gizmos辅助线（显示交互范围）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
}

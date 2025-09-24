using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class approachHighLight : MonoBehaviour
{
    [Header("发光设置")]
    [Tooltip("发光强度")]
    public float glowIntensity = 100f;
    [Tooltip("触发距离")]
    public float triggerDistance = 20f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color glowColor;
    private bool isGlowing = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        glowColor = originalColor * glowIntensity; // 发光强度修改
    }

    private void Update()
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        // 找到玩家
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= triggerDistance && !isGlowing)
        {
            StartGlow();
        }
        else if (distance > triggerDistance && isGlowing)
        {
            StopGlow();
        }
    }

    private void StartGlow()
    {
        isGlowing = true;
        spriteRenderer.color = glowColor;
    }

    private void StopGlow()
    {
        isGlowing = false;
        spriteRenderer.color = originalColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 选择关卡界面中人物简单的控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameApp.CameraManager.SetPos(transform.position);   //摄像机跟随玩家
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0)
        {
            animator.Play("idle");
        }
        else
        {
            if ((h > 0 && transform.localScale.x < 0) || (h < 0 && transform.localScale.x > 0))
            {
                Flip();
            }

            Vector3 pos = transform.position + Vector3.right * h * moveSpeed * Time.deltaTime;
            //这里是不要跑出边界，空气墙
            pos.x = Mathf.Clamp(pos.x, -32, 24);
            transform.position = pos;
            GameApp.CameraManager.SetPos(transform.position);   //摄像机跟随玩家
            animator.Play("move");
        }
    }

    //转向
    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

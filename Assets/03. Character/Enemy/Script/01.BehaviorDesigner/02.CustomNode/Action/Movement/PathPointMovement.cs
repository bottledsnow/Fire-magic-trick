using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PathPointMovement : Action
{
    public SharedGameObject targetObject;
    public float moveSpeed = 5f; // 最大速度
    private Rigidbody rb; // 敌人的刚体组件
    public Vector3 targetPosition; // 当前目标位置
    public float maxOffset = 1;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>(); // 获取敌人的刚体组件
        GenerateNewPathPoint(); // 生成初
    }

    public override TaskStatus OnUpdate()
    {
        Movement();
        _LookAtTarget();
        // 检查是否接近当前路径点
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 生成新的路径点
            GenerateNewPathPoint();
        }
        return TaskStatus.Success;
    }

    private void GenerateNewPathPoint()
    {
        // 生成新的路径点，包括随机偏移
        Vector3 playerPosition = GameObject.Find("Player").transform.position; // 假设玩家对象命名为"Player"
        Vector3 offset = new Vector3(
            Random.Range(-maxOffset, maxOffset),
            0f,
            Random.Range(-maxOffset, maxOffset)
        );
        targetPosition = playerPosition + offset;
    }

    private void Movement()
    {
        // 计算施加的力
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        Vector3 force = moveDirection * moveSpeed * Time.deltaTime;

        // 对刚体施加力来移动
        rb.AddForce(force, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed * Time.deltaTime)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed * Time.deltaTime;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void _LookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
}
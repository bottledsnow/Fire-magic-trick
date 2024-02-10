using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationController : MonoBehaviour
{
    private Gamepad gamepad;

    private void Start()
    {
        // 获取第一个连接的手柄
        gamepad = Gamepad.current;
    }

    private void Update()
    {
        test();
    }
    private void test()
    {
        // 示例：按下空格键触发手柄震动
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // 震动的强度（0.0f到1.0f）
            float intensity = 0.5f;

            // 震动的持续时间（秒）
            float duration = 0.25f;

            // 调用震动函数
            Vibrate(intensity, duration);
        }
    }

    public void Vibrate(float intensity, float duration)
    {
        if (gamepad != null)
        {
            // 使用Gamepad类的震动函数
            gamepad.SetMotorSpeeds(intensity, intensity);

            // 在指定的持续时间后停止震动
            Invoke("StopVibration", duration);
        }
    }

    private void StopVibration()
    {
        if (gamepad != null)
        {
            // 停止震动
            gamepad.SetMotorSpeeds(0, 0);
        }
    }
}
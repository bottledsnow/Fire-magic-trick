using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    [SerializeField] private float stopTime;

    private float timer = 0;
    private bool Trigger;
    private bool isTimerFinish;
    private bool isTimer;
    private bool isMove = false;
    private bool isTimerStop;
    private int moveIndex = 0;

    private void Update()
    {
        MoveSystem();
        stopTimerSystem();
    }
    private void MoveSystem()
    {
        MoveToPoint(points[moveIndex]);
    }
    private void MoveToPoint(Transform point)
    {
        Vector3 TargetPoint = point.position;
        Vector3 Direction = (TargetPoint - this.transform.position).normalized;

        isMove = MoveCheck(point);

        if (isMove)
        {
            this.transform.position += Direction * speed * Time.deltaTime;
            SetTrigger(false);
        }

        if(!isMove)
        {
            if(!Trigger)
            {
                SetTrigger(true);
                SetIsTimer(true);
            }

            if(!isMove && isTimerFinish)
            {
                MoveIndexAdd();
                SetIsTimerFinish(false);
            }
        }
    }
    private bool MoveCheck(Transform point)
    {
        Vector3 length = point.position - this.transform.position;

        if (length.magnitude < 0.25f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void stopTimerSystem()
    {
        if(isTimer)
        {
            timer += Time.deltaTime;
        }

        if(timer > stopTime)
        {
            timer = 0;
            SetIsTimer(false);
        }
    }
    private void MoveIndexAdd()
    {
        moveIndex++;

        if (moveIndex >= points.Length)
        {
            SetMoveIndex(0);
        }
    }
    private void SetMoveIndex(int value)
    {
        moveIndex = value;
    }
    private void SetIsTimer(bool active)
    {
        isTimer = active;
    }
    private void SetIsTimerFinish(bool active)
    {
        isTimerFinish = active;
    }
    private void SetTrigger(bool active)
    {
        Trigger = active;
    }
    private void SetIsTimerStop(bool active)
    {
        isTimerStop = active;
    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(points[i].position, 0.5f);
        }
    }

}

using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    [SerializeField] private float stopTime;

    private float timer = 0;
    private bool Trigger;
    private bool triggerTimer;
    private bool isTimer;
    private bool isMove = false;
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

        if(!isMove && !Trigger)
        {
            SetTrigger(true);
            MoveIndexAdd();
            Debug.Log("Stop and Add");
        }

        /*
        if (!isMove && !triggerTimer)
        {
            if(timer == 0)
            {
                Debug.Log("Stop and Add");
                MoveIndexAdd();
                Initialization();
            }else
            {
                SetIsTimer(true);
                SetIsTriggerTimer(true);
            }
        }
        */
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
            Initialization();
        }

        if(!isTimer && triggerTimer)
        {
            MoveIndexAdd();
        }
    }
    private void MoveIndexAdd()
    {
        int moveIndexNew = moveIndex++;
        SetMoveIndex(moveIndexNew);

        if (moveIndex >= points.Length)
        {
            SetMoveIndex(0);
            Debug.Log("000");
        }
    }
    private void Initialization()
    {
        timer = 0;
        SetIsTimer(false);
        SetIsTriggerTimer(false);
    }
    private void SetMoveIndex(int value)
    {
        moveIndex = value;
    }
    private void SetIsTimer(bool active)
    {
        isTimer = active;
    }
    private void SetIsTriggerTimer(bool active)
    {
        triggerTimer = active;
    }
    private void SetTrigger(bool active)
    {
        Trigger = active;
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

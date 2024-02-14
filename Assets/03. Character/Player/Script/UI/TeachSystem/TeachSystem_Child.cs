using UnityEngine;

public class TeachSystem_Child : MonoBehaviour
{
    public bool canTeachMutipleTimes = false;
    //Script
    private TeachSystem teachSystem;

    //variable
    public int index = 0;
    private bool isTrigger = false;

    private void Start()
    {
        teachSystem = GameManager.singleton.UISystem.GetComponent<TeachSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!isTrigger)
            {
                teachSystem.OpenTeach(index);

                if (!canTeachMutipleTimes) isTrigger = true;
            }
        }
    }
    public void TriggerThisTeach()
    {
        if (!isTrigger)
        {
            teachSystem.OpenTeach(index);
            isTrigger = true;
        }
    }
}

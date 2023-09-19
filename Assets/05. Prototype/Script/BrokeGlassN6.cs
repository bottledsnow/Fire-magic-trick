using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class BrokeGlassN6 : MonoBehaviour
{
    [SerializeField] private Rigidbody[] glasses;
    [SerializeField] private float force = 100f;
    [SerializeField] private MMF_Player Feedbacks;
    [SerializeField] private float Delay=0.5f;
    private bool isTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isTrigger)
            {
                Feedbacks.PlayFeedbacks();
                DelayBroken();
                isTrigger = true;
            }
        }
    }
    private async void DelayBroken()
    {
        await Task.Delay((int)(Delay*1000));
        Broke();
    }
    private void Start()
    {
        GetAllChild();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Broke();
        }
    }
    private void GetAllChild()
    {
        glasses = new Rigidbody[transform.childCount];
        Transform parentTransform = transform;

        int childCount = parentTransform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childGameObject = childTransform.gameObject;
            glasses[i] = childGameObject.GetComponent<Rigidbody>();
        }
    }
    public void Broke()
    {
        Debug.Log("Broke");
        for (int i = 0; i < glasses.Length; i++)
        {
            if (glasses[i] != null)
            {
                glasses[i].useGravity = true;
                glasses[i].isKinematic = false;
                glasses[i].AddExplosionForce(force, transform.position, 250f);
            }
        }
    }
}

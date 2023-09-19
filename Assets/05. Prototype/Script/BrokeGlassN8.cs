using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class BrokeGlassN8 : MonoBehaviour
{
    [SerializeField] private Rigidbody[] glasses;
    [SerializeField] private float force = 100f;
    
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
                glasses[i].AddExplosionForce(force, transform.position, 2500f);
            }
        }
    }
}

using UnityEngine;

public class CameraIgnore : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask havePlayer;
    [SerializeField] private LayerMask noPlayer;

    private void Awake()
    {
        _camera= Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _camera.cullingMask = noPlayer;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _camera.cullingMask = havePlayer;
        }
    }
}

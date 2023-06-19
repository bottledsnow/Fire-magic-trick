using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostInput : MonoBehaviour
{
    [SerializeField] private Transform Ghost;
    [SerializeField] private Transform ghostPoint;

    private ghost ghostScipt;
    private Transform player;

    private bool useGhost;
    private void Awake()
    {
        ghostScipt = Ghost.GetComponent<ghost>();
        player = this.transform;
    }
    private void Update()
    {
        TouseGhost();
    }

    private void TouseGhost()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 playerforward = player.transform.forward;

            ghostScipt.Ghostforward = playerforward;
            useGhost = true;
            Ghost.transform.position = ghostPoint.transform.position;
            Ghost.transform.rotation = Quaternion.LookRotation(playerforward);
            ghostScipt.transform.rotation = Quaternion.Euler(-90, ghostScipt.transform.rotation.eulerAngles.y, ghostScipt.transform.rotation.eulerAngles.z);
            playerforward = player.gameObject.transform.forward;
            ghostScipt.enabled = true;
        }
    }
}

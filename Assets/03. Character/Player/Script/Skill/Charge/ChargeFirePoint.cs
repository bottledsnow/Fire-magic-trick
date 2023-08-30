using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeFirePoint : MonoBehaviour
{
    private void Update()
    {
        transform.position -= transform.up * Time.deltaTime * 0.1f;
    }
}

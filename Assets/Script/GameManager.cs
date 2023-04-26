using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public PlayerInputManager PlayerInput;
    private void Awake()
    {
        GM = this;
    }
}

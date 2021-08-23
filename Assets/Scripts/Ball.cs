using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

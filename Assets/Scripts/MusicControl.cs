using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    private static GameObject musicInstance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(musicInstance == null)
        {
            musicInstance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

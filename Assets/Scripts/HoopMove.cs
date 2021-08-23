using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMove : MonoBehaviour
{
    [SerializeField] private Vector3 worldPointPos;
    [SerializeField] private Vector3 ViewPoint;
    [SerializeField] private float hoopSpeed;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Quaternion[] weaponsQuaternion;
    [SerializeField] private AudioSource song;
    private Camera mainCamera;
    private bool isMovingDown;
    
    public void Start()
    {
        mainCamera = Camera.main;
        isMovingDown = true;
        
    }

    void FixedUpdate()
    {
        worldPointPos = transform.position;
        ViewPoint = mainCamera.WorldToViewportPoint(worldPointPos);
        if(ViewPoint.y >= 0.1f && isMovingDown)
        {
            transform.Translate(Vector2.down * hoopSpeed * GameEnvironment.singleton.GetCurrentLevel());
            if(ViewPoint.y <= 0.2f)
            {
                isMovingDown = false;
            }
        }
        else
        {
            transform.Translate(Vector2.up * hoopSpeed * GameEnvironment.singleton.GetCurrentLevel());
            if (ViewPoint.y >= 0.9f)
            {
                isMovingDown = true;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            song.Play();
            collision.transform.tag = "Untagged";
            int randomWeapon = Random.Range(0, weapons.Length);
            GameObject instance = Instantiate(weapons[randomWeapon], 
                new Vector3(weapons[randomWeapon].transform.position.x + Random.Range(-100, 100), weapons[randomWeapon].transform.position.y,
                weapons[randomWeapon].transform.position.z),weaponsQuaternion[randomWeapon]);
            
        }
    }
}

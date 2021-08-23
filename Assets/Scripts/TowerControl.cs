using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerControl : MonoBehaviour
{
    
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameControl gameControl;
    [SerializeField] private float healthCoefficient;
    [SerializeField] private AudioSource song;
    void Start()
    {
        healthSlider.maxValue = GameEnvironment.singleton.GetCurrentLevel() * healthCoefficient;

        if (GameEnvironment.singleton.GetCurrentLevel() >= 5)
        {
            healthSlider.maxValue *= 2;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            song.Play();
            collision.gameObject.tag = "Untagged";
            healthSlider.value += Random.Range(10,15);
            if(healthSlider.value >= healthSlider.maxValue)
            {
                Time.timeScale = 0;
                gameControl.WinPanel();
            }
        }
    }


    void Update()
    {
        
    }
}

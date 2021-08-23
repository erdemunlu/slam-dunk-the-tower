using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class HomeControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highestLevelText;
    [SerializeField] private AudioSource song;
    [SerializeField] private GameObject creditPanel;
    public void Start()
    {
        Time.timeScale = 1;
        highestLevelText.text = GameEnvironment.singleton.GetHighestLevel().ToString();
    }
    public void playButton()
    {
        song.Play();
        SceneManager.LoadScene("PlayScene");
    } 

    public void OpenCreditPanel()
    {
        song.Play();
        creditPanel.SetActive(true);
    }
    public void CloseCreditPanel()
    {
        song.Play();
        creditPanel.SetActive(false);
    }
}

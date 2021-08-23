using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivotRigidbody;
    [SerializeField] private Transform TouchZone;
    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSpringJoint;
    private bool isDragging;
    private GameObject instance;
    private Vector3 centerPoint;
    [SerializeField] private float radius;
    [SerializeField] private float disjointDelay;
    [SerializeField] private float respownBallDelay;
    [SerializeField] private float ballSpeed;
    [SerializeField] private float timeDurationCoefficient;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject[] panels;
    [SerializeField] private AudioSource[] songs;
    private float timeDuration;
    private bool playing;
    private void Start()
    {
        Time.timeScale = 1;
        playing = true;
        mainCamera = Camera.main;
        isDragging = false;
        centerPoint = TouchZone.position;
        levelText.text = $"Level {GameEnvironment.singleton.GetCurrentLevel()}";
        if (GameEnvironment.singleton.GetCurrentLevel() >= 5)
        {
            timeDuration = GameEnvironment.singleton.GetCurrentLevel() * timeDurationCoefficient/1.2f;
        }
        else
        {
            timeDuration = GameEnvironment.singleton.GetCurrentLevel() * timeDurationCoefficient;
        }
        RespownBall(); 
    }
    void Update()
    {
        if(timeDuration <= 1 && playing)
        {
            Time.timeScale = 0;
            GameOverPanel();
            playing = false;
        }
        if (timeDuration <= 1) { return; }
        int minute = Mathf.FloorToInt(timeDuration / 60);
        int second = Mathf.FloorToInt(timeDuration % 60);
        timeDuration -= Time.deltaTime;
        timeText.text = string.Format("{00:00}:{01:00}", minute, second);
        

        if (!Touchscreen.current.primaryTouch.press.isPressed && isDragging == false) { return; }

        if (!Touchscreen.current.primaryTouch.press.isPressed && isDragging)
        {
            if (isDragging)
            {
                BallLaunch();
                isDragging = false;
                return;
            }
            return;
        }

        if(currentBallRigidbody == null) { return; }

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            
            Vector3 pixelPoint = Touchscreen.current.primaryTouch.position.ReadValue();
            pixelPoint.z = 22.4f;
            Vector3 worldPoint = mainCamera.ScreenToWorldPoint(pixelPoint);
            Vector3 offset = worldPoint - centerPoint;
            currentBallRigidbody.position = centerPoint + Vector3.ClampMagnitude(offset, radius);

            isDragging = true;
        }
        

    }
    

    public void BallLaunch()
    {
        songs[0].Play();
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;
        instance.GetComponent<Rigidbody2D>().velocity = Vector2.right * ballSpeed;
        Invoke(nameof(Disjoint), disjointDelay);
    }
    public void Disjoint()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
        Invoke(nameof(RespownBall), respownBallDelay);

    }

    public void RespownBall()
    {
        instance = Instantiate(ballPrefab, pivotRigidbody.position, Quaternion.identity);
        instance.transform.SetParent(TouchZone.transform);
        currentBallRigidbody = instance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = instance.GetComponent<SpringJoint2D>();
        currentBallSpringJoint.connectedBody = pivotRigidbody;
        currentBallRigidbody.isKinematic = true;

    }

    public void WinPanel()
    {
        if (!songs[1].isPlaying)
            songs[1].Play();
        panels[0].SetActive(true);
    }
    public void GameOverPanel()
    {
        if(!songs[2].isPlaying)
         songs[2].Play();
        panels[1].SetActive(true);
    }
   

    public void NextLevelButton()
    {
        songs[3].Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameEnvironment.singleton.LevelUp();
        if (GameEnvironment.singleton.GetCurrentLevel() > GameEnvironment.singleton.GetHighestLevel())
        {
            GameEnvironment.singleton.SetHighestLevel(GameEnvironment.singleton.GetCurrentLevel());
        }
    }

    public void PlayAgainButton()
    {
        songs[3].Play();
        GameEnvironment.singleton.ResetLevel();
        SceneManager.LoadScene("PlayScene");
    }

    public void HomeButton()
    {
        songs[3].Play();
        SceneManager.LoadScene("HomeScene");
        GameEnvironment.singleton.ResetLevel();
    }

    private void OnApplicationQuit()
    {
        GameEnvironment.singleton.ResetLevel();
    }

}

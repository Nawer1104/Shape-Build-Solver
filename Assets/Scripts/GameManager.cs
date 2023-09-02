using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const float DOUBLE_CLICK_TIME = .2f;

    private float lastClickTime;

    public static GameManager Instance;

    public List<Level> levels;

    private int startIndex = 0;

    private int currentIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentIndex = startIndex;

        levels[currentIndex].gameObject.SetActive(true);
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public void ReSetCurrentBallPos()
    {
        levels[currentIndex].ball.ResetPos();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
            {
                levels[currentIndex].ball.SetGravityScale(2);
            }

            lastClickTime = Time.time;
        }

        if (levels[currentIndex].stars.Count == 0)
        {
            levels[currentIndex].gameObject.SetActive(false);

            currentIndex += 1;

            if (currentIndex == 3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                currentIndex = 0;
            }

            levels[currentIndex].gameObject.SetActive(true);
        }
    }
}
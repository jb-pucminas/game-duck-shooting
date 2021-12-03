using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    bool running = true;

    int maxKill = 18;
    int totalKill = 0;
    public Text killTXT;

    float timer = 10.0f;
    public Text timerTXT;

    public Image awardImage01;
    public Image awardImage02;
    public Image awardImage03;
    public GameObject endgameCanvas;

    private void Start()
    {
        ResetTotalKills();
        endgameCanvas.SetActive(false);
    }

    void Update()
    {
        if (running == true)
        {
            HandleClick();
            HandleTimer();
        }
    }

    void HandleTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = 0.0f;
            running = false;

            CheckGame();
        }

        double time = System.Math.Round(timer, 2);
        timerTXT.text = time.ToString();
    }

    void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 cameraForward = Camera.main.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(clickPos, cameraForward, out hit))
            {
                GameObject obj = hit.collider.gameObject;

                if (obj.layer == Configs.LAYER_DUCK_INDEX)
                {
                    AddTotalKills();
                    Destroy(obj.gameObject);
                }
            }
        }
    }

    void CheckGame()
    {
        if (running == false)
        {
            endgameCanvas.SetActive(true);

            int percent = totalKill * 100 / maxKill;
            if (percent < 50)
            {
                awardImage01.color = new Color(0, 0, 0);
                awardImage02.color = new Color(0, 0, 0);
                awardImage03.color = new Color(0, 0, 0);
            }
            else if (percent < 75)
            {
                awardImage02.color = new Color(0, 0, 0);
                awardImage03.color = new Color(0, 0, 0);
            }
            else if (percent < 100)
            {
                awardImage03.color = new Color(0, 0, 0);
            }
        }
    }

    void ResetTotalKills(int value = 0)
    {
        totalKill = value;
        killTXT.text = totalKill.ToString();
    }

    void AddTotalKills(int increment = 1)
    {
        totalKill += increment;
        killTXT.text = totalKill.ToString();
        if (totalKill >= maxKill) running = false;

        CheckGame();
    }

    public void HandleResetGame()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject jet;

    public GameObject dirLight;
    private Light dirLightSrc;
    public GameObject pointLight;
    private Light pointLightSrc;
    public GameObject spotLight;
    private Light spotLightSrc;


    public Text timerText;
    public Text mainText;

    public bool redToggled = false;
    public bool gameWon = false;
    public int timeSeconds = 15;

    public string STR_TIMER = "Remaining Time: ";
    public string STR_WIN = "You Win!";
    public string STR_LOSE = "Game Over";

    // Start is called before the first frame update
    void Start()
    {
        mainText.text = "";
        dirLightSrc = dirLight.GetComponent<Light>();
        pointLightSrc = pointLight.GetComponent<Light>();
        spotLightSrc = spotLight.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.A))
        {

            if (redToggled)
                RenderSettings.ambientSkyColor = Color.black;
            else
                RenderSettings.ambientSkyColor = Color.red;

            redToggled = !redToggled;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dirLightSrc.enabled = !dirLightSrc.enabled;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            pointLightSrc.enabled = !pointLightSrc.enabled;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            spotLightSrc.enabled = !spotLightSrc.enabled;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        spotLight.transform.LookAt(jet.transform);
        timerText.text = STR_TIMER + timeSeconds.ToString() + "s";

    }

    private void GameOver()
    {
        Debug.Log(STR_LOSE);
        mainText.text = STR_LOSE;
    }

    public void Win()
    {
        gameWon = true;
        Debug.Log(STR_WIN);
        mainText.text = STR_WIN;
    }





    public void StartTimer()
    {
        InvokeRepeating("DecrementTime", 1f, 1f);
    }

    private void DecrementTime()
    {
        timeSeconds--;
        if (timeSeconds <= 0)
        {
            timeSeconds = 0;//because this method will keep running
        }
        if (timeSeconds == 0 && !gameWon)
        {
            GameOver();
        }
    }
}

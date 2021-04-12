using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject health;
    public GameObject timerText;
    public GameObject player;
    private float time;
    public bool pause = false;
    public GameObject paused;

    public bool GameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        paused.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        health.GetComponent<Text>().text = "Health:   " + player.GetComponent<playerController>().health;
        timerText.GetComponent<Text>().text = "Time:  " + Time.time.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && pause == false)
        {
            pause = true;
            Time.timeScale = 0f;
            paused.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pause == true)
        {
            pause = false;
            Time.timeScale = 1f;
            paused.SetActive(false);
        }

        if (GameEnd)
            loadMainMenu();
    }

    void loadMainMenu()
    {
        StartCoroutine(lvlLoader("MainMenu", 1));
    }

    IEnumerator lvlLoader(string levelName, int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(levelName);
    }
}
//need to reference text field of text component and set text field to new string  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadLevel1()
    {
        StartCoroutine("levelLoader", "lvl1");  

    }
    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator levelLoader(string levelName)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("lvl1");
    }


}

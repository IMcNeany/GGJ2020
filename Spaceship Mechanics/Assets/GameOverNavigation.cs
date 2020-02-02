using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverNavigation : MonoBehaviour
{
  //  SceneSwitcher switcher; 

    // Start is called before the first frame update
    void Start()
    {
      //  switcher = gameObject.GetComponent<SceneSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReplayPressed()
    {
        // switcher.LoadScene()
        SceneManager.LoadScene(1);
    }

    public void OnMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleter : MonoBehaviour
{
    public Text level_text;
    public string level_name;
    public List<BrokenObject> broken_parts;
    public List<Door> doors;
    //list of doors?

    private void Awake()
    {
        level_text = GameObject.Find("LevelText").GetComponent<Text>();
        if(level_name == "")
        {
            level_name = gameObject.name;
        }
    }
    void Update()
    {
        bool complete = true;
        for(int i = 0; i < broken_parts.Count; i++)
        {
            if(broken_parts[i].broken == true)
            {
                complete = false;
            }
        }

        if(complete)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].isUnlocked = true;
                doors[i].UpdateLights();
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            level_text.text = level_name;
        }
    }
}

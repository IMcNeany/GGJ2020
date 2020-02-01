using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenObject : MonoBehaviour
{
    public Sprite broken_sprite;
    public Sprite fixed_sprite;
    public int debris_needed = 2;
    private int current_debris = 0;
    public bool broken = true;
    public Image progress_image;
    public GameObject progress_prefab;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = broken_sprite;
        broken = true;
        GameObject image_obj = Instantiate(progress_prefab, transform.position, transform.rotation) as GameObject;
        image_obj.transform.parent = GameObject.Find("Canvas").gameObject.transform;
        
        progress_image = image_obj.transform.GetChild(0).GetComponent<Image>();
        progress_image.fillAmount = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (current_debris >= debris_needed)
        {
            return;
        }
        else
        {
            if (collision.gameObject.tag == "Debris")
            {
                if(collision.gameObject.GetComponent<DebrisChecker>().magnetized == true)
                {
                    current_debris++;
                    collision.gameObject.GetComponent<DebrisChecker>().DestroyDebris();
                }
            }
        }
    }

    private void Update()
    {
        if(current_debris >= debris_needed)
        {
            GetComponent<SpriteRenderer>().sprite = fixed_sprite;
            broken = false;
        }
    }
}

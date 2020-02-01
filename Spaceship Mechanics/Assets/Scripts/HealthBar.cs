using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image backBar, fullBar;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        backBar = transform.GetChild(0).GetComponent<Image>();
        fullBar = transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fullBar.fillAmount = health;
    }
}

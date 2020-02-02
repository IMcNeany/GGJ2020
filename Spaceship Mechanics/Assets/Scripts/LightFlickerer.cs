using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightFlickerer : MonoBehaviour
{
    public Light2D current_light;
    private float flicker_timer = 5.0f;
    private float flicker_delay = 0.2f;
    private float original_intensity = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        if(current_light == null)
        {
            current_light = GetComponent<Light2D>();
            if(current_light.intensity > 0.0f)
            {
                original_intensity = current_light.intensity;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        flicker();
    }

    private void flicker()
    {
        if(flicker_timer <= 0.0f)
        {
            flicker_timer = Random.Range(0.5f, 8.0f);
            flicker_delay = Random.Range(0.2f, 1.0f);
        }
        if (flicker_timer <= flicker_delay && flicker_timer > 0.0f)
        {
            current_light.intensity = 0.0f;
        }
        else
        {
            current_light.intensity = original_intensity;
        }

        flicker_timer -= 1 * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public List<Transform> SpawnPostions;
    public GameObject asteroid;

    float time = 0;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > timer)
        {
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
       int number = Random.Range(0, SpawnPostions.Count);

        Asteroid currentAsteroid = Instantiate(asteroid, SpawnPostions[number]).GetComponent<Asteroid>();
        currentAsteroid.LaunchAsteroid();

        time = 0;
    }
}

using UnityEngine;

public class box_spawner_script : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private float spawn_time;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn_time = 3;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawn_time)
        {
            timer -= spawn_time;
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}

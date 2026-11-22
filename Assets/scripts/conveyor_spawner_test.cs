using UnityEngine;

public class conveyor_spawner : MonoBehaviour
{
    [SerializeField] private GameObject conveyor;
    [SerializeField] private GameObject box_spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(box_spawner, new Vector3(0, 2, 3), Quaternion.identity);

        for (int i = 0; i < 5; ++i)
        {
            Instantiate(conveyor, new Vector3(0, 0.9546664f, i * 2f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

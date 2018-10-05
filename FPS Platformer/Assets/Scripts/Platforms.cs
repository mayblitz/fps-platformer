using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField]
    private int range = 50;

    [SerializeField]
    private Transform platform;

    [SerializeField]
    private Transform lightSource;

    private void Awake()
    {
        int created = 0;
        int platforms = range * 10;

        while (created < platforms)
        {
            Transform point = Instantiate(platform);

            while (true)
            {
                float x = Random.Range(1, range);
                float y = Random.Range(1, range + 30);
                float z = Random.Range(1, range);

                Vector3 position = new Vector3(x, y, z);

                if (!Physics.CheckBox(position, point.localScale))
                {
                    point.localPosition = position;
                    created++;
                    break;
                }
            }

            point.eulerAngles = new Vector3(Random.Range(-15, 15), Random.Range(-50, 50), Random.Range(-15, 15));
        }
    }
}


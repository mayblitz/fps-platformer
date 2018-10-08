using System.Linq;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    [SerializeField]
    private int maxX = 100;

    [SerializeField]
    private int maxY = 200;

    [SerializeField]
    private int maxZ = 100;

    [SerializeField]
    private int lightIntervalY = 10;

    [SerializeField]
    private int numberOfPlatforms = 2000;

    [SerializeField]
    private Transform pointLight;

    [SerializeField]
    private Transform platform;

    private void Awake()
    {
        CreatePlatforms();
        //Vector3 firstLight = CreateLights();
        //CreatePlayer(firstLight);
        CreatePlayer();
    }

    private void CreatePlatforms()
    {
        int created = 0;

        while (created < numberOfPlatforms)
        {
            Transform point = Instantiate(platform);
            float scaleX = Random.Range(1, 8);
            float scaleY = Random.Range(1, 8);
            float scaleZ = Random.Range(1, 8);
            point.localScale = new Vector3(scaleX, scaleY, scaleZ);

            while (true)
            {
                float x = Random.Range(1, maxX);
                float y = Random.Range(1, maxY);
                float z = Random.Range(1, maxZ);

                Vector3 position = new Vector3(x, y, z);

                if (!Physics.CheckBox(position, point.localScale))
                {
                    point.localPosition = position;
                    created++;
                    break;
                }
            }

            point.eulerAngles = new Vector3(Random.Range(-5, 5), Random.Range(-50, 50), Random.Range(-5, 5));
        }
    }

    private Vector3 CreateLights()
    {
        bool isFirstLightFound = false;
        Vector3 firstLight = Vector3.zero;

        for (int i = 5; i < maxY; i += lightIntervalY)
        {
            Transform point = Instantiate(pointLight);

            float x = Random.Range(1, maxX);
            float z = Random.Range(1, maxZ);

            Vector3 position = new Vector3(x, i, z);

            point.localPosition = position;

            if (!isFirstLightFound)
            {
                firstLight = position;
                isFirstLightFound = true;
            }
        }

        return firstLight;
    }

    private void CreatePlayer(Vector3 firstLight)
    {
        GameObject player = GameObject.FindWithTag("Player");
        Collider[] platforms = Physics.OverlapSphere(firstLight, 15);

        Collider lowestPlatform = platforms.OrderBy(p => p.transform.localPosition.y).ToArray()[1];

        player.transform.position = new Vector3(
                   lowestPlatform.transform.position.x,
                   lowestPlatform.transform.position.y + 4,
                   lowestPlatform.transform.position.z);
    }

    private void CreatePlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Collider[] platforms = Physics.OverlapSphere(player.transform.position, 30);

        Collider lowestPlatform = platforms.OrderBy(p => p.transform.localPosition.y).ToArray()[1];

        player.transform.position = new Vector3(
                   lowestPlatform.transform.position.x,
                   lowestPlatform.transform.position.y + 4,
                   lowestPlatform.transform.position.z);
    }
}

using UnityEngine;

public class SamplePerlinTerrain : MonoBehaviour
{
    public int width = 30;
    public int depth = 30;
    public float scale = 0.1f;
    public float heightMultiplier = 8f;
    public int waterLevel = 4;

    public GameObject waterprefab;
    public GameObject Dirtprefab;
    public GameObject GrassPrefab;


    SamplePerlinNoise simpleNoise;
    void Start()
    {
        simpleNoise = GetComponent<SamplePerlinNoise>();

        

        Generate();
    }

    public void Generate()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);
        simpleNoise.seed = Random.Range(0, 10000);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float xCoord = x * scale + offsetX;
                float zCoord = z * scale + offsetZ;

                float noise = simpleNoise.Noise(xCoord, zCoord);

                int height = Mathf.RoundToInt(noise * heightMultiplier);

                CreateCube(x, z, height);
            }
        }
    }

    void CreateCube(int x, int z, int height)
    {
        for (int y = 0; y <= height; y++)
        {
            Vector3 position = new Vector3(x, y, z);
            if (y == height)
            {
                Instantiate(GrassPrefab, position, Quaternion.identity, transform);
            }
            else
            {
                Instantiate(Dirtprefab, position, Quaternion.identity, transform);
            }   
        }
        for (int y = height + 1; y <= waterLevel; y++)
        {
            Vector3 waterPosition = new Vector3(x, y, z);
            Instantiate(waterprefab, waterPosition, Quaternion.identity, transform);
        }
    }

}

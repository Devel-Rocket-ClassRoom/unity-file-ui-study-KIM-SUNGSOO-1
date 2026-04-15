using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[Serializable]
public class SomeClass
{
    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 pos;
    [JsonConverter(typeof(QuarternionConverter))]
    public Quaternion rot;
    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 scale;
    [JsonConverter(typeof(ColorConverter))]
    public Color color;

    public override string ToString()
    {
        return $"{pos}\n{rot}\n{scale}\n{color}\n";
    }
}
public class JsonTest2 : MonoBehaviour
{
    public string flieName = "test.json";
    public GameObject cube;
    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", flieName);

    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject capsulePrefab;
    public GameObject cylinderPrefab;

    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();

    private List<GameObject> createdObjects = new List<GameObject>();

    public Vector3 minPos = new Vector3(-5f, 0.5f, -5f);
    public Vector3 maxPos = new Vector3(5f, 3f, 5f);

    public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 maxScale = new Vector3(2f, 2f, 2f);

    public int minCreateCount = 1;
    public int maxCreateCount = 10;
    public JsonSerializerSettings jsonSettings;
    public void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuarternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());

        InitPrefabDictionary();
    }
    private void InitPrefabDictionary()
    {
        prefabDict.Clear();

        if (cubePrefab != null)
            prefabDict.Add("Cube", cubePrefab);

        if (spherePrefab != null)
            prefabDict.Add("Sphere", spherePrefab);

        if (capsulePrefab != null)
            prefabDict.Add("Capsule", capsulePrefab);

        if (cylinderPrefab != null)
            prefabDict.Add("Cylinder", cylinderPrefab);
    }
    public void Save()
    {
        SomeClass obj = new SomeClass
        {
            pos = cube.transform.position,
            rot = cube.transform.rotation,
            scale = cube.transform.localScale,
            color = cube.GetComponent<MeshRenderer>().material.color,

        };

        string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest");

        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        string path = Path.Combine(pathFolder, "test.json");

        string json = JsonConvert.SerializeObject(obj, jsonSettings);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string obj = File.ReadAllText(FileFullPath);
        var json = JsonConvert.DeserializeObject<SomeClass>(obj,jsonSettings);
        Debug.Log(obj);
        cube.transform.position = json.pos;
        cube.transform.rotation = json.rot;
        cube.transform.localScale = json.scale;
        cube.GetComponent<MeshRenderer>().material.color = json.color;

    }

    public void Clear()
    {
        Debug.Log($"Clear 호출 오브젝트: {gameObject.name}, ID: {GetInstanceID()}");
        Debug.Log("Clear 시작 개수: " + createdObjects.Count);
        

        for (int i = createdObjects.Count - 1; i >= 0; i--)
        {
            if (createdObjects[i] != null)
            {
                Debug.Log("삭제 요청: " + createdObjects[i].name);
                Destroy(createdObjects[i]);
            }
        }

        createdObjects.Clear();
        Debug.Log("리스트 비움 완료");
    }
    public void Create()
    {
        Debug.Log("Create 시작");
        Debug.Log($"Create 호출 오브젝트: {gameObject.name}, ID: {GetInstanceID()}");
        if (prefabDict.Count == 0)
        {
            Debug.LogWarning("등록된 프리팹이 없습니다.");
            return;
        }

        int randomCount = UnityEngine.Random.Range(minCreateCount, maxCreateCount + 1);
        Debug.Log("생성할 개수: " + randomCount);

        for (int i = 0; i < randomCount; i++)
        {
            Debug.Log("반복 시작 i = " + i);

            GameObject prefab = GetRandomPrefab();
            Debug.Log("선택된 프리팹: " + prefab.name);

            Vector3 randomPos = GetRandomPosition();
            Quaternion randomRot = GetRandomRotation();
            Vector3 randomScale = GetRandomScale();
            Color randomColor = GetRandomColor();

            GameObject go = Instantiate(prefab, randomPos, randomRot);
            Debug.Log("Instantiate 완료: " + go.name);

            go.transform.localScale = randomScale;

            MeshRenderer mr = go.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                mr.material.color = randomColor;
            }

            createdObjects.Add(go);
            Debug.Log("리스트 추가 완료 / 현재 개수: " + createdObjects.Count);
        }
    }
    private GameObject GetRandomPrefab()
    {
        List<GameObject> prefabList = new List<GameObject>(prefabDict.Values);
        int randomIndex = UnityEngine.Random.Range(0, prefabList.Count);
        return prefabList[randomIndex];
    }

    private Vector3 GetRandomPosition()
    {
        float x = UnityEngine.Random.Range(minPos.x, maxPos.x);
        float y = UnityEngine.Random.Range(minPos.y, maxPos.y);
        float z = UnityEngine.Random.Range(minPos.z, maxPos.z);

        return new Vector3(x, y, z);
    }

    private Quaternion GetRandomRotation()
    {
        float x = UnityEngine.Random.Range(0f, 360f);
        float y = UnityEngine.Random.Range(0f, 360f);
        float z = UnityEngine.Random.Range(0f, 360f);

        return Quaternion.Euler(x, y, z);
    }

    private Vector3 GetRandomScale()
    {
        float x = UnityEngine.Random.Range(minScale.x, maxScale.x);
        float y = UnityEngine.Random.Range(minScale.y, maxScale.y);
        float z = UnityEngine.Random.Range(minScale.z, maxScale.z);

        return new Vector3(x, y, z);
    }

    private Color GetRandomColor()
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);

        return new Color(r, g, b, 1f);
    }

}

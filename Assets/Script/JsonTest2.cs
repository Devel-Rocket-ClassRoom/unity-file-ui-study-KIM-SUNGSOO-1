using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

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

    public JsonSerializerSettings jsonSettings;
    public void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuarternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
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

        string json = JsonConvert.SerializeObject(obj);
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

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        //Save
    //        SomeClass obj = new SomeClass
    //        {
    //            pos = cube.transform.position,
    //            rot = cube.transform.rotation,
    //            scale = cube.transform.localScale,
    //            color = cube.GetComponent<MeshRenderer>().material.color,

    //        };

    //        string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest");

    //        if (!Directory.Exists(pathFolder))
    //        {
    //            Directory.CreateDirectory(pathFolder);
    //        }
    //        string path = Path.Combine(pathFolder, "player3.json");

    //        string json = JsonConvert.SerializeObject(obj);
    //        File.WriteAllText(path, json);

    //        Debug.Log(path);
    //        Debug.Log(json);

    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        string path = Path.Combine(Application.persistentDataPath, "JsonTest", "player3.json");

    //        string json = File.ReadAllText(path);
    //        SomeClass obj = JsonConvert.DeserializeObject<SomeClass>(json);
    //        Debug.Log($"{obj.pos}/{obj.rot}/{obj.scale}/{obj.color}");

    //    }
    //}
    
}

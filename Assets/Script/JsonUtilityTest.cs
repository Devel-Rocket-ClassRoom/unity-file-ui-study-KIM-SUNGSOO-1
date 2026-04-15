using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;
    public Dictionary<string, int> score = new Dictionary<string, int>
    {
        {"stage1", 100 },
        {"stage2", 200 },
    }; //UnityJsonUtility가 지원을안함
}
public class JsonUtilityTest : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Save
            PlayerInfo obj = new PlayerInfo
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
                
            };

            string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest");

            if(!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            string path = Path.Combine(pathFolder,"player.json");
            string json = JsonUtility.ToJson(obj, prettyPrint: true);
            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);

        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            string path = Path.Combine(Application.persistentDataPath,"JsonTest", "player.json");

            string json = File.ReadAllText(path);
            PlayerInfo obj = JsonUtility.FromJson<PlayerInfo>(json);
            JsonUtility.FromJsonOverwrite(json, obj);
            Debug.Log(json);
            Debug.Log($"{obj.playerName}/ {obj.health}/ {obj.lives}/ {obj.position}");
        }
    }
}

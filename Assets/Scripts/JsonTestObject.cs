using UnityEngine;

public class JsonTestObject : MonoBehaviour
{
    public string prefabName;
    private Renderer ren;

    private void Awake()
    {
        ren = GetComponent<Renderer>();
    }

    public void Set(ObjectSaveData data)
    {
        //prefabName = data.prefabName;
        transform.position = data.pos;
        transform.rotation = data.rot;
        transform.localScale = data.scale;
        ren.material.color = data.color;
    }

    public ObjectSaveData GetSaveData()
    {
        ObjectSaveData data = new ObjectSaveData();
        data.prefabName = prefabName;
        data.pos = transform.position;
        data.rot = transform.rotation;
        data.scale = transform.localScale;
        data.color = ren.material.color;
        return data;
    }
}

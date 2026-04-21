using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataV4();
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 4321;
            
            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);

                foreach (var saveItemData in SaveLoadManager.Data.ItemList)
                {
                    Debug.Log(saveItemData.instanceId);
                    Debug.Log(saveItemData.itemData.Name);
                    Debug.Log(saveItemData.CreationTime);

                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }

        }   
    }
}

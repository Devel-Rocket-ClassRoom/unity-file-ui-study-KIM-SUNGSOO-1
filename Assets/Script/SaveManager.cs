using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string saveFolderPath;
    private string saveFilePath1;
    private string saveFilePath2;
    private string saveFilePath3;
    
    private void Start()
    {
        saveFolderPath = Path.Combine(Application.persistentDataPath, "SaveData");
        saveFilePath1 = Path.Combine(saveFolderPath, "save1.txt");
        saveFilePath2 = Path.Combine(saveFolderPath, "save2.txt");
        saveFilePath3 = Path.Combine(saveFolderPath, "save3.txt");

        CreateSaveFolder();

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            CreateTestFiles();
            Debug.Log("파일 생성/덮어쓰기 완료");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("=== 세이브 파일 목록 ===");
            showFileList();
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            copyFile();
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            deleteFile();
            
        }
    }

    void CreateSaveFolder()
    {
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        Debug.Log("세이브 폴더 경로 : " + saveFolderPath);
        
    }
    void CreateTestFiles()
    {
        File.WriteAllText(Path.Combine(saveFolderPath, "save1.txt"),"내용1");
        File.WriteAllText(Path.Combine(saveFolderPath, "save2.txt"),"내용2");
        File.WriteAllText(Path.Combine(saveFolderPath, "save3.txt"),"내용3");
    }

    void showFileList()
    {
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
            Debug.Log($"폴더 생성: {saveFolderPath}");
        }
        

        string[] files = Directory.GetFiles(saveFolderPath);
        foreach (string file in files)
        {
            Debug.Log($"파일: {Path.GetFileName(file)}");
        }
    }
    void copyFile()
    {
        if (File.Exists(saveFilePath1))
        {
            File.Copy(saveFilePath1, Path.Combine(saveFolderPath, "save1_backup.txt"), true );
            Debug.Log("파일 복사 완료");
        }
        else
        {
            Debug.Log("save1.txt 파일이 없습니다.");
        }
    }

    void deleteFile()
    {
        if (File.Exists(saveFilePath3))
        {
            Debug.Log("파일이 존재합니다.");
            File.Delete(saveFilePath3);
            Debug.Log("파일이 삭제되었습니다.");
        }
        else
        {
            Debug.Log("삭제할 파일이 존재하지 않습니다.");
        }
    }
}

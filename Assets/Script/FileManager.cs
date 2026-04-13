using System.IO;
using UnityEngine;

public class FileXorEncryption : MonoBehaviour
{
    private string secretPath;
    private string encryptedPath;
    private string decryptedPath;

    private const byte KEY = 0xAB;
    private const string ORIGINAL_MESSAGE = "Hello Unity World";

    void Start()
    {
        secretPath = Path.Combine(Application.persistentDataPath, "secret.txt");
        encryptedPath = Path.Combine(Application.persistentDataPath, "encrypted.dat");
        decryptedPath = Path.Combine(Application.persistentDataPath, "decrypted.txt");

        Debug.Log("경로: " + Application.persistentDataPath);

        Debug.Log("S: 원본 생성 | E: 암호화 | D: 복호화 | P: 결과 출력");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CreateOriginalFile();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EncryptFile(secretPath, encryptedPath, KEY);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DecryptFile(encryptedPath, decryptedPath, KEY);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintResults();
        }
    }

    void CreateOriginalFile()
    {
        File.WriteAllText(secretPath, ORIGINAL_MESSAGE);
        Debug.Log("원본 생성 완료: " + File.ReadAllText(secretPath));
    }

    void EncryptFile(string sourcePath, string destPath, byte key)
    {
        if (!File.Exists(sourcePath))
        {
            Debug.LogWarning("원본 파일이 없음! 먼저 S 눌러서 생성해라");
            return;
        }

        using (FileStream input = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
        using (FileStream output = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        {
            int data;
            while ((data = input.ReadByte()) != -1)
            {
                byte encryptedByte = (byte)(data ^ key);
                output.WriteByte(encryptedByte);
            }

            Debug.Log($"암호화 완료 (파일 크기: {output.Length} bytes)");
        }
    }

    void DecryptFile(string sourcePath, string destPath, byte key)
    {
        if (!File.Exists(sourcePath))
        {
            Debug.LogWarning("암호화 파일이 없음! 먼저 E 눌러라");
            return;
        }

        using (FileStream input = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
        using (FileStream output = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        {
            int data;
            while ((data = input.ReadByte()) != -1)
            {
                byte decryptedByte = (byte)(data ^ key);
                output.WriteByte(decryptedByte);
            }

            Debug.Log("복호화 완료");
        }
    }

    void PrintResults()
    {
        if (!File.Exists(secretPath) || !File.Exists(decryptedPath))
        {
            Debug.LogWarning("파일이 부족함 (S → E → D 순서로 실행해라)");
            return;
        }

        string original = File.ReadAllText(secretPath);
        string decrypted = File.ReadAllText(decryptedPath);

        Debug.Log("원본: " + original);
        Debug.Log("복호화 결과: " + decrypted);
        Debug.Log("원본과 일치: " + (original == decrypted));
    }
}
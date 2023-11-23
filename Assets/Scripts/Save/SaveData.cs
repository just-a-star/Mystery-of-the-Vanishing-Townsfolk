using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public static SaveData singleton;
    [Header("Posisi Player")]
    public GameObject playerPosition;
    public VectorValue playerStorage;

    [Header("Scene Player")]
    public StringValue playerScene;

    [Header("Objek Player")]
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    public bool AdaFile;

    private void Awake()
    {
        singleton = this;
    }

    public void SaveGame()
    {
        playerStorage.initialValue = playerPosition.transform.position;
        playerScene.initialValue = SceneManager.GetActiveScene().name;
        for (int i = 0; i < objects.Count; i++)
        {
            string objek = JsonUtility.ToJson(objects[i]);
            File.WriteAllText(Application.persistentDataPath + $"/objekPlayer{i}.json", objek);

            Debug.Log(objek);
        }

        Debug.Log("Data tersimpan");

    }

    public void LoadGame()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            string filePath = Application.persistentDataPath + $"/objekPlayer{i}.json";

            if (File.Exists(filePath))
            {
                AdaFile = true;
                string jsonText = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(jsonText, objects[i]);
                Debug.Log($"Data objekPlayer{i} dimuat");

                Debug.Log(jsonText);
            }
            else
            {
                AdaFile = false;
                Debug.LogError($"File objekPlayer{i}.json tidak ditemukan");
            }
        }
    }

    public void ResetGame()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            string filePath = Application.persistentDataPath + $"/objekPlayer{i}.json";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);

                if (objects[i] is FloatValue floatValue)
                {
                    floatValue.initialValue = floatValue.defaultValue;
                }
                else if (objects[i] is BoolValue boolValue)
                {
                    boolValue.initialValue = boolValue.defaultValue;
                }

                Debug.Log($"File objekPlayer{i}.json dihapus dan initialValue diatur ke defaultValue");
            }
            else
            {
                Debug.Log($"File objekPlayer{i}.json tidak ditemukan");
            }
        }

        Debug.Log("Scriptables direset");
    }

}

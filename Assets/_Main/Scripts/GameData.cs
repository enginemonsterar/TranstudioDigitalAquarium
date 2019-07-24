using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : Singleton<GameData>
{
    [SerializeField] private InputField fieldPath;

    // Start is called before the first frame update

    public void SavePath(){
        PlayerPrefs.GetString("fieldPath", fieldPath.text);
        Debug.Log(fieldPath.text);
    }

    public string GetPath(){
        return PlayerPrefs.GetString("fieldPath", "");
    }


}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SaveFilePath = Application.persistentDataPath + "/geometry_survival_gamestate";
    public static readonly BinaryFormatter Formatter = new BinaryFormatter();

    static SaveSystem()
    {
        var ss = new SurrogateSelector();
        var context = new StreamingContext(StreamingContextStates.All);
        ss.AddSurrogate(typeof(Quaternion), context, new QuaternionSerializationSurrogate());
        ss.AddSurrogate(typeof(Vector3), context, new Vector3SerializationSurrogate());
        Formatter.SurrogateSelector = ss;
    }

    public static void SaveGame(GameState gameState)
    {
        using (var stream = new FileStream(SaveFilePath, FileMode.Create))
        {

            try
            {
                Formatter.Serialize(stream, gameState);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        };
    }

    public static GameState LoadGame()
    {
        if (File.Exists(SaveFilePath))
        {
            using (var stream = new FileStream(SaveFilePath, FileMode.Open))
            {
                try
                {
                    GameState gameState = (GameState)Formatter.Deserialize(stream);
                    return gameState;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return null;
                }
            };
        }
        else
        {
            return null;
        }
    }

    public static bool CheckSaveFileExist() => File.Exists(SaveFilePath);

    public static bool DeleteSaveFile()
    {
        try
        {
            File.Exists(SaveFilePath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return false;
        }
    }
}

using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{	
	public static bool IsLoading = false;


	public static bool HasSave()
	{
		string path = Application.persistentDataPath + "/Save.bin";
		if (File.Exists(path)) return true;
		else return false;
	}

	public static void Save()
	{
		string path = Application.persistentDataPath + "/Save.bin";
		using (FileStream stream = new FileStream(path,FileMode.Create))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			PlayerData data = new PlayerData();
			formatter.Serialize(stream, data);
		}
	}

	public static void Load()
	{
		string path = Application.persistentDataPath + "/Save.bin";
		if (HasSave())
		{
			using (FileStream stream = new FileStream(path, FileMode.Open))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				PlayerData data = formatter.Deserialize(stream) as PlayerData;

				PlayerManager.instance.LoadData(data);
			}
		}



	}
}

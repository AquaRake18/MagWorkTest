using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
	private static string _LevelDataPath = Application.dataPath + "/LevelData/levels.dat";
	private static string _UserDataPath = Application.persistentDataPath + "/user.dat";

	public static void SaveLevels(LevelData[] storedLevels) {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(_LevelDataPath, FileMode.Create);

		formatter.Serialize(stream, storedLevels);
		stream.Close();
	}

	public static LevelCollection LoadLevels() {
		if (File.Exists(_LevelDataPath)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(_LevelDataPath, FileMode.Open);

			LevelData[] data = formatter.Deserialize(stream) as LevelData[];
			stream.Close();
			return new LevelCollection(data);
		} else {
			Debug.LogWarning("Levels save file not found at " + _LevelDataPath);
		}
		return new LevelCollection(null);
	}

	public static void SaveUserData(UserData playerData) {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(_UserDataPath, FileMode.Create);

		formatter.Serialize(stream, playerData);
		stream.Close();
	}

	public static UserData LoadUserData() {
		if (File.Exists(_UserDataPath)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(_UserDataPath, FileMode.Open);

			UserData data = formatter.Deserialize(stream) as UserData;
			stream.Close();
			return data;
		} else {
			Debug.LogWarning("User save file not found at " + _UserDataPath);
		}
		return null;
	}
}

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
	private static string _AssetsPath = Application.dataPath + "/LevelData/levels.dat";
	private static string _SecurePath = Application.persistentDataPath + "/levels.dat";

	public static void SaveLevels(LevelData[] storedLevels) {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(_AssetsPath, FileMode.Create);

		formatter.Serialize(stream, storedLevels);
		stream.Close();
	}

	public static LevelData[] LoadLevels() {
		if (File.Exists(_AssetsPath)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(_AssetsPath, FileMode.Open);

			LevelData[] data = formatter.Deserialize(stream) as LevelData[];
			stream.Close();
			return data;
		} else {
			Debug.LogWarning("Save file not found at " + _AssetsPath);
		}
		return null;
	}
}

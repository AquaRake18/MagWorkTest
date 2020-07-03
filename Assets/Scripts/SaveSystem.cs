using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
	private static string _Path = Application.persistentDataPath + "/levels.dat";

	public static void SaveLevels(LevelData[] storedLevels) {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(_Path, FileMode.Create);

		formatter.Serialize(stream, storedLevels);
		stream.Close();
	}

	public static LevelData[] LoadLevels() {
		if (File.Exists(_Path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(_Path, FileMode.Open);

			LevelData[] data = formatter.Deserialize(stream) as LevelData[];
			stream.Close();
			return data;
		} else {
			Debug.LogWarning("Save file not found at " + _Path);
		}
		return null;
	}
}

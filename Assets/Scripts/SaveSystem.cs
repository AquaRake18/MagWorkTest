using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
	private static string _Path = Application.persistentDataPath + "/levels.dat";

	public static void SaveLevels(LevelCollection levelCollection) {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(_Path, FileMode.Create);

		formatter.Serialize(stream, levelCollection);
		stream.Close();
	}

	public static LevelCollection LoadLevels() {
		if (File.Exists(_Path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(_Path, FileMode.Open);

			LevelCollection data = formatter.Deserialize(stream) as LevelCollection;
			stream.Close();
			return data;
		} else {
			Debug.LogWarning("Save file not found at " + _Path);
		}
		return null;
	}
}

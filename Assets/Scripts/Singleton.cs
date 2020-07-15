using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _Instance;
    public static T Instance {
        get {
            if (_Instance == null) {
                _Instance = (T)FindObjectOfType(typeof(T));
                if (_Instance == null) {
                    GameObject singletonObject = new GameObject();
                    _Instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }
            }
            return _Instance;
        }
    }
}

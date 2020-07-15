using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
	: Singleton<ObjectPooler>
	, IObserverUnloadScene {
    // Singleton
	protected ObjectPooler() {}

	[System.Serializable]
	public class Pool {
		public string _Tag;
		public GameObject _Prefab;
		public int _Count;
	}

	public List<Pool> _InspectorPools = new List<Pool>();
	private Dictionary<string, Queue<GameObject>> _PooledObjects = new Dictionary<string, Queue<GameObject>>();

	void Start() {
		Publisher.Instance.Subscribe(this);
		if (_InspectorPools.Count > 0) {
			foreach (Pool pool in _InspectorPools) {
				AddPool(pool);
			}
		}
	}

	// IObserverUnloadScene
	public void OnUnloadScene() {
		_PooledObjects.Clear();
		foreach(KeyValuePair<string, Queue<GameObject>> pair in _PooledObjects) {
			foreach (GameObject go in pair.Value) {
				go.SetActive(false);
				go.transform.SetParent(gameObject.transform);
			}
		}
	}

	public void AddPool(Pool pool) {
		if (_PooledObjects.ContainsKey(pool._Tag)) {
			return;
		}
		Queue<GameObject> objPool = new Queue<GameObject>();
		for (int i = 0; i < pool._Count; ++i) {
			InstantiateToPool(objPool, pool._Prefab);
		}
		_PooledObjects.Add(pool._Tag, objPool);
	}

	public GameObject SpawnFromPool(string tag) {
		if (!_PooledObjects.ContainsKey(tag)) {
			return null;
		}
		GameObject go = _PooledObjects[tag].Dequeue();
		_PooledObjects[tag].Enqueue(go);
		if (!go.activeSelf) {
			ResetGameObject(go);
			return go;
		}
		Debug.LogWarning($"Pool {tag}'s size is too small. Will instantiate during runtime.");
		GameObject goClone = InstantiateToPool(_PooledObjects[tag], go);
		ResetGameObject(goClone);
		return goClone;
	}

	private GameObject InstantiateToPool(Queue<GameObject> objPool, GameObject prefab) {
		GameObject go = Instantiate(
			prefab,
			Vector3.zero,
			Quaternion.identity
		);
		go.SetActive(false);
		go.transform.SetParent(gameObject.transform);
		objPool.Enqueue(go);
		return go;
	}

	private void ResetGameObject(GameObject go) {
		go.SetActive(true);
		IPooledObject goSpawned = go.GetComponent<IPooledObject>();
		if (goSpawned != null) {
			goSpawned.OnSpawn();
		}
	}
}

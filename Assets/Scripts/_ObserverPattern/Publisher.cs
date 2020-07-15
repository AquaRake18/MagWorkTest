using System;
using System.Collections.Generic;

public class Publisher : Singleton<Publisher> {
    // Singleton
    protected Publisher() {}

    private List<IObserverLevelEnd> _ObserversLevelEnd = new List<IObserverLevelEnd>();
    private List<IObserverUnloadScene> _ObserversUnloadScene = new List<IObserverUnloadScene>();

    public void UnsubscribeAll() {
        _ObserversLevelEnd.Clear();
        _ObserversUnloadScene.Clear();
    }

    public void NotifyAll(ESubjectTypes subject) {
        switch (subject) {
            case ESubjectTypes.LevelEnd:
                foreach (IObserverLevelEnd observer in _ObserversLevelEnd) {
                    observer.OnLevelEnd();
                }
                break;
            case ESubjectTypes.UnloadScene:
                foreach (IObserverUnloadScene observer in _ObserversUnloadScene) {
                    observer.OnUnloadScene();
                }
                break;
        }
    }

    public void Subscribe(object obj) {
        if (typeof(IObserverLevelEnd).IsAssignableFrom(obj.GetType())) {
            _ObserversLevelEnd.Add((IObserverLevelEnd)obj);
        }
        if (typeof(IObserverUnloadScene).IsAssignableFrom(obj.GetType())) {
            _ObserversUnloadScene.Add((IObserverUnloadScene)obj);
        }
    }

    public void Unsubscribe(object obj) {
        if (typeof(IObserverLevelEnd).IsAssignableFrom(obj.GetType())) {
            _ObserversLevelEnd.Remove((IObserverLevelEnd)obj);
        }
        if (typeof(IObserverUnloadScene).IsAssignableFrom(obj.GetType())) {
            _ObserversUnloadScene.Remove((IObserverUnloadScene)obj);
        }
    }
}

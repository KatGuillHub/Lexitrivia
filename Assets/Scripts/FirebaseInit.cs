using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseInit : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log(" Firebase inicializado correctamente");
            }
            else
            {
                Debug.LogError(" Error al inicializar Firebase: " + dependencyStatus);
            }
        });
    }
}


using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class MostrarPuntajeInicio : MonoBehaviour
{
    public TMP_Text textoPuntuacion;
    private DatabaseReference dbRef;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbRef = FirebaseDatabase.GetInstance(app, "https://lextrivia-umng-default-rtdb.firebaseio.com/").RootReference;

                if (!string.IsNullOrEmpty(UserSession.uid))
                {
                    dbRef.Child("users").Child(UserSession.uid).Child("score").GetValueAsync().ContinueWith(t =>
                    {
                        if (t.IsCompleted && t.Result != null && t.Result.Exists)
                        {
                            int score = int.Parse(t.Result.Value.ToString());
                            UserSession.score = score;

                            // Actualizar UI en hilo principal
                            UnityMainThreadDispatcher.Instance().Enqueue(() =>
                            {
                                textoPuntuacion.text = "Puntaje: " + score;
                            });
                        }
                        else
                        {
                            Debug.LogWarning("⚠ No se encontró puntuación en Firebase.");
                        }
                    });
                }
            }
            else
            {
                Debug.LogError("❌ Firebase no disponible: " + task.Result);
            }
        });
    }
}

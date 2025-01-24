using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private void Awake()
    
    {
        // Si ya existe una instancia de este objeto, destruye el duplicado
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Mantén este objeto entre escenas
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}


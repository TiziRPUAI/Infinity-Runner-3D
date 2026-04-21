using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class AutoInputSystemFixer : MonoBehaviour
{
    private static AutoInputSystemFixer instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeInputSystemBeforeScene()
    {
        Debug.Log("[InputSystem] Inicializando Input System ANTES de cargar escena...");
        ConfigureEventSystem();
    }

    private void Awake()
    {
        // Singleton para que solo haya una instancia
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        ConfigureEventSystem();
    }

    private void Start()
    {
        ConfigureEventSystem();
    }

    private void Update()
    {
        // Verificar cada frame si el módulo antiguo aparece
        EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();
        if (eventSystem != null)
        {
            StandaloneInputModule oldModule = eventSystem.GetComponent<StandaloneInputModule>();
            if (oldModule != null)
            {
                Debug.LogWarning("[InputSystem] StandaloneInputModule detectado nuevamente, limpiando...");
                ConfigureEventSystem();
            }
        }
    }

    private static void ConfigureEventSystem()
    {
        EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();

        if (eventSystem == null)
        {
            Debug.LogWarning("[InputSystem] No se encontró EventSystem en la escena");
            return;
        }

        bool changed = false;

        // Remover todos los StandaloneInputModule que encuentre
        StandaloneInputModule[] oldModules = eventSystem.GetComponents<StandaloneInputModule>();
        foreach (var oldModule in oldModules)
        {
            if (oldModule != null)
            {
                Debug.Log("[InputSystem] Destruyendo StandaloneInputModule...");
                oldModule.enabled = false;
                Object.DestroyImmediate(oldModule, true);
                changed = true;
            }
        }

        // Agregar el módulo nuevo si no existe
        InputSystemUIInputModule newModule = eventSystem.GetComponent<InputSystemUIInputModule>();
        if (newModule == null)
        {
            Debug.Log("[InputSystem] Agregando InputSystemUIInputModule...");
            newModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            changed = true;
        }

        if (changed)
        {
            Debug.Log("✓ [InputSystem] EventSystem reconfigurado correctamente");
        }
    }
}

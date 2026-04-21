using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class InputSystemInitializer : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("[InputSystem] InputSystemInitializer.Awake() ejecutándose...");
        ConfigureEventSystem();
    }

    private void Start()
    {
        Debug.Log("[InputSystem] InputSystemInitializer.Start() verificando nuevamente...");
        ConfigureEventSystem();
    }

    private void LateUpdate()
    {
        // Verificación adicional en LateUpdate
        EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();
        if (eventSystem != null)
        {
            StandaloneInputModule oldModule = eventSystem.GetComponent<StandaloneInputModule>();
            if (oldModule != null && oldModule.enabled)
            {
                Debug.LogWarning("[InputSystem] StandaloneInputModule aún está activo, desactivando...");
                oldModule.enabled = false;
                Object.DestroyImmediate(oldModule);
                ConfigureEventSystem();
            }
        }

        // Ejecutar solo una vez
        enabled = false;
    }

    private static void ConfigureEventSystem()
    {
        EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();

        if (eventSystem == null)
        {
            Debug.LogWarning("[InputSystem] No se encontró EventSystem en la escena");
            return;
        }

        try
        {
            // Remover el módulo antiguo si existe
            StandaloneInputModule oldModule = eventSystem.GetComponent<StandaloneInputModule>();
            if (oldModule != null)
            {
                Debug.Log("[InputSystem] Eliminando StandaloneInputModule antiguo...");
                oldModule.enabled = false;
                Object.DestroyImmediate(oldModule, true);
                Debug.Log("[InputSystem] StandaloneInputModule eliminado");
            }

            // Agregar el módulo nuevo si no existe
            InputSystemUIInputModule newModule = eventSystem.GetComponent<InputSystemUIInputModule>();
            if (newModule == null)
            {
                Debug.Log("[InputSystem] Agregando InputSystemUIInputModule...");
                eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
                Debug.Log("[InputSystem] InputSystemUIInputModule agregado");
            }
            else
            {
                Debug.Log("[InputSystem] InputSystemUIInputModule ya existe");
            }

            Debug.Log("✓ [InputSystem] EventSystem configurado correctamente para Input System");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[InputSystem] Error configurando EventSystem: {ex.Message}");
        }
    }
}

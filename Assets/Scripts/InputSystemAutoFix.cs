using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using System.Collections.Generic;

/// <summary>
/// Elimina continuamente el StandaloneInputModule y lo reemplaza con InputSystemUIInputModule
/// Se ejecuta en cada frame para asegurar que no vuelva a aparecer
/// </summary>
public class InputSystemAutoFix : MonoBehaviour
{
    private static InputSystemAutoFix instance;
    private int lastFrameFixed = -1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BeforeSceneLoad()
    {
        Debug.Log("[InputSystem] RuntimeInitialize: BeforeSceneLoad");
        FixEventSystemImmediate();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AfterSceneLoad()
    {
        Debug.Log("[InputSystem] RuntimeInitialize: AfterSceneLoad");
        FixEventSystemImmediate();
    }

    private void Awake()
    {
        // Crear singleton persistente
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad para que persista entre recargas
            if (gameObject.name != "[InputSystemAutoFix]")
            {
                gameObject.name = "[InputSystemAutoFix]";
            }
            DontDestroyOnLoad(gameObject);
            Debug.Log("[InputSystem] InputSystemAutoFix singleton creado");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        FixEventSystemImmediate();
    }

    private void Start()
    {
        FixEventSystemImmediate();
    }

    private void Update()
    {
        // Verificar cada frame (pero solo si cambió algo)
        if (Time.frameCount != lastFrameFixed)
        {
            EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();
            if (eventSystem != null)
            {
                StandaloneInputModule oldModule = eventSystem.GetComponent<StandaloneInputModule>();
                if (oldModule != null)
                {
                    Debug.LogWarning($"[InputSystem] Frame {Time.frameCount}: StandaloneInputModule detectado, eliminando inmediatamente...");
                    FixEventSystemImmediate();
                    lastFrameFixed = Time.frameCount;
                }
            }
        }
    }

    private void LateUpdate()
    {
        // Segunda verificación en LateUpdate
        EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();
        if (eventSystem != null)
        {
            StandaloneInputModule oldModule = eventSystem.GetComponent<StandaloneInputModule>();
            if (oldModule != null && oldModule.enabled)
            {
                Debug.LogWarning("[InputSystem] LateUpdate: StandaloneInputModule aún activo");
                oldModule.enabled = false;
                Object.DestroyImmediate(oldModule, true);
            }
        }
    }

    private static void FixEventSystemImmediate()
    {
        EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.LogWarning("[InputSystem] No EventSystem encontrado");
            return;
        }

        // Obtener TODOS los componentes y eliminar StandaloneInputModule por tipo
        List<StandaloneInputModule> toRemove = new List<StandaloneInputModule>();
        foreach (var component in eventSystem.GetComponents<StandaloneInputModule>())
        {
            if (component != null)
            {
                toRemove.Add(component);
            }
        }

        // Eliminar todos los módulos antiguos encontrados
        foreach (var oldModule in toRemove)
        {
            Debug.Log("[InputSystem] Eliminando StandaloneInputModule");
            oldModule.enabled = false;
            Object.DestroyImmediate(oldModule, true);
        }

        // Agregar el módulo nuevo
        InputSystemUIInputModule newModule = eventSystem.GetComponent<InputSystemUIInputModule>();
        if (newModule == null)
        {
            Debug.Log("[InputSystem] Agregando InputSystemUIInputModule");
            newModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            if (newModule != null)
            {
                newModule.enabled = true;
                Debug.Log("[InputSystem] InputSystemUIInputModule habilitado");
            }
        }

        Debug.Log("✓ [InputSystem] EventSystem configurado correctamente");
    }
}

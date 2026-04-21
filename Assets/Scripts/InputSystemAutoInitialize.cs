using UnityEngine;

/// <summary>
/// Crea automáticamente el GameObject con InputSystemAutoFix
/// </summary>
public static class InputSystemAutoInitialize
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateAutoFixObject()
    {
        Debug.Log("[InputSystem] Inicializador: Creando objeto para InputSystemAutoFix...");

        // Buscar si ya existe
        InputSystemAutoFix existing = Object.FindFirstObjectByType<InputSystemAutoFix>();
        if (existing != null)
        {
            Debug.Log("[InputSystem] InputSystemAutoFix ya existe");
            return;
        }

        // Crear nuevo GameObject
        GameObject autoFixObj = new GameObject("[InputSystemAutoFix]");
        autoFixObj.AddComponent<InputSystemAutoFix>();
        Object.DontDestroyOnLoad(autoFixObj);

        Debug.Log("[InputSystem] Objeto InputSystemAutoFix creado y persistente");
    }
}

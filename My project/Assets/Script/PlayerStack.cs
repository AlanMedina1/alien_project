using UnityEngine;
using System.Collections.Generic;

public class PlayerStack : MonoBehaviour
{
    [Header("Configuración del Stack")]
    [SerializeField] private Transform stackParent; // El objeto vacío dentro del Player
    
    [Tooltip("Distancia en el eje Z entre cada objeto. Ajusta este valor según el largo de tu modelo.")]
    [SerializeField] private float objectSpacing = 0.6f; 

    // Lista para controlar los objetos acumulados
    private List<CollectibleObject> stackedObjects = new List<CollectibleObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (other.TryGetComponent<CollectibleObject>(out CollectibleObject collectible))
            {
                // DETECTAR ERROR DE ESCALA: Si el padre tiene escala alterada, avisamos en la consola
                if (stackParent.localScale != Vector3.one)
                {
                    Debug.LogWarning($"¡Atención! El 'StackParent' tiene una escala de {stackParent.localScale}. Para que la distancia sea exacta, su escala en el Inspector DEBE SER (1, 1, 1).");
                }

                // Calculamos la posición hacia ADELANTE (eje Z). 
                // Multiplicamos la cantidad de objetos por el espacio que ocupa cada uno.
                float targetZ = stackedObjects.Count * objectSpacing;
                
                // Mantenemos X e Y en 0 para que queden perfectamente alineados al centro
                Vector3 localTargetPosition = new Vector3(0f, 0f, targetZ);

                // Mandamos el objeto a su posición
                collectible.Collect(stackParent, localTargetPosition);

                // Lo añadimos a la lista
                stackedObjects.Add(collectible);
            }
        }
    }
}
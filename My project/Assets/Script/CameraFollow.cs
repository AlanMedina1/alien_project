using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform target; // Arrastra aquí a tu jugador (Player)

    [Header("Configuración")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 6f, -10f); // Distancia ideal (arriba y atrás)
    [SerializeField] private float smoothSpeed = 5f; // Qué tan suave sigue al jugador

    private void LateUpdate()
    {
        // Verificación de seguridad por si el jugador es destruido o no está asignado
        if (target == null) return;

        // 1. Calculamos la posición ideal a la que debería ir la cámara
        Vector3 desiredPosition = target.position + offset;

        // 2. Interpolamos de forma suave entre la posición actual de la cámara y la ideal
        // Usamos LateUpdate y Lerp para evitar que la cámara "tiemble" (jittering)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 3. Aplicamos la posición
        transform.position = smoothedPosition;
    }
}
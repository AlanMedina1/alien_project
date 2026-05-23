using UnityEngine;
using System.Collections;

public class CollectibleObject : MonoBehaviour
{
    private bool isCollected = false;

    // Este método se llamará desde el script que maneje el "Stack" del jugador
    public void Collect(Transform stackParent, Vector3 localTargetPosition)
    {
        if (isCollected) return;
        isCollected = true;

        // 1. Desactivar colisionadores y físicas para optimizar brutalmente el rendimiento móvil
        if (TryGetComponent<Collider>(out Collider col)) col.enabled = false;
        if (TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = true;

        // 2. Hacerlo hijo del punto de stack del jugador
        transform.SetParent(stackParent);

        // 3. Animación suave hacia su posición asignada en el montón
        StartCoroutine(AnimateToStack(localTargetPosition));
    }

    private IEnumerator AnimateToStack(Vector3 localTarget)
    {
        float elapsed = 0f;
        float duration = 0.2f; // Tiempo que tarda en "volar" al stack
        Vector3 startLocalPos = transform.localPosition;
        Quaternion startRotation = transform.localRotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Interpolación suave de posición y rotación local
            transform.localPosition = Vector3.Lerp(startLocalPos, localTarget, t);
            transform.localRotation = Quaternion.Lerp(startRotation, Quaternion.identity, t);
            
            yield return null;
        }

        transform.localPosition = localTarget;
        transform.localRotation = Quaternion.identity;
    }
}
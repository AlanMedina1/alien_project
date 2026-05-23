using UnityEngine;

public class PlayerMovement
{
    using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float sideSpeed = 0.5f;
    [SerializeField] private float limitX = 4f; // Límite lateral de la pista

    private Vector2 lastTouchPosition;

    void Update()
    {
        // 1. Movimiento constante hacia adelante
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 2. Control Táctil Lateral
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Guardamos la posición inicial donde el jugador tocó la pantalla
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Calculamos cuánto se movió el dedo horizontalmente desde el último frame
                float deltaX = touch.position.x - lastTouchPosition.x;

                // Calculamos la nueva posición deseada
                float newX = transform.position.x + (deltaX * sideSpeed * Time.deltaTime);

                // Limitamos la posición para que no se caiga de la plataforma
                newX = Mathf.Clamp(newX, -limitX, limitX);

                // Aplicamos la posición manteniendo la altura (Y) y el avance (Z) actuales
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);

                // Actualizamos la última posición del toque
                lastTouchPosition = touch.position;
            }
        }
    }
}
}

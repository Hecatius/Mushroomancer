using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // R�cup�rer le SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Obtenir la position de la souris dans l'espace monde
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        // Calculer la direction locale de la souris par rapport au sprite
        Vector3 localMousePosition = transform.InverseTransformPoint(mouseWorldPosition);

        // Flip du sprite bas� sur la position locale
        if (localMousePosition.x > 0) // La souris est � droite localement
        {
            spriteRenderer.flipX = false;
        }
        else if (localMousePosition.x < 0) // La souris est � gauche localement
        {
            spriteRenderer.flipX = true;
        }
    }

    // M�thode pour obtenir la position de la souris dans l'espace monde
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Ajuster la profondeur
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}

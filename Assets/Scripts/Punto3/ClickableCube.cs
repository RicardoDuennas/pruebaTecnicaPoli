using UnityEngine;

public class ClickableCube : MonoBehaviour
{
    private GameManager gameManager;
    private Color cubeColor;
    private float lifetime;
    private bool hasBeenClicked = false;

    public void Initialize(GameManager manager, Color color, float lifetime)
    {
        this.gameManager = manager;
        this.cubeColor = color;
        this.lifetime = lifetime;
        Destroy(gameObject, lifetime);
    }

    private void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            gameManager.AddScore(cubeColor);
            Destroy(gameObject);
        }
    }
}
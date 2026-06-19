using UnityEngine;

public class HouseColor : MonoBehaviour
{
    [SerializeField] public Color houseColor = Color.white;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = houseColor;
    }
}


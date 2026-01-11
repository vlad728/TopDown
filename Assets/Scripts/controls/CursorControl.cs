using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 pos = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(texture, pos, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

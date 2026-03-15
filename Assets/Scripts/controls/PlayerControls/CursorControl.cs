using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 pos = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(texture, pos, CursorMode.Auto);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSprite : MonoBehaviour
{
    public static CursorSprite instance;

    [Header("UI")]
    public Sprite cursorSell;
    public Sprite cursorUpgrade;
    public Sprite cursorNormal;

    Texture2D cursorText;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ChangeCursor("");
    }

    public void ChangeCursor(string cursor)
    {
        if(cursor == "")
        {
            Change(cursorNormal);
        } else if (cursor == "sell")
        {
            Change(cursorSell);
        }
        else if (cursor == "upgrade")
        {
            Change(cursorUpgrade);
        }
    }

    void Change(Sprite sprite)
    {
        cursorText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);


        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                              (int)sprite.textureRect.y,
                                              (int)sprite.textureRect.width,
                                              (int)sprite.textureRect.height);

        cursorText.SetPixels(pixels);
        cursorText.Apply();

        Cursor.SetCursor(cursorText, Vector2.zero, CursorMode.Auto);
    }
}

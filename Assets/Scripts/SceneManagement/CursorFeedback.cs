using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFeedback : MonoBehaviour
{
    public Texture2D redCursor;
    public Texture2D whiteCursor;
    private IEnumerator Feedback()
    {
        Cursor.SetCursor(redCursor, Vector2.zero, CursorMode.Auto);
        yield return new WaitForSeconds(0.15f);
        Cursor.SetCursor(whiteCursor, Vector2.zero, CursorMode.Auto);
    }

    public void StartCursorFeedback()
    {
        gameObject.SetActive(true);
        StartCoroutine(Feedback());
    }
}

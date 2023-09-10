using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFeedback : MonoBehaviour
{
    [SerializeField] private Texture2D redCursor;
    [SerializeField] private Texture2D whiteCursor;
    private IEnumerator Feedback()
    {
        Cursor.SetCursor(redCursor, Vector2.zero, CursorMode.Auto);
        yield return new WaitForSeconds(0.15f);
        Cursor.SetCursor(whiteCursor, Vector2.zero, CursorMode.Auto);
    }
/// <summary>
/// turns the cursor red when enemy is defeated
/// </summary>
    public void StartCursorFeedback()
    {
        gameObject.SetActive(true);
        StartCoroutine(Feedback());
    }
}

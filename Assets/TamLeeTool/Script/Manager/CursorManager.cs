using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

    public static CursorManager Instance;

    public Texture2D cursor_normal;
    public Texture2D cursor_npc_talk;
    public Texture2D cursor_attack;
    public Texture2D cursor_lockTarget;
    public Texture2D cursor_pick;

    private Vector2 hotspot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    public void Init()
    {
        Instance = this;
    }

    public void SetNormal()
    {
        Cursor.SetCursor(cursor_normal,hotspot,cursorMode);
    }

    public void SetNpcTalk()
    {
        Cursor.SetCursor(cursor_npc_talk, hotspot, cursorMode);
    }

    public void SetAttack()
    {
        Cursor.SetCursor(cursor_attack, hotspot, cursorMode);
    }

    public void SetLockTarget()
    {
        Cursor.SetCursor(cursor_lockTarget, hotspot, cursorMode);
    }

    public void SetPick()
    {
        Cursor.SetCursor(cursor_pick, hotspot, cursorMode);
    }
}

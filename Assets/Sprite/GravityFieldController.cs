using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldStateMachine : MonoBehaviour
{
    public GameObject gravityField;

    public enum GravityFieldState
    {
        State1_Normal,
        State2_SelectArea,
        State3_SelectDirection
    }

    public GravityFieldState currentState = GravityFieldState.State1_Normal;

    [Header("=== 拖拽矩形 Sprite ===")]
    public SpriteRenderer selectionSprite;

    [Header("=== 方向十字 Sprite ===")]
    public SpriteRenderer directionCrossSprite;

    private bool isDragging = false;
    private Vector3 dragStartPos;
    private Vector3 dragEndPos;

    // 状态3使用的中心点
    private Vector3 directionCenter;


    void Start()
    {
        if (selectionSprite != null)
            selectionSprite.gameObject.SetActive(false);

        if (directionCrossSprite != null)
            directionCrossSprite.gameObject.SetActive(false);
    }


    void Update()
    {
        switch (currentState)
        {
            case GravityFieldState.State1_Normal:
                HandleState1();
                break;

            case GravityFieldState.State2_SelectArea:
                HandleState2();
                break;

            case GravityFieldState.State3_SelectDirection:
                HandleState3();
                break;
        }
    }


    // ----------------------------
    // 状态 1
    // ----------------------------
    void HandleState1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragStartPos.z = 0;

            isDragging = true;

            if (selectionSprite != null)
                selectionSprite.gameObject.SetActive(true);

            ChangeState(GravityFieldState.State2_SelectArea);
        }
    }


    // ----------------------------
    // 状态 2：拖拽矩形区域
    // ----------------------------
    void HandleState2()
    {
        if (isDragging)
        {
            Vector3 nowPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            nowPos.z = 0;

            UpdateSelectionSprite(dragStartPos, nowPos);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragEndPos.z = 0;

            isDragging = false;

            if (selectionSprite != null)
                selectionSprite.gameObject.SetActive(false);

            ChangeState(GravityFieldState.State3_SelectDirection);
        }
    }


    // ----------------------------
    // 使用 SpriteRenderer 作为拉伸矩形选框
    // ----------------------------
    void UpdateSelectionSprite(Vector3 start, Vector3 end)
    {
        if (selectionSprite == null) return;

        Vector3 center = (start + end) * 0.5f;
        selectionSprite.transform.position = center;

        float width = Mathf.Abs(end.x - start.x);
        float height = Mathf.Abs(end.y - start.y);

        selectionSprite.transform.localScale = new Vector3(width, height, 1);
    }


    // ----------------------------
    // 状态 3：方向选择
    // ----------------------------
    void HandleState3()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 dir = mousePos - directionCenter;
            float threshold = 0.1f;

            if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
            {
                if (dir.y > threshold) Debug.Log("方向：上");
                else if (dir.y < -threshold) Debug.Log("方向：下");

                Vector3 gravityFieldPos = (dragStartPos + dragEndPos) / 2;

                GameObject gF = GameObject.Instantiate(gravityField, gravityFieldPos, Quaternion.identity);

                gF.transform.localScale = new Vector3(Mathf.Abs(dragStartPos.x - dragEndPos.x), Mathf.Abs(dragStartPos.y - dragEndPos.y), 1);
               
            }
            else
            {
                if (dir.x > threshold) Debug.Log("方向：右");
                else if (dir.x < -threshold) Debug.Log("方向：左");
                Vector3 gravityFieldPos = (dragStartPos + dragEndPos) / 2;

                GameObject gF = GameObject.Instantiate(gravityField, gravityFieldPos, Quaternion.identity);

                gF.transform.localScale = new Vector3(Mathf.Abs(dragStartPos.x - dragEndPos.x), Mathf.Abs(dragStartPos.y - dragEndPos.y), 1);
            }

            ResetToState1();
        }
    }


    // ----------------------------
    // 状态切换
    // ----------------------------
    void ChangeState(GravityFieldState newState)
    {
        currentState = newState;
        Debug.Log("切换状态：" + newState);

        if (newState == GravityFieldState.State3_SelectDirection)
        {
            // 记录十字中心点
            directionCenter = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            directionCenter.z = 0;

            // 显示方向十字 Sprite
            if (directionCrossSprite != null)
            {
                directionCrossSprite.gameObject.SetActive(true);
                directionCrossSprite.transform.position = directionCenter;
            }
        }
        else
        {
            // 离开 State3 时隐藏十字
            if (directionCrossSprite != null)
                directionCrossSprite.gameObject.SetActive(false);
        }
    }


    void ResetToState1()
    {
        ChangeState(GravityFieldState.State1_Normal);
    }
}


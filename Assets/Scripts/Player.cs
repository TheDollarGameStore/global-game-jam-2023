using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public int gridPosX;

    private bool right = true;
    // Start is called before the first frame update
    void Start()
    {
        gridPosX = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(GameManager.instance.grid[0, gridPosX].transform.position.x, transform.position.y), 15f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gridPosX = Mathf.Max(gridPosX - 1, 0);
            right = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gridPosX = Mathf.Min(5, gridPosX + 1);
            right = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.instance.PlacePiece();
        }

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(right ? 1f : -1f, 1f, 1f), 15f * Time.deltaTime);
    }
}

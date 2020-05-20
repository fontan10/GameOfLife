using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Range(10, 200), SerializeField]
    private int _resolution = 100;

    [Range(1, 60), SerializeField]
    private int framesPerSecond = 60;

    private static IGridManagerable<bool> _gridManager = new MonoColourGridManager();

    // Start is called before the first frame update
    void Start()
    {
        _gridManager.TileSprite = Resources.Load<Sprite>("Sprites/Square");
        _gridManager.Resolution = _resolution;

        _gridManager.CreateGrids();

        StartCoroutine(UpdateGridLoop());
    }

    IEnumerator UpdateGridLoop()
    {
        while(true)
        {
            _gridManager.UpdateGrids();

            float delay = 1f / (float)framesPerSecond;
            yield return new WaitForSeconds(delay);
        }
    }
}

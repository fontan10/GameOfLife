using System.Data.Common;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _resolutionText, _framesPerSecondText;

    private int _resolution = 200;
    private int _framesPerSecond = 60;

    private IGridManagerable[] _gridManagers;
    private IGridManagerable _gridManager;
    private int _gridManagerIndex = 0;

    private int _screenWidth, _screenHeight;

    private float _nextUpdate;
    private bool _isClicking;

    private void Start()
    {
        _nextUpdate = 1f / (float)_framesPerSecond;
        _screenWidth = Camera.main.pixelWidth;
        _screenHeight = Camera.main.pixelHeight;

        _gridManagers = new IGridManagerable[]
        {
            new PolyColourGridManager { TileSprite = Resources.Load<Sprite>("Sprites/Square") },
            new MonoColourGridManager { TileSprite = Resources.Load<Sprite>("Sprites/Square") }
        };

        _gridManager = _gridManagers[_gridManagerIndex];

        Begin();
    }

    private void Update()
    {
        if (Time.time >= _nextUpdate)
        {
            _nextUpdate = Time.time + 1f / _framesPerSecond;
            _gridManager.UpdateGrids();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isClicking = true;
        } else if (Input.GetMouseButtonUp(0))
        {
            _isClicking = false;
        }

        if (_isClicking)
        {
            int cols = _resolution;
            int rows = (int)(cols / Camera.main.aspect);

            float rowHeight = (float)_screenHeight / rows;
            float columnWidth = (float)_screenWidth / cols;

            int clickedRow = (int)(Input.mousePosition.y / rowHeight);
            int clickedCol = (int)(Input.mousePosition.x / columnWidth);

            if(clickedCol < cols &&
                clickedRow < rows &&
                clickedCol > 0 &&
                clickedRow > 0)
            {
                _gridManager.SetAlive(clickedCol, clickedRow);
            }

        }
    }

    public void Begin()
    {
        Time.timeScale = 0;

        _gridManager.Resolution = _resolution;
        _gridManager.CreateGrids();

        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void Restart()
    {
        Time.timeScale = 0;
        Destroy(_gridManager.Tiles);
        Begin();
        Time.timeScale = 1;
    }

    public void UpdateResolution(float resolution)
    {
        _resolutionText.text = ((int)resolution).ToString() + " Resolution";
        Time.timeScale = 0;
        _resolution = (int)resolution;
        Restart();
        Time.timeScale = 1;
    }

    public void UpdateFramesPerSecond(float framesPerSecond)
    {
        Time.timeScale = 0;
        _framesPerSecondText.text = ((int)framesPerSecond).ToString() + " frames / s";
        _framesPerSecond = (int)framesPerSecond;
        Time.timeScale = 1;
    }

    public void ChangeGridManager()
    {
        Time.timeScale = 0;
        Destroy(_gridManager.Tiles);

        _gridManagerIndex = (_gridManagerIndex + 1) % _gridManagers.Length;
        _gridManager = _gridManagers[_gridManagerIndex];

        Begin();
        Time.timeScale = 1;
    }

    public void Clear()
    {
        int cols = _resolution;
        int rows = (int)(cols / Camera.main.aspect);

        for(int i = 0; i < cols; ++i)
        {
            for(int j = 0; j < rows; ++j)
            {
                _gridManager.SetDead(i, j);
            }
        }
    }
}

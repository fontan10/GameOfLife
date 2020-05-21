using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    /// <value> The text above the resolution and frames/s sliders, respectively. </value>
    [SerializeField]
    private TextMeshProUGUI _resolutionText, _framesPerSecondText;

    /// <value> Number of Tiles in the x-axis. </value>
    private int _resolution = 150;
    /// <value> Grids will update on screen this quickly. </value>
    private int _framesPerSecond = 60;

    /// <value> The IGridManagerable the program loops through when the Color button is clicked. </value>
    private IGridManagerable[] _gridManagers;

    /// <value> The current IGridManagerable. </value>
    private IGridManagerable _gridManager;
    private int _gridManagerIndex = 0;

    /// <value> Measured in pixels. </value>
    private int _screenWidth, _screenHeight;

    /// <value> Time for next update measured in seconds. </value>
    private float _nextUpdate;
    /// <value> Is the user clicking? </value>
    private bool _isClicking;



    private void Start()
    {
        _nextUpdate = 1f / (float)_framesPerSecond;
        _screenWidth = Camera.main.pixelWidth;
        _screenHeight = Camera.main.pixelHeight;

        /// <value> Add <c>IGridManagerable</c>s here for to be looped through. </value>
        _gridManagers = new IGridManagerable[]
        {
            new PolyColourGridManager { TileSprite = Resources.Load<Sprite>("Sprites/Square") },
            new MonoColourGridManager { TileSprite = Resources.Load<Sprite>("Sprites/Square") }
        };

        _gridManager = _gridManagers[_gridManagerIndex];

        Begin();
    }

    /// <summary> Called every frame to update the <c>TileSprite</c>s on screen and check for User input. </summary>
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
        }
        else if (Input.GetMouseButtonUp(0))
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

    /// <summary>
    /// Initializes the grids to the correct <c>_resolution</c>
    /// </summary>
    public void Begin()
    {
        Time.timeScale = 0;

        _gridManager.Resolution = _resolution;
        _gridManager.CreateGrids();

        Time.timeScale = 1;
    }

    /// <summary>
    /// Toggles <c>Time.timeScale</c> between 0 and 1
    /// </summary>
    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    /// <summary>
    /// Destroys the Tiles on screen and creates a new grid.
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 0;
        Destroy(_gridManager.Tiles);
        Begin();
        Time.timeScale = 1;
    }

    /// <summary>
    /// Sets <c>_resolution</c> and the Resolution Text displayed on screen.
    /// </summary>
    public void UpdateResolution(float resolution)
    {
        _resolutionText.text = ((int)resolution).ToString() + " Resolution";
        Time.timeScale = 0;
        _resolution = (int)resolution;
        Restart();
        Time.timeScale = 1;
    }

    /// <summary>
    /// Sets <c>_framesPerSecond</c> and the Frames/s Text displayed on screen.
    /// </summary>
    /// <param name="framesPerSecond"></param>
    public void UpdateFramesPerSecond(float framesPerSecond)
    {
        Time.timeScale = 0;
        _framesPerSecondText.text = ((int)framesPerSecond).ToString() + " frames / s";
        _framesPerSecond = (int)framesPerSecond;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Sets <c>_gridManager</c> to the next <c>IGridManagerable</c> in <c>_gridManagers</c>
    /// and resets the Tiles displayed on screen.
    /// </summary>
    public void ChangeGridManager()
    {
        Time.timeScale = 0;
        Destroy(_gridManager.Tiles);

        _gridManagerIndex = (_gridManagerIndex + 1) % _gridManagers.Length;
        _gridManager = _gridManagers[_gridManagerIndex];

        Begin();
        Time.timeScale = 1;
    }

    /// <summary>
    /// Sets all the <c>TileSprite</c>s on screen to Dead.
    /// </summary>
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

using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _resolutionText, _framesPerSecondText;

    private int _resolution = 100;
    private int _framesPerSecond = 60;

    private IGridManagerable[] _gridManagers;
    private IGridManagerable _gridManager;
    private int _gridManagerIndex = 0;

    private void Start()
    {
        _gridManagers = new IGridManagerable[]
        {
            new PolyColourGridManager { TileSprite = Resources.Load<Sprite>("Sprites/Square") },
            new MonoColourGridManager { TileSprite = Resources.Load<Sprite>("Sprites/Square") }
        };

        _gridManager = _gridManagers[_gridManagerIndex];

        Begin();

    }

    public void Begin()
    {
        _gridManager.Resolution = _resolution;
        _gridManager.CreateGrids();

        InvokeRepeating("UpdateGridLoop", 0f, 1f/(float)_framesPerSecond);
    }

    private void UpdateGridLoop()
    {
        _gridManager.UpdateGrids();
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void Restart()
    {
        CancelInvoke("UpdateGridLoop");
        Destroy(_gridManager.Tiles);

        Begin();
    }

    public void UpdateResolution(float resolution)
    {
        _resolutionText.text = ((int)resolution).ToString() + " Resolution";
        _resolution = (int)resolution;
        Restart();
    }

    public void UpdateFramesPerSecond(float framesPerSecond)
    {
        _framesPerSecondText.text = ((int)framesPerSecond).ToString() + " frames / s";
        CancelInvoke("UpdateGridLoop");
        _framesPerSecond = (int)framesPerSecond;
        InvokeRepeating("UpdateGridLoop", 0f, 1f / (float)_framesPerSecond);
    }

    public void ChangeGridManager()
    {
        CancelInvoke("UpdateGridLoop");
        Destroy(_gridManager.Tiles);

        _gridManagerIndex = (_gridManagerIndex + 1) % _gridManagers.Length;
        _gridManager = _gridManagers[_gridManagerIndex];

        Begin();
    }
}

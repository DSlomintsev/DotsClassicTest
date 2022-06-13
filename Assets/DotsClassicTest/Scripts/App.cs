using DotsClassicTest.Board;
using DotsClassicTest.Constants;
using DotsClassicTest.Line;
using DotsClassicTest.Spawner;
using DotsClassicTest.Utils;
using UnityEngine;
using UnityEngine.InputSystem;


namespace DotsClassicTest
{
    public class App : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public static ICoroutineRunner CoroutineRunner;
        public static ISpawner Spawner;

        private BoardPresenter _board;
        private LinePresenter _line;

        private void Awake()
        {
            CoroutineRunner = gameObject.AddComponent<CoroutineRunner>();
            Spawner = new ResourceSpawner();

            var playerInput = Spawner.Spawn<PlayerInput>(PrefabConstants.PlayerInput);

            _board = InitBoard(playerInput);

            _board.FillBoard();

            _line = InitLine(playerInput, new CellLinePointWrapper(_board.SelectedCells));
        }

        private BoardPresenter InitBoard(PlayerInput playerInput)
        {
            return new BoardPresenter
            {
                Camera = mainCamera,
                Config = new BoardConfig(),
                Model = new BoardModel(),
                View = Spawner.Spawn<BoardView>(PrefabConstants.Board),
                Input = new BoardInput(playerInput)
            };
        }

        private LinePresenter InitLine(PlayerInput playerInput, ILinePointWrapper linePointWrapper)
        {
            return new LinePresenter
            {
                Camera = mainCamera,
                Model = new LineModel
                {
                    PointData = linePointWrapper
                },
                View = Spawner.Spawn<LineView>(PrefabConstants.Line),
                Input = new LineInput(playerInput),
            };
        }
    }
}
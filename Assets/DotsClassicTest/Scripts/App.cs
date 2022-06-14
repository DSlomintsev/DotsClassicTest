using System.Collections.Generic;
using DotsClassicTest.Board;
using DotsClassicTest.Constants;
using DotsClassicTest.Line;
using DotsClassicTest.Line.Data;
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
        public static PlayerInput PlayerInput;

        private BoardPresenter _board;
        private LinePresenter _line;
        

        private void Awake()
        {
            InitUtils();

            _board = CreateBoard();
            _board.InitBoard(_board.Config.Rows,_board.Config.Cols);

            _line = CreateLine(new CellLinePointWrapper(_board.SelectedCells));
        }

        private void InitUtils()
        {
            CoroutineRunner = gameObject.AddComponent<CoroutineRunner>();
            Spawner = new ResourceSpawner();
            PlayerInput = Spawner.Spawn<PlayerInput>(PrefabConstants.PlayerInput);
        }

        private BoardPresenter CreateBoard()
        {
            return new BoardPresenter
            {
                Camera = mainCamera,
                Config = new BoardConfig(),
                Model = new BoardModel(),
                View = Spawner.Spawn<BoardView>(PrefabConstants.Board),
                Input = new BoardInput(PlayerInput)
            };
        }

        private LinePresenter CreateLine(ILinePointWrapper linePointWrapper)
        {
            return new LinePresenter
            {
                Camera = mainCamera,
                Model = new LineModel
                {
                    PointData = linePointWrapper
                },
                View = Spawner.Spawn<LineView>(PrefabConstants.Line),
                Input = new LineInput(PlayerInput),
            };
        }
    }
}
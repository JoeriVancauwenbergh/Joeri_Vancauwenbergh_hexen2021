using System.Collections.Generic;
using UnityEngine;
using My.Unity.Commons;
using BoardSystem;
using CardSystem;

//joeri
namespace GameSystem
{
    public class GameLoop : SingletonMonoBehaviour<GameLoop>
    {
        [SerializeField] private PositionHelper _positionHelper;
        [SerializeField] private Configuration _configuration;
        [SerializeField] private Canvas _canvas;


        private Pawn<Tile> _playerPawn;
        public Pawn<Tile> Player => _playerPawn;


        private BaseCard<Pawn<Tile>, Tile> _selectedCard;
        public BaseCard<Pawn<Tile>, Tile> SelectedCard => _selectedCard;


        private int _boardRadius;
        private Grid<Tile> _grid;
        private Board<Pawn<Tile>, Tile> _board;

        private int _deckSize;
        private int _amountCardTypes;
        private PushbackCard _pushbackCard;
        private SlashCard _slashCard;
        private SwipeCard _swipeCard;
        private TeleportCard _teleportCard;
        private Deck<BaseCard<Pawn<Tile>, Tile>, Pawn<Tile>, Tile> _deck;


        void Start()
        {
            _positionHelper.Awake();

            _boardRadius = _configuration.BoardRadius;
            _grid = new Grid<Tile>(_boardRadius);
            _board = new Board<Pawn<Tile>, Tile>();
            ConnectTile();
            ConnectPawn();

            _deckSize = _configuration.DeckSize;
            _amountCardTypes = _configuration.AmountCardTypes;
            _pushbackCard = _configuration.PushbackCard;
            _slashCard = _configuration.SlashCard;
            _swipeCard = _configuration.SwipeCard;
            _teleportCard = _configuration.TeleportCard;
            _deck = new Deck<BaseCard<Pawn<Tile>, Tile>, Pawn<Tile>, Tile>(_board, _grid);

            CreateDeck();
        }

        private void ConnectTile()
        {
            var tiles = FindObjectsOfType<Tile>();
            
            foreach (var tile in tiles)
            {
                var (x, y) = _positionHelper.WorldToGridCoordinates( _grid, tile.transform.localPosition);
                _grid.Register(tile, x, y);

                //DebugTile(tile);
            }
        }

        private void ConnectPawn()
        {
            var pawns = FindObjectsOfType<PawnMonoBehaviour>();
            
            foreach (var pawn in pawns)
            {
                var (x, y) = _positionHelper.WorldToGridCoordinates(_grid, pawn.transform.localPosition);

                if (_grid.TryGetPositionAt(x, y, out var tile))
                {
                    _board.Place(pawn, tile);

                    //DebugPawn(pawn, tile);
                }
            }
        }

        private void CreateDeck()
        {
            for (int i = 0; i < _deckSize; i++)
            {
                switch (Random.Range(0, _amountCardTypes))
                {
                    case 0:
                        var newPushbackCard = Instantiate(_pushbackCard, _canvas.transform.GetChild(0));
                        _deck.Register(newPushbackCard);
                        break;
                    case 1:
                        var newSlashCard = Instantiate(_slashCard, _canvas.transform.GetChild(0));
                        _deck.Register(newSlashCard);
                        break;
                    case 2:
                        var newSwipeCard = Instantiate(_swipeCard, _canvas.transform.GetChild(0));
                        _deck.Register(newSwipeCard);
                        break;
                    case 3:
                        var newTeleportCard = Instantiate(_teleportCard, _canvas.transform.GetChild(0));
                        _deck.Register(newTeleportCard);
                        break;
                }
            }

            _deck.FillHand(_configuration.AmountInHandCards);

            //DebugDeck();
        }

        public void Execute(Tile tile)
            =>_deck.PlayCard(SelectedCard, Player, tile);

        public void Highlight(Tile tile)
        {
            if (tile == null)
                return;
            if (SelectedCard == null)
                return;

            List<Tile> tiles = SelectedCard.Positions(Player, tile);

            foreach (Tile t in tiles)
                if (t != null) t.Highlight = true;
        }

        internal void UnhighlightAll()
        {
            List<Tile> tiles = _grid.GetTiles();

            foreach (Tile t in tiles)
                t.Highlight = false;
        }

        // DEBUG /////////////////////////////////////////////////////////////////////////////////////////////////
        internal void DebugTile(Tile tile)
        {
            var gridCoord = _positionHelper.WorldToGridCoordinates(_grid, tile.transform.localPosition);
            var worldPos = _positionHelper.GridToWorldPosition(_grid, gridCoord.x, gridCoord.y);
            var cubeCoord = _positionHelper.GridToCubeCoordinates(_grid, gridCoord.x, gridCoord.y);
            var cubetoGridCoord = _positionHelper.CubeToGridCoordinates(_grid, cubeCoord.q, cubeCoord.r);
            Debug.Log($"{tile.name} at GC {gridCoord} and WP {worldPos} and CC {cubeCoord} and C2G {cubetoGridCoord}");
        }

        internal void DebugPawn(Pawn<Tile> pawn, Tile tile)
        {
            var pawnWorldPos = pawn.transform.localPosition;
            var tileWorldPos = tile.transform.localPosition;

            Debug.Log($"{pawn.name} at WP {pawnWorldPos} and {tile.name} at WP {tileWorldPos}");
        }
    }
}
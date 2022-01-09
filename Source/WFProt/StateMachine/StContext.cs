using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class StContext
    {
        public Action<PieceName> Out_ShowCurrentPiece;
        public Action<CommandAction> ShowCurrentAction;

        // For DEbug
        private IGuiState _currentSt;
        public IGuiState CurrentState
        {
            get
            {
                return _currentSt;
            }
            set
            {
                string name ;
                name = (null == _currentSt) ? "Null" : _currentSt.ToString();
                Console.WriteLine(string.Format("Current State changed from: {0} to {1}", name, value));

                _currentSt = value;

                // reflejando cambios de estado
                ShowStateChange(_currentSt);
            }
        }


        private void ShowStateChange(IGuiState currentState)
        {
            //    ShowCurrentAction?.Invoke(action);
            ShowCurrentAction?.Invoke((currentState as BaseState).Action);
        }

        //public PieceName CurrentPiece
        //{ get;  set; }

        // For DEbug
        private PieceName _currentPiece;
        public PieceName CurrentPiece
        {
            get
            {
                return _currentPiece;
            }

            set
            {
                //string name;
                //name = (null == _currentPiece) ? "Null" : _currentPiece.ToString();
                Console.WriteLine(string.Format("Current Piece changed from: {0} to {1}", _currentPiece, value));

                _currentPiece = value;

                // reflejando cambios de pieza
                Out_ShowCurrentPiece(_currentPiece);
            }
        }

        internal int NextPieceIndex { get; set; }

        // Entradas, se corresponden a metodos de la interfase
       

        public void ActionDeleteClicked()
        {
            CurrentState.ActionDeleteClicked();
        }

        public void ActionSelectClicked()
        {
            CurrentState.ActionSelectClicked();
        }


        public void PieceButtonClicked(PieceName piece)
        {
            CurrentState.PieceButtonClicked(piece);
        }

        public void NextPieceImageClicked( int index, PieceName piece  = PieceName.None)
        {
            CurrentState.NextPieceImageClicked( index, piece);
        }

        public void GridCellClicked(Coord position)
        {
            CurrentState.GridCellClicked(position);
        }


        // Factory
        public void CreateStates()
        {
    
           
            SelectionState = new SelectionState(this, CommandAction.Select);
            DeletionState = new DeletionState(this, CommandAction.Delete);
            GridCellDeletionState = new GridCellDeletionState(this, CommandAction.Delete);
            NextPieceDeletionState = new NextPieceDeletionState(this, CommandAction.Delete);
            PieceSettingState = new PieceSettingState(this, CommandAction.Select);
            NextPiecePlayState = new NextPiecePlayState(this, CommandAction.Play);
            NextPieceDrawingState = new NextPieceDrawingState(this, CommandAction.Select);
            GridCellDrawingState = new GridCellDrawingState(this, CommandAction.Select);

            CurrentState = SelectionState;
        }

        //public InitialState InitialState { get; private set; }
        public DeletionState DeletionState { get; private set; }
        public SelectionState SelectionState { get; private set; }
        public PieceSettingState PieceSettingState { get; private set; }
        public NextPiecePlayState NextPiecePlayState { get; private set; }
        public NextPieceDrawingState NextPieceDrawingState { get; private set; }
        public GridCellDrawingState GridCellDrawingState { get; private set; }
        public GridCellDeletionState GridCellDeletionState { get; private set; }
        public NextPieceDeletionState NextPieceDeletionState { get; private set; }

        
    
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Conway
{
    public enum BoardState
    {
        Running,
        Stopped,
        Dead
    }

    public class Board : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Fields

        private BoardState state = BoardState.Stopped;
        private System.Windows.Threading.DispatcherTimer timer;
        private Ruleset rules;
        private Dictionary<Cell, List<Cell>> neighborDict;

        #endregion

        #region Commands and Command Delegates        

        private RelayCommand resizeCommand;
        public RelayCommand ResizeCommand { get { return resizeCommand; } }

        private RelayCommand newCommand;
        public RelayCommand NewCommand { get { return newCommand; } }

        private RelayCommand startCommand;
        public RelayCommand StartCommand { get { return startCommand; } }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand { get { return updateCommand; } }

        private RelayCommand clearCommand;
        public RelayCommand ClearCommand { get { return clearCommand; } }        

        private RelayCommand stopCommand;
        public RelayCommand StopCommand
        {
            get
            {
                if (stopCommand == null)
                    stopCommand = new RelayCommand(param => this.StopSimulation(), param => state != BoardState.Stopped);
                return stopCommand;
            }
        }

        private void InitializeCommands()
        {
            startCommand = new RelayCommand(param => this.RunSimulation());
            updateCommand = new RelayCommand(param => this.UpdateSimulation());
            clearCommand = new RelayCommand(param => this.ClearSimulation());
            newCommand = new RelayCommand(param => this.CreateNewBoard());
            resizeCommand = new RelayCommand(this.CreateNewBoard, p => state != BoardState.Running);
        }

        private void ClearSimulation()
        {
            foreach (Cell cell in cellBoard)
            {
                cell.IsAlive = false;
            }

            state = BoardState.Dead;
        }

        private void RunSimulation()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += UpdateSimulation;
            timer.Start();
            state = BoardState.Running;
        }

        private void StopSimulation()
        {
            timer.Stop();
            state = BoardState.Stopped;
        }

        private void CreateNewBoard(object dimension)
        {
            int size = Convert.ToInt32(dimension);
            CreateNewBoard(size, size);           
        }

        #endregion

        #region Properties

        public ObservableCollection<Cell> cellBoard { get; set; }

        private int rows = 100;
        public int Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                OnPropertyChanged();
            }
        }
            
        private int columns = 100;
        public int Columns
        {
            get { return columns; }
            set
            {
                columns = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor/helpers

        public Board()
        {
            cellBoard = new ObservableCollection<Cell>();
            rules = new Ruleset(new List<int> { 3 }, new List<int> { 1, 2, 3, 4, 5 });
            CreateNewBoard();
            InitializeCommands();            
        }

        private void CreateNewBoard()
        {
            CreateNewBoard(Rows, Columns);
        }
          
        private void CreateNewBoard(int _rows, int _columns)
        {
            Rows = _rows;
            Columns = _columns;

            cellBoard.Clear();

            for (int i = 0; i < Rows * Columns; i++)
            {
                cellBoard.Add(new Cell(false));
            }
            InitializeNeighborDictionary();
        }

        #endregion

        #region UpdateBoard

        private void UpdateSimulation()
        {            
            for (int i = 0; i < Rows*Columns; i++)
            {
                Cell cell = cellBoard[i];
                cell.AliveCount = AliveCount(cell);
            }

            for (int i = 0; i < Rows * Columns; i++)
            {
                Cell cell = cellBoard[i];
                
                if (cell.IsAlive == false && rules.BirthRules.Contains(cell.AliveCount))
                    cell.IsAlive = true;                
                else if (cell.IsAlive == true && (!rules.SurviveRules.Contains(cell.AliveCount)))
                    cell.IsAlive = false;
            }

            Debug.WriteLine(rules);
        }               

        private void UpdateSimulation(object sender, EventArgs e)
        {
            UpdateSimulation();
        }

        private byte AliveCount(Cell cell)
        {
            byte aliveCount = 0;
            foreach (Cell neighbor in neighborDict[cell])
            {
                if (neighbor.IsAlive == true)
                    aliveCount++;
            }

            return aliveCount;
        }

        #endregion

        #region DictionaryInitialization

        private void InitializeNeighborDictionary()
        {
            neighborDict = new Dictionary<Cell, List<Cell>>();
            for (int i = 0; i < Rows*Columns; i++)
            {
                Cell cell = cellBoard[i];
                neighborDict[cell] = GetNeighbors(i);
            }
        }

        private List<Cell> GetNeighbors(int index)
        {
            var neighborList = new List<Cell>();

            int row = index / Columns;
            int col = index % Columns;

            for (int rowDisp = -1; rowDisp <= 1; rowDisp++)
            {
                for (int colDisp = -1; colDisp <= 1; colDisp++)
                {
                    if (rowDisp != 0 || colDisp != 0)
                    {
                        int adjRow = row + rowDisp;
                        int adjCol = col + colDisp;

                        if (adjRow >= 0 && adjRow < Rows && adjCol >= 0 && adjCol < Columns)
                            neighborList.Add(cellBoard[RowColToIndex(adjRow, adjCol)]);
                    }
                }
            }

            return neighborList;
        }   
        
        private int RowColToIndex(int row, int col)
        {
            return row * Columns + col;
        }

        #endregion
    }
}

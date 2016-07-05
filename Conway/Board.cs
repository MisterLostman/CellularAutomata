using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Conway
{
    public class Board : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public ObservableCollection<Cell> cellBoard { get; set; }        
        public Dictionary<Cell, List<Cell>> neighborDict;

        private int rows = 50;
        public int Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                OnPropertyChanged();
            }
        }
            
        private int columns = 50;
        public int Columns
        {
            get { return columns; }
            set
            {
                columns = value;
                OnPropertyChanged();
            }
        }
          
        
        public void CreateNewBoard()
        {
            CreateNewBoard(Rows, Columns);
        }
          
        public void CreateNewBoard(int _rows, int _columns)
        {
            Rows = _rows;
            Columns = _columns;

            cellBoard = new ObservableCollection<Cell>();

            for (int i = 0; i < Rows * Columns; i++)
            {
                cellBoard.Add(new Cell(false));
            }
            InitializeNeighborDictionary();
        }

        public void Clear()
        {
            foreach (Cell cell in cellBoard)
            {
                cell.IsAlive = false;
            }
        }

        #region UpdateBoard

        public void Update()
        {            
            for (int i = 0; i < Rows*Columns; i++)
            {
                Cell cell = cellBoard[i];
                cell.AliveCount = AliveCount(cell);
            }

            for (int i = 0; i < Rows * Columns; i++)
            {
                Cell cell = cellBoard[i];
                if (cell.IsAlive == false)
                {
                    if (cell.AliveCount == 3)
                        cell.IsAlive = true;
                    else
                        cell.IsAlive = false;
                }
                else
                {
                    if (cell.AliveCount > 3 || cell.AliveCount < 2)
                        cell.IsAlive = false;
                    else
                        cell.IsAlive = true;
                }
            }            
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

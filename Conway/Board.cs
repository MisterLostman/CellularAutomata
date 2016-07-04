using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conway
{
    public class Board
    {
        public ObservableCollection<Cell> cellBoard { get; set; }        
        public Dictionary<Cell, List<Cell>> neighborDict;

        public int Rows { get; set; } = 50;
        public int Columns { get; set; } = 50;
          
        
        public void CreateNewBoard()
        {
            CreateNewBoard(Rows, Columns);
        }
          
        public void CreateNewBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            cellBoard = new ObservableCollection<Cell>();

            for (int i = 0; i < rows * columns; i++)
            {
                cellBoard.Add(new Cell(false));
            }
            InitializeNeighborDictionary();
        }      

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

        public void Update(object source, System.Timers.ElapsedEventArgs e)
        {
            this.Update();
        }

        public byte AliveCount(Cell cell)
        {
            byte aliveCount = 0;
            foreach (Cell neighbor in neighborDict[cell])
            {
                if (neighbor.IsAlive == true)
                    aliveCount++;
            }

            return aliveCount;
        }

        public void UpdateTest()
        {
            foreach(Cell cell in cellBoard)
            {
                cell.Flip();
            }
        }

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
            List<Cell> neighborList = new List<Cell>();

            int row = index / Columns;
            int col = index % Columns;

            bool bottomEdge = (row >= Rows - 1);
            bool rightEdge = (col >= Columns - 1);
            bool topEdge = (row <= 0);
            bool leftEdge = (col <= 0);

            if (bottomEdge == false)
            {
                neighborList.Add(cellBoard[RowColToIndex(row + 1, col)]);
                if (rightEdge == false)
                    neighborList.Add(cellBoard[RowColToIndex(row + 1, col + 1)]);
                else
                    neighborList.Add(cellBoard[RowColToIndex(row + 1, 0)]);

                if (leftEdge == false)
                    neighborList.Add(cellBoard[RowColToIndex(row + 1, col - 1)]);
                else
                    neighborList.Add(cellBoard[RowColToIndex(row + 1, Columns - 1)]);
            }
            else
            {
                neighborList.Add(cellBoard[RowColToIndex(0, col)]);
                if (rightEdge == false)
                    neighborList.Add(cellBoard[RowColToIndex(0, col + 1)]);
                else
                    neighborList.Add(cellBoard[RowColToIndex(0, 0)]);

                if (leftEdge == false)
                    neighborList.Add(cellBoard[RowColToIndex(0, col - 1)]);
                else
                    neighborList.Add(cellBoard[RowColToIndex(0, Columns - 1)]);
            }


            if (topEdge == false)
            {
                neighborList.Add(cellBoard[RowColToIndex(row - 1, col)]);
                if (leftEdge == false)
                    neighborList.Add(cellBoard[RowColToIndex(row - 1, col - 1)]);
                else
                    neighborList.Add(cellBoard[RowColToIndex(row - 1, Columns - 1)]);

                if (rightEdge == false)
                    neighborList.Add(cellBoard[RowColToIndex(row - 1, col + 1)]);
                else
                    neighborList.Add(cellBoard[RowColToIndex(row - 1, 0)]);
            }
            else
            {
                neighborList.Add(cellBoard[RowColToIndex(Rows - 1, col)]);
                if (leftEdge == false)
                    neighborList.Add(cellBoard[RowColToIndex(Rows - 1, col - 1)]);
                else
                    neighborList.Add(cellBoard[RowColToIndex(Rows - 1, Columns - 1)]);

                if (rightEdge == false)
                    neighborList.Add(cellBoard[RowColToIndex(Rows - 1, col + 1)]);
                else
                    neighborList.Add(cellBoard[RowColToIndex(Rows - 1, 0)]);
            }

            if (rightEdge == false)
                neighborList.Add(cellBoard[RowColToIndex(row, col + 1)]);
            else
                neighborList.Add(cellBoard[RowColToIndex(row, 0)]);

            if (leftEdge == false)
                neighborList.Add(cellBoard[RowColToIndex(row, col - 1)]);
            else
                neighborList.Add(cellBoard[RowColToIndex(row, Columns - 1)]);

            return neighborList;
        }

        private int RowColToIndex(int row, int col)
        {
            return row * Columns + col;
        }

        #endregion
    }
}

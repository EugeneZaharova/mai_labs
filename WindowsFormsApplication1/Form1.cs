using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GraphOrlov GRAPHS;

        private void label1_Click(object sender, EventArgs e)
        {

        }       

        private void CreateButton_Click(object sender, EventArgs e)
        {
            int verCount = (int) VerticInsertsNum.Value;

            if (verCount < 1)
                return;

            GRAPHS = new GraphOrlov(verCount);

            FillTable();
            GraphCanvas.Invalidate();
        }

        private void FillTable()
        {
            TableOfAdjacency.Columns.Clear();

            foreach (Vertex ver in GRAPHS.Vertexes)
            {
                DataGridViewColumn column = new DataGridViewCheckBoxColumn();
                column.Name = ver.Name;

                TableOfAdjacency.Columns.Add(column);
            }

            foreach (Vertex ver in GRAPHS.Vertexes)
            {
                var row = new DataGridViewRow();
                row.CreateCells(TableOfAdjacency);
                row.HeaderCell.Value = ver.Name;

                TableOfAdjacency.Rows.Add(row);
            }

            TableOfAdjacency.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void GraphCanvas_Paint(object sender, PaintEventArgs e)
        {
            GRAPHS?.Render(e.Graphics); //хитровыебанная проверка на не нулл
        }

        private void TableOfAdjacency_CurrentCellChanged(object sender, EventArgs e)
        {

            var value = TableOfAdjacency.Rows[TableOfAdjacency.CurrentCell.RowIndex].Cells[TableOfAdjacency.CurrentCell.ColumnIndex].Value;
            bool b = value != null && (bool)value;

            if (b)
                return;

            string name1 = TableOfAdjacency.Columns[TableOfAdjacency.CurrentCell.ColumnIndex].Name;

            var ver1 = GRAPHS.Vertexes.FirstOrDefault(v => v.Name == name1);

            if (ver1 == null)
                return;

            string name2 = (string)TableOfAdjacency.Rows[TableOfAdjacency.CurrentCell.RowIndex].HeaderCell.Value;

            var ver2 = GRAPHS.Vertexes.FirstOrDefault(v => v.Name == name2);

            if (ver2 == null)
                return;

            GRAPHS.Edges.Add(new Edge(ver1, ver2));

            GraphCanvas.Invalidate();
        }

        private void UnderSetFounder_Click(object sender, EventArgs e)
        {
            if (GRAPHS == null) return;

            int c = GRAPHS.Vertexes.Count;

            bool[,] bools = new bool[c,c];

            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    var value = TableOfAdjacency.Rows[i].Cells[j].Value;
                    if (value != null)
                        bools[i, j] = (bool) value;
                }
            }

            resTB.Text = GetFunctionFromTable(bools);

        }


        private string GetFunctionFromTable(bool[,] matrix)
        {
            string res="";
            var size = matrix.GetLength(0);
            
            for (int i = 0; i < size; i++) // вынести отдельной функцией
            {
                matrix[i, i] = true;
            }

            for (int i = 0; i < size; i++)
            {
                bool first = false;
                res += '(';

                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j])
                    {
                        if (!first)
                        {
                            res += "X" + (j + 1);
                            first = true;
                            continue;
                        }
                        res += " V X" + (j + 1);
                    }
                }

                if (size - i == 1)
                {
                    res += ')';
                    continue;
                }
                res += ") & ";
            }

            for (int i = 0; i < size; i++)
            {
                int r = 0;
                int c = 0;

                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j])
                        ++c;
                    if (matrix[j, i])
                        ++r;
                }

                if (r == 1 && c == 1)
                {
                    res = "Нет таких, т.к. существует вершина не принадлежащая графу";
                    break;
                }
            }

            return res;
        }

        Vertex current = null;
        bool movemode = false;
        int deltax = 0;
        int prevx = 0;
        int deltay = 0;
        int prevy = 0;
        private void GraphCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            int R = 15;

            var Vertexes = GRAPHS.Vertexes;
            foreach (Vertex vertex in Vertexes)
            {
                var mouserec = new Rectangle(e.X - 1, e.Y -1, 2, 2);
                var rec = new Rectangle(vertex.x - R, vertex.y - R, R * 2, R * 2);
                if(rec.Contains(mouserec)) 
                {
                    current = vertex;
                    movemode = true;
                    deltax = 0;
                    prevx = e.X;
                    deltay = 0;
                    prevy = e.Y;
                    break;
                }
            }
        }

        private void GraphCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            movemode = false;
            current = null;
        }

        private void GraphCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!movemode) return;
            if (current == null) return;

            deltax = e.X - prevx;
            deltay = e.Y - prevy;

            prevx = e.X;
            prevy = e.Y;

            current.x += deltax;
            current.y += deltay;

            GraphCanvas.Invalidate();
        }

        private void TableOfAdjacency_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
}

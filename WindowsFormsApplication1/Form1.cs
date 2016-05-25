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

            DataGridViewColumn column;
            DataGridViewRow row;

            foreach (Vertex ver in GRAPHS.Vertexes)
            {
                column = new DataGridViewCheckBoxColumn();
                column.Name = ver.Name;

                TableOfAdjacency.Columns.Add(column);
            }

            foreach (Vertex ver in GRAPHS.Vertexes)
            {
                row = new DataGridViewRow();
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

        private void TableOfAdjacency_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void TableOfAdjacency_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void TableOfAdjacency_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }

        private void TableOfAdjacency_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TableOfAdjacency_CurrentCellChanged(object sender, EventArgs e)
        {

            var value = TableOfAdjacency.Rows[TableOfAdjacency.CurrentCell.RowIndex].Cells[TableOfAdjacency.CurrentCell.ColumnIndex].Value;
            bool b = value != null && (bool)value;

            if (b) return;

            string name1 = TableOfAdjacency.Columns[TableOfAdjacency.CurrentCell.ColumnIndex].Name;

            var ver1 = GRAPHS.Vertexes.FirstOrDefault(v => v.Name == name1);
            if (ver1 == null) return;

            string name2 = (string)TableOfAdjacency.Rows[TableOfAdjacency.CurrentCell.RowIndex].HeaderCell.Value;

            var ver2 = GRAPHS.Vertexes.FirstOrDefault(v => v.Name == name2);
            if (ver2 == null) return;

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

            return res;
        }
    }
}

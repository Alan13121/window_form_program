using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hw2
{
    public partial class MyDrawing_Form : Form
    {
        List<Shape> shapes = new List<Shape>();
        Model model = new Model();
        Panel Canva = new DoubleBufferedPanel();
        PresentationModel.PresentationModel _presentationModel;
        public MyDrawing_Form()
        {
            InitializeComponent();
            Canva.Location = new Point(180, 49);
            //Canva.BackColor = Color.Red;
            Canva.Width = 568;
            Canva.Height = 513;
            Canva.MouseDown += Canva_MouseDown;
            Canva.MouseUp += Canva_MouseUp;
            Canva.MouseMove += Canva_MouseMove;
            Canva.Paint += Canva_Paint;
            _presentationModel = new PresentationModel.PresentationModel(model, Canva);
            model._modelChanged += HandleModelChanged;
            Controls.Add(Canva);
        }

        private void add_shape_buttom_Click(object sender, EventArgs e)
        {
            string[] new_shape = { shape_type_comboBox.Text, shape_text_textBox.Text, shape_x_textBox.Text, shape_y_textBox.Text, shape_height_textBox.Text, shape_width_textBox.Text };
            List<int> numerical_data = new List<int>();

            if (new_shape[0] == "形狀")
            {
                MessageBox.Show("請選擇形狀");
                return;
            }
            else
            {

                for (int i = 1; i < 6; i++)
                {
                    if (new_shape[i] == "")
                    {
                        MessageBox.Show("請輸入資料");
                        return;
                    }

                    if (i == 1) continue;
                    else if (int.TryParse(new_shape[i], out var result))
                    {
                        numerical_data.Add(result);
                    }
                    else
                    {
                        MessageBox.Show("輸入錯誤(請勿在X,Y,H,W中輸入「非數字字元」)");
                        return;
                    }
                }

            }

            Canva.Invalidate();
            model.enter_new_shape(new_shape);

            List<Shape> shapelist = model.GetShapes();
            shape_info_dataGridView.Rows.Clear();
            for (int i = 0; i < shapelist.Count; i++)
            {
                shape_info_dataGridView.Rows.Add("刪除", shapelist[i].ID, shapelist[i].ShapeName, shapelist[i].text, shapelist[i].X, shapelist[i].Y, shapelist[i].Height, shapelist[i].Width);
            }
        }

        private void shape_info_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)  // 確認點擊的行有效
            {
                int shapeID = int.Parse(shape_info_dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                model.remove_shape(shapeID);
                shape_info_dataGridView.Rows.Remove(shape_info_dataGridView.Rows[e.RowIndex]);
                Canva.Invalidate();
            }

        }
        //Invalidate(true);
        public void HandleModelChanged()
        {
            Canva.Invalidate();
        }


        private void Canva_Paint(object sender, PaintEventArgs e)
        {
            var shapeList = model.GetShapes();
            _presentationModel.Draw(e.Graphics);
            shape_info_dataGridView.Rows.Clear();
            for (int i = 0; i < shapeList.Count; i++)
            {
                shape_info_dataGridView.Rows.Add("刪除", shapeList[i].ID, shapeList[i].ShapeName, shapeList[i].text, shapeList[i].X, shapeList[i].Y, shapeList[i].Height, shapeList[i].Width);

            }


        }
        private void Canva_MouseDown(object sender, MouseEventArgs e)
        {
            
            model.PointerPressed(e.X, e.Y);

        }
        private void Canva_MouseUp(object sender, MouseEventArgs e)
        {
            model.PointerReleased(e.X, e.Y);

            ResetToolButtonColors();
            Canva.Cursor = Cursors.Default;
        }

        private void Canva_MouseMove(object sender, MouseEventArgs e)
        {
            
            model.PointerMoved(e.X, e.Y);

        }

        private void ResetToolButtonColors()
        {
            StartToolButton.BackColor = Color.White;
            TerminatorToolButton.BackColor = Color.White;
            ProcessToolButton.BackColor = Color.White;
            DecisionToolButton.BackColor = Color.White;
        }

        private void SetToolButtonSelected(System.Windows.Forms.ToolStripButton button, int shapeType)
        {
            ResetToolButtonColors();
            model.SetType(shapeType);
            button.BackColor = Color.DodgerBlue;
            Canva.Cursor = Cursors.Cross;
        }

        private void StartToolButton_Click(object sender, EventArgs e)
        {
            SetToolButtonSelected(StartToolButton, 0);
        }

        private void TerminatorToolButton_Click(object sender, EventArgs e)
        {
            SetToolButtonSelected(TerminatorToolButton, 1);
        }

        private void ProcessToolButton_Click(object sender, EventArgs e)
        {
            SetToolButtonSelected(ProcessToolButton, 2);
        }

        private void DecisionToolButton_Click(object sender, EventArgs e)
        {
            SetToolButtonSelected(DecisionToolButton, 3);
        }


        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            ResetToolButtonColors();
            Canva.Cursor = Cursors.Default;
        }
    }
}

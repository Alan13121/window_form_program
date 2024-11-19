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
            add_shape_buttom.DataBindings.Add("Enabled", _presentationModel, "isAddButtonEnabled");
            add_shape_buttom.Enabled = false;
        }


        private void add_shape_buttom_Click(object sender, EventArgs e)
        {
            string[] new_shape = { shape_type_comboBox.Text, shape_text_textBox.Text, shape_x_textBox.Text, shape_y_textBox.Text, shape_height_textBox.Text, shape_width_textBox.Text };
           
           

            Canva.Invalidate();
            model.enter_new_shape(new_shape);

            List<Shape> shapelist = model.GetShapes();
            shape_info_dataGridView.Rows.Clear();
            for (int i = 0; i < shapelist.Count; i++)
            {
                shape_info_dataGridView.Rows.Add("刪除", shapelist[i].ID, shapelist[i].ShapeName, shapelist[i].Text, shapelist[i].X, shapelist[i].Y, shapelist[i].Height, shapelist[i].Width);
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
                shape_info_dataGridView.Rows.Add("刪除", shapeList[i].ID, shapeList[i].ShapeName, shapeList[i].Text, shapeList[i].X, shapeList[i].Y, shapeList[i].Height, shapeList[i].Width);

            }


        }
        private void Canva_MouseDown(object sender, MouseEventArgs e)
        {
            
            model.PointerPressed(e.X, e.Y);

        }
        private void Canva_MouseUp(object sender, MouseEventArgs e)
        {
            model.PointerReleased(e.X, e.Y);

            //ResetToolButtonColors();
            Canva.Cursor = Cursors.Default;
            StartToolButton.Checked = false;
            TerminatorToolButton.Checked = false;
            DecisionToolButton.Checked = false;
            ProcessToolButton.Checked = false;
        }

        private void Canva_MouseMove(object sender, MouseEventArgs e)
        {
            
            model.PointerMoved(e.X, e.Y);

        }

        /*private void ResetToolButtonColors()
        {
            StartToolButton.BackColor = Color.White;
            TerminatorToolButton.BackColor = Color.White;
            ProcessToolButton.BackColor = Color.White;
            DecisionToolButton.BackColor = Color.White;
        }*/

        private void SetToolButtonSelected(System.Windows.Forms.ToolStripButton button, int shapeType)
        {
            model.ChangeToDrawingState();
            model.SetType(shapeType);
            //Canva.Cursor = Cursors.Cross;
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
            Canva.Cursor = Cursors.Cross;

            // 從事件參數中取得被點擊的按鈕
            ToolStripButton clickedButton = e.ClickedItem as ToolStripButton;

            // 如果 clickedButton 為 null，則表示點擊的項目不是按鈕，直接返回
            if (clickedButton == null)
            {
                return;
            }

            // 遍歷所有 ToolStripItem，將所有按鈕的 Checked 設為 false
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item is ToolStripButton button)
                {
                    button.Checked = false;
                }
            }

            // 將當前點擊的按鈕 Checked 設為 true
            clickedButton.Checked = true;
        }

        private void shape_type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _presentationModel.ShapeComboBoxIndexChanged(shape_type_comboBox.SelectedIndex);
            _presentationModel.AllAttributeFilled();
        }

        private void shape_text_textBox_TextChanged(object sender, EventArgs e)
        {
            _presentationModel.TextTextBoxTextChanged(shape_text_textBox.Text);
            _presentationModel.AllAttributeFilled();
        }

        private void shape_x_textBox_TextChanged(object sender, EventArgs e)
        {
            _presentationModel.XTextBoxTextChanged(shape_x_textBox.Text);
            _presentationModel.AllAttributeFilled();
        }

        private void shape_y_textBox_TextChanged(object sender, EventArgs e)
        {
            _presentationModel.YTextBoxTextChanged(shape_y_textBox.Text);
            _presentationModel.AllAttributeFilled();
        }

        private void shape_height_textBox_TextChanged(object sender, EventArgs e)
        {
            _presentationModel.HTextBoxTextChanged(shape_height_textBox.Text);
            _presentationModel.AllAttributeFilled();
        }

        private void shape_width_textBox_TextChanged(object sender, EventArgs e)
        {
            _presentationModel.WTextBoxTextChanged(shape_width_textBox.Text);
            _presentationModel.AllAttributeFilled();
        }
    }
}

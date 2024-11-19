using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hw2.PresentationModel
{
    internal class PresentationModel : INotifyPropertyChanged
    {
        Model model;
        public event PropertyChangedEventHandler PropertyChanged;
        bool isAddButtonEnabled = false;
        bool isShapeComboBoxIndexChanged, isTextTextBoxChanged, isXTextBoxChanged, isYTextBoxChanged, isHTextBoxChanged, isWTextBoxChanged;

        public PresentationModel(Model model, Control canvas)
        {
            this.model = model;
            isShapeComboBoxIndexChanged = isTextTextBoxChanged = isXTextBoxChanged = isYTextBoxChanged = isHTextBoxChanged = isWTextBoxChanged = false;

        }
        public void TextTextBoxTextChanged(string text)
        {
            if (text != "")
                isTextTextBoxChanged = true;
            else
                isTextTextBoxChanged = false;
        }
        public void XTextBoxTextChanged(string text)
        {
            if (text != "")
                isXTextBoxChanged = true;
            else
                isXTextBoxChanged = false;
        }
        public void YTextBoxTextChanged(string text)
        {
            if (text != "")
                isYTextBoxChanged = true;
            else
                isYTextBoxChanged = false;
        }
        public void HTextBoxTextChanged(string text)
        {
            if (text != "")
                isHTextBoxChanged = true;
            else
                isHTextBoxChanged = false;
        }
        public void WTextBoxTextChanged(string text)
        {
            if (text != "")
                isWTextBoxChanged = true;
            else
                isWTextBoxChanged = false;
        }
        public void ShapeComboBoxIndexChanged(int index)
        {
            if (index != -1)
                isShapeComboBoxIndexChanged = true;
            else
                isShapeComboBoxIndexChanged = false;
        }
        public void AllAttributeFilled()
        {
            isAddButtonEnabled = isShapeComboBoxIndexChanged && isTextTextBoxChanged && isXTextBoxChanged && isYTextBoxChanged && isHTextBoxChanged && isWTextBoxChanged;
            //Console.WriteLine("isAddButtonEnabled:" + isAddButtonEnabled);
            notify("isAddButtonEnabled");
        }
        public bool IsAddButtonEnabled
        {
            get
            {
                return isAddButtonEnabled;
            }
        }
        private void notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Draw(System.Drawing.Graphics graphics)
        {
            model.Draw(new WindowsFormsGraphicsAdaptor(graphics));
        }
    }
}

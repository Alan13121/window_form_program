using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hw2.PresentationModel
{
    internal class PresentationModel
    {
        Model model;
        public PresentationModel(Model model, Control canvas)
        {
            this.model = model;
        }
        public void Draw(System.Drawing.Graphics graphics)
        {
            model.Draw(new WindowsFormsGraphicsAdaptor(graphics));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Input;
namespace StateMachineNodeEditor
{ 
   public class Editor:Grid
    {
        private Point? _movePoint;
        private double zoom = 1;
        private double scale = 0.05;
        private double MaxScale = 3;
        private double MinScale = 0.1;
        ScaleTransform t;
        public Editor()
        {
            //MouseWheel
            TransformGroup transformGroup = new TransformGroup();
            ScaleTransform scaleTransform = new ScaleTransform();
            Binding bind1 = new Binding();
            bind1.Path = new PropertyPath("ScaleX");
           // scaleTransform.ScaleX = bind1;
            TranslateTransform translateTransform = new TranslateTransform();
            translateTransform.X = 0;
            translateTransform.Y = 0;
            t = scaleTransform;
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
            this.RenderTransform = transformGroup;
            base.MouseMove += MouseMove;
          //  this.DataContext += zoom;
            t.ScaleX -= 0.5;
            // this.RenderTransform
        }
        private void MouseMove(object sender, MouseEventArgs e)
        {
            t.ScaleX -= 0.5;
        }
        private TransformGroup CreateTransformGroup()
        {
            TransformGroup transformGroup = new TransformGroup();
            return null;
        }
        public double ScaleX
        {
            get; set;
        }
        public double ScaleY
        {
            get; set;
        }
        public double TransformX
        {
            get; set;
        }
        public double TransformY
        {
            get; set;
        }
    }
}

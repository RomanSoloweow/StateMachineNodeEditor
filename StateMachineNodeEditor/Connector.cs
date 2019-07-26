using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace StateMachineNodeEditor
{

   public class Connector:Control
    {
        public Path path = new Path(); 
        public PathGeometry pathGeometry = new PathGeometry();       
        public PathFigure pathFigure = new PathFigure();
        public BezierSegment bezierSegment = new BezierSegment();
        public Connector()
        {
        
            pathFigure.Segments.Add(bezierSegment);
            pathGeometry.Figures.Add(pathFigure);
            path.Data = pathGeometry;
           path.Stroke = Brushes.White;
          pathFigure.StartPoint = new Point(0, 0); ;
            bezierSegment.Point1 = new Point(100, 100);
          bezierSegment.Point2 = new Point(100, 100);
        bezierSegment.Point3 = new Point(500, 500);
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }
}

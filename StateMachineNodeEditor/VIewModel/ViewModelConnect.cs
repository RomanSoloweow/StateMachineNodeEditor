using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReactiveUI.Fody.Helpers;
using StateMachineNodeEditor.Helpers;
using ReactiveUI;
using ReactiveUI.Wpf;
using DynamicData;
using System.Windows;
using System.Windows.Media;

namespace StateMachineNodeEditor.ViewModel
{
    public class ViewModelConnect:ReactiveObject
    {
        /// <summary>
        /// Точка, из которой выходит линия ( совпадает с центром элемента, из которого выходит линия)
        /// </summary>
        [Reactive] public MyPoint StartPoint { get; set; } = new MyPoint();

        /// <summary>
        /// Точка, в которую приходит линия ( совпадает с центром элемента, в который приходит линия)
        /// </summary>
        [Reactive] public MyPoint EndPoint { get; set; } = new MyPoint();

        /// <summary>
        /// Первая промежуточная точка линии 
        /// </summary>
        [Reactive] public MyPoint Point1 { get; set; } = new MyPoint();

        /// <summary>
        /// Вторая промежуточная точка линии
        /// </summary>
        [Reactive] public MyPoint Point2 { get; set; } = new MyPoint();

        /// <summary>
        /// Цвет линии
        /// </summary>
        [Reactive] public Brush Stroke { get; set; } = Brushes.White;

        /// <summary>
        /// Элемент, из которого выходит линия
        /// </summary>
        public ViewModelConnector FromConnector;

        /// <summary>
        /// Элемент, в который приходит линия
        /// </summary>
        public ViewModelConnector ToConnector;

        public ViewModelConnect(ViewModelConnector fromConnector)
        {
            FromConnector = fromConnector;
            this.WhenAnyValue(x => x.StartPoint.Value, x => x.EndPoint.Value).Subscribe(_ => Update());
            this.WhenAnyValue(x => x.FromConnector.Position.Value).Subscribe(value => StartPoint.Set(value));
            this.WhenAnyValue(x => x.ToConnector.Position.Value).Subscribe(value => EndPoint.Set(value));
        }
        private void Update()
        {
            MyPoint different = EndPoint - StartPoint;
            Point1.Set(StartPoint.X + 3 * different.X / 8, StartPoint.Y + 1 * different.Y / 8);
            Point2.Set(StartPoint.X + 5 * different.X / 8, StartPoint.Y + 7 * different.Y / 8);
        }
    }
}

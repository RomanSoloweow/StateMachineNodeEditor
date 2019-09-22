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
        [Reactive] public Point StartPoint { get; set; }

        /// <summary>
        /// Точка, в которую приходит линия ( совпадает с центром элемента, в который приходит линия)
        /// </summary>
        [Reactive] public Point EndPoint { get; set; }

        /// <summary>
        /// Первая промежуточная точка линии (считается автоматически)
        /// </summary>
        [Reactive] public Point Point1 { get; set; }

        /// <summary>
        /// Вторая промежуточная точка линии (считается автоматически)
        /// </summary>
        [Reactive] public Point Point2 { get; set; }

        /// <summary>
        /// Цвет линии
        /// </summary>
        [Reactive] public Brush Stroke { get; set; }

        /// <summary>
        /// Элемент, из которого выходит линия
        /// </summary>
        public ViewModelConnector FromConnector;

        /// <summary>
        /// Элемент, в который приходит линия
        /// </summary>
        public ViewModelConnector ToConnector;
    }
}

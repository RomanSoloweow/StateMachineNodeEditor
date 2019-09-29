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
using System.Windows.Media;
using System.Windows;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
namespace StateMachineNodeEditor.ViewModel
{
    public class ViewModelSelector: ReactiveObject
    {
        /// <summary>
        /// Размер Селектора
        /// </summary>
        [Reactive] public Size Size { get; set; }

        /// <summary>
        /// Отображается ли выделение
        /// </summary>
        [Reactive] public bool? Visible { get; set; } = true;

        /// <summary>
        /// Точка левого верхнего угла
        /// </summary>
        [Reactive] public MyPoint Point1 { get; set; } = new MyPoint();

        /// <summary>
        /// Точка нижнего правого угла
        /// </summary>
        [Reactive] public MyPoint Point2 { get; set; } = new MyPoint();

        /// <summary>
        /// Масштаб
        /// </summary>
        [Reactive] public Scale Scale { get; set; } = new Scale();

        public ViewModelSelector()
        {
            this.WhenAnyValue(x => x.Point1.Value, x => x.Point2.Value).Subscribe(_ => UpdateSize());
            this.WhenAnyValue(x => x.Point1.Value).Subscribe(vm => Scale.Center.Set(vm));

        }

        private void UpdateSize()
        {
            MyPoint different = Point1 - Point2;

            Size = new Size(Math.Abs(different.X), Math.Abs(different.Y));

            //Если нужно отражаем по X и/или Y 
            Scale.Scales.Set((different.X > 0) ? 1 : -1, (different.Y > 0) ? 1 : -1);
        }

        #region Setup Commands
        #endregion Setup Commands
    }
}

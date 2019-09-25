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
        /// Ширина
        /// </summary>
        [Reactive] public double Width { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        [Reactive] public double Height { get; set; }

        /// <summary>
        /// Отображается ли выделение
        /// </summary>
        [Reactive] public bool Visible { get; set; } = true;

        /// <summary>
        /// Перенос (Координата от левого верхнего угла)
        /// </summary>
        [Reactive] public Translate Translate { get; set; } = new Translate();

        /// <summary>
        /// Масштаб
        /// </summary>
        [Reactive] public Scale Scale { get; set; } = new Scale();
    }
}

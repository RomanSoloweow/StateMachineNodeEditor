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
    public class ViewModelConnector: ReactiveObject
    {
        /// <summary>
        /// Координата перехода ( нужна для создания соединения )
        /// </summary>
        [Reactive] public MyPoint Position { get; set; }

        /// <summary>
        /// Имя перехода ( вводится в узле)
        /// </summary>
        [Reactive] public string Name { get; set; }

        /// <summary>
        /// Доступно ли имя перехода для редактирования
        /// </summary>
        [Reactive] public bool TextEnable { get; set; } = false;

        /// <summary>
        /// Отображается ли переход
        /// </summary>
        [Reactive] public bool? Visible { get; set; } = true;

        /// <summary>
        /// Доступен ли переход для создания соединия
        /// </summary>
        [Reactive] public bool FormEnable { get; set; } = true;

        /// <summary>
        /// Цвет рамки, вокруг перехода
        /// </summary>
        [Reactive] public Brush FormStroke { get; set; } = Brushes.Black;

        /// <summary>
        /// Цвет перехода
        /// </summary>
        [Reactive] public Brush FormFill { get; set; } = Brushes.DarkGray;

        /// <summary>
        /// Узел, которому принадлежит переход
        /// </summary>
        public ViewModelNode Node { get; set; }

        /// <summary>
        /// Соединение, которое связанно с этим переходом
        /// </summary>
        public ViewModelConnect Connect { get; set; }

        public ViewModelConnector(ViewModelNode viewModelNode)
        {
            Node = viewModelNode;
        }
    }
}

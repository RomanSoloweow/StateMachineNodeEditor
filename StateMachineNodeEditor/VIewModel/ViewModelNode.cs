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
using DynamicData.Binding;

namespace StateMachineNodeEditor.ViewModel
{
    /// <summary>
    /// Отображение для узла
    /// </summary>
    public class ViewModelNode:ReactiveObject, IValidatableViewModel
    {

        public ValidationContext ValidationContext { get; } = new ValidationContext();

        /// <summary>
        /// Масштаб (Общий для всех узлов)
        /// </summary>
        [Reactive] public Scale Scale { get; set; } = new Scale();

        /// <summary>
        /// Перенос (Координата от левого верхнего угла)
        /// </summary>
        [Reactive] public Translate Translate { get; set; } = new Translate(); 

        /// <summary>
        /// Название узла (Указано в шапке)
        /// </summary>
        [Reactive] public string Name { get; set; }

        /// <summary>
        /// Флаг того, что узел выбран
        /// </summary>
        [Reactive] public bool Selected { get; set; }

        /// <summary>
        /// Цвет рамки вокруг узла
        /// </summary>
        [Reactive] public Brush BorderBrush { get; set; } = null;

        /// <summary>
        /// Вход для соединения с этим узлом
        /// </summary>
        public ViewModelConnector Input;

        /// <summary>
        /// Выход ( используется, когда список переходов свернут )
        /// </summary>
        public ViewModelConnector Output;

        /// <summary>
        /// Список переходов
        /// </summary>
        public IObservableCollection<ViewModelConnector> Transitions = new ObservableCollectionExtended<ViewModelConnector>();

        /// <summary>
        /// Текущий переход, из которого можно создать соединение
        /// </summary>
        public ViewModelConnector CurrentConnector;

        /// <summary>
        /// Канвас, которому принадлежит узел
        /// </summary>
        public ViewModelNodesCanvas NodesCanvas;

        public ViewModelNode(ViewModelNodesCanvas nodesCanvas)
        {
            NodesCanvas = nodesCanvas;
            SetupConnectors();
        }
        private void SetupConnectors()
        {
            Input = new ViewModelConnector(this);
            Output = new ViewModelConnector(this)
            {
                Visible = null
            };
            AddEmptyConnector();
        }
        private ViewModelConnector AddEmptyConnector()
        {
            if (CurrentConnector != null)
            {
                CurrentConnector.TextEnable = true;
                CurrentConnector.FormEnable = false;
                CurrentConnector.Name = "Transition_" + Transitions.Count.ToString();
            }
            CurrentConnector = new ViewModelConnector(this)
            {
                TextEnable = false
            };
            Transitions.Insert(0, CurrentConnector);
            return CurrentConnector;
        }
        public void Move(Point delta)
        {
            Translate.Add(delta);
        }

    }
}

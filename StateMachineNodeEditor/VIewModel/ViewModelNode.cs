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
        /// Точка левого верхнего угла
        /// </summary>
        [Reactive] public MyPoint Point1 { get; set; } = new MyPoint();

        /// <summary>
        /// Точка нижнего правого угла
        /// </summary>
        [Reactive] public MyPoint Point2 { get; set; } = new MyPoint();

        /// <summary>
        /// Размер узла
        /// </summary>
        [Reactive]  public Size Size { get; set; }

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
        /// Отображаются ли переходы
        /// </summary>
        [Reactive] public bool? TransitionsVisible { get; set; } = true;

        /// <summary>
        /// Может ли быть удален
        /// </summary>
        [Reactive] public bool CanBeDelete { get; set; } = true;

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
            SetupCommands();

            //this.WhenAnyValue(x => x.Point1.Value, x => x.Size).Subscribe(_ => UpdatePoint2());
        }


        #region Connectors
        private void SetupConnectors()
        {
            Input = new ViewModelConnector(this)
            {
                Name = "Input"
            };
            Output = new ViewModelConnector(this)
            {
                Name = "Output",
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
        #endregion Connectors

        #region Commands
        public SimpleCommandWithParameter <object> CommandSelect { get; set; }
        public SimpleCommandWithParameter <MyPoint> CommandMove{ get; set; }
        public SimpleCommandWithParameter <object> CommandCollapse { get; set; }

        private void SetupCommands()
        {
            CommandSelect = new SimpleCommandWithParameter<object>(this, Select);
            CommandMove  = new SimpleCommandWithParameter<MyPoint>(this, Move);
            CommandCollapse = new SimpleCommandWithParameter<object>(this, Collapse);
        }

        #endregion Commands
        private void Collapse(object obj)
        {
            bool value = (bool)obj;
            Output.Visible = !value;
            if (value)
                TransitionsVisible = value;
            else
                TransitionsVisible = null ;

        }
        private void Select(object selectOne)
        {
            this.Selected = true;
            this.BorderBrush = Brushes.Red;
            //bool selectOnlyOne = false;
            //bool.TryParse(parameters.ToString(), out selectOnlyOne);
        }
        private void Move(MyPoint delta)
        {
            Point1 += delta;
        }

        private void UpdatePoint2()
        {
            Point2.Set(Point1.X + Size.Width, Point1.Y + Size.Height);
        }
    }
}

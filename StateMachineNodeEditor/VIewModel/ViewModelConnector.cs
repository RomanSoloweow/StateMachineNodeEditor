using ReactiveUI.Fody.Helpers;
using StateMachineNodeEditor.Helpers;
using ReactiveUI;
using System.Windows.Media;


namespace StateMachineNodeEditor.ViewModel
{
    public class ViewModelConnector: ReactiveObject
    {
        /// <summary>
        /// Координата перехода ( нужна для создания соединения )
        /// </summary>
        [Reactive] public MyPoint Position { get; set; } = new MyPoint();

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
        [Reactive] public ViewModelNode Node { get; set; }

        /// <summary>
        /// Соединение, которое связанно с этим переходом
        /// </summary>
        [Reactive] public ViewModelConnect Connect { get; set; }

        /// <summary>
        /// Канвас, которому принадлежит соединение
        /// </summary>
        [Reactive] public ViewModelNodesCanvas NodesCanvas { get; set; }

        public ViewModelConnector(ViewModelNodesCanvas nodesCanvas, ViewModelNode viewModelNode)
        {
            Node = viewModelNode;
            NodesCanvas = nodesCanvas;
            SetupCommands();
        }
        #region Commands
        public SimpleCommand CommandDrag { get; set; }
        public SimpleCommand CommandDrop { get; set; }
        public SimpleCommand CommandCheckDrop { get; set; }
        public SimpleCommand CommandAdd { get; set; }
        public SimpleCommand CommandDelete { get; set; }

        public SimpleCommandWithParameter<string> CommandValidateName { get; set; }


        private void SetupCommands()
        {
            CommandDrag = new SimpleCommand(this, Drag);
            CommandDrop = new SimpleCommand(this, Drop);
            CommandCheckDrop = new SimpleCommand(this, CheckDrop);
            CommandAdd = new SimpleCommand(this, Add);
            CommandDelete = new SimpleCommand(this, Delete);

            CommandValidateName = new SimpleCommandWithParameter<string>(this, ValidateName);
        }
        #endregion Commands
        private void ValidateName(string newName)
        {
            NodesCanvas.CommandValidateConnectName.Execute(new ValidateObjectProperty<ViewModelConnector, string>(this, newName));
        }
        private void Add()
        {
            Node.CommandAddConnector.Execute(this);
        }
        private void Delete()
        {
            Node.CommandDeleteConnector.Execute(this);
        }
        
        private void Drag()
        {
            Node.NodesCanvas.CommandAddFreeConnect.Execute(Node.CurrentConnector);
            //FromConnector.Connect = this;
        }

        private void Drop()
        {
            if(Node.NodesCanvas.CurrentConnect.FromConnector.Node!=this.Node)
                Node.NodesCanvas.CurrentConnect.ToConnector = this;
        }
        private void CheckDrop()
        {
            if(Node.NodesCanvas.CurrentConnect.ToConnector==null)
            {
                Node.NodesCanvas.CommandDeleteFreeConnect.Execute();              
            }
            else
            {
                Node.CommandAddEmptyConnector.Execute();
                Node.NodesCanvas.CommandAddConnect.Execute(Node.NodesCanvas.CurrentConnect);
            }
        }
    }
}

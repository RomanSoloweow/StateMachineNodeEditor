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
using DynamicData.Binding;

namespace StateMachineNodeEditor.ViewModel
{
    public class ViewModelNodesCanvas : ReactiveObject
    {
        public IObservableCollection<ViewModelNode> Nodes { get; set; } = new ObservableCollectionExtended<ViewModelNode>();
        public IObservableCollection<ViewModelConnect> Connects { get; set; } = new ObservableCollectionExtended<ViewModelConnect>();

        public ViewModelSelector Selector { get; set; } = new ViewModelSelector();
        public ViewModelConnect CurrentConnect { get; set; }
        private ViewModelConnect AddEmptyConnect()
        {
            CurrentConnect = new ViewModelConnect();
            Connects.Add(CurrentConnect);
            return CurrentConnect;
        }
        public ViewModelNodesCanvas()
        {
            Nodes.Add(new ViewModelNode());
            AddEmptyConnect();
            SetupCommands();
            //commandi = new Commandi(Func);
        }
        
        #region Commands
        public Command CommandRedo { get; set; }
        public Command CommandUndo { get; set; }
        public Command CommandSelectAll { get; set; }
        public Command CommandUnSelectAll { get; set; }
        public Command CommandSelect { get; set; }
        public Command CommandNew { get; set; }
        public Command CommandDelete { get; set; }
        public Command CommandCopy { get; set; }
        public Command CommandPaste { get; set; }
        public Command CommandCut { get; set; }
        public Command CommandMoveDown { get; set; }
        public Command CommandMoveLeft { get; set; }
        public Command CommandMoveRight { get; set; }
        public Command CommandMoveUp { get; set; }
        public Command CommandSimpleMoveAllNode { get; set; }
        public Command CommandSimpleMoveAllSelectedNode { get; set; }
        public Command CommandMoveAllNode { get; set; }
        public Command CommandMoveAllSelectedNode { get; set; }
        //public Command CommandDropOver { get; set; }

        public void SetupCommands()
        {
            CommandRedo = new Command(this, Command.Redo);
        }

        #endregion Commands

    }
}

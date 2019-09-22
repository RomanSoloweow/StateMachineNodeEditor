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
namespace StateMachineNodeEditor.ViewModel
{
    public class ViewModelNodesCanvas : ReactiveObject
    {       
        public SourceList<ViewModelNode> Nodes { get; set; } = new SourceList<ViewModelNode>();

        public SourceList<ViewModelConnect> Connects { get; set; } = new SourceList<ViewModelConnect>();
        public ViewModelNodesCanvas()
        {
            Nodes.Add(new ViewModelNode());
        }
    }
}

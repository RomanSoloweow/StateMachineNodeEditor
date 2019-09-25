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
        public ViewModelNodesCanvas()
        {
            Nodes.Add(new ViewModelNode());
        }
    }
}

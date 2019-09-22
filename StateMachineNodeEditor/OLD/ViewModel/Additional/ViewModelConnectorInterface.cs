using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineNodeEditor
{
   public interface ViewModelConnectorInterface
   {
        String Text { get; set; }
        bool TextIsEnable { get; set; }

        bool? Visible { get; set; }

        bool FormIsEnable { get; set; }

    }
}

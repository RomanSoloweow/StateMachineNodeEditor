using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StateMachineNodeEditor
{
    /// <summary>
    /// Interaction logic for VIewNodesCanvas.xaml
    /// </summary>
    public partial class ViewNodesCanvas : UserControl
    {
        public ViewNodesCanvas()
        {
            InitializeComponent();
            ViewModelNodesCanvas = new ViewModelNodesCanvas(new ModelNodesCanvas());
            DataContext = ViewModelNodesCanvas;

            this.DataContextChanged += DataContextChange;
            this.MouseRightButtonDown += mouseRightDown;
        }
        public void mouseRightDown(object sender, MouseButtonEventArgs e)
        {
            positionRightClick = e.GetPosition(this);
        }
        private Point positionRightClick;
        public ViewModelNodesCanvas ViewModelNodesCanvas { get; set; }
        public void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModelNodesCanvas = e.NewValue as ViewModelNodesCanvas;
        }
        
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }
        private void Select(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandSelect.Execute(e.Parameter);
        }
        private void SelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandSelectAll.Execute(e.Parameter);
        }
        private void New(object sender, ExecutedRoutedEventArgs e)
        {
            Point point = new Point();

            if (positionRightClick != point)
                point = positionRightClick;
            else
                point = Mouse.GetPosition(this);

            positionRightClick = new Point();
            ViewModelNodesCanvas.CommandNew.Execute(point);
        }
        private void Redo(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandRedo.Execute(e.Parameter);
        }
        private void Undo(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandUndo.Execute(e.Parameter);
        }
        private void Copy(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandCopy.Execute(e.Parameter);
        }
        private void Paste(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandPaste.Execute(e.Parameter);
        }
        private void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandDelete.Execute(e.Parameter);
        }
        private void Cut(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandCut.Execute(e.Parameter);
        }

        private void MoveDown(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandMoveDown.Execute(e.Parameter);
        }
        private void MoveLeft(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandMoveLeft.Execute(e.Parameter);
        }
        private void MoveRight(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandMoveRight.Execute(e.Parameter);
        }
        private void MoveUp(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandMoveUp.Execute(e.Parameter);
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Keyboard.Focus(this);
        }
    }
}

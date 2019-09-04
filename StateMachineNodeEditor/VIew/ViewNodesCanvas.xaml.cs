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
        public ViewModelNodesCanvas ViewModelNodesCanvas { get; set; }
        private Point? positionRightClick;
        private Point positionLeftClick;
        private Point? positionMove;
        public ViewNodesCanvas()
        {
            InitializeComponent();
            ViewModelNodesCanvas = new ViewModelNodesCanvas(new ModelNodesCanvas());
            DataContext = ViewModelNodesCanvas;

            this.DataContextChanged += DataContextChange;
  
            this.MouseMove += OnMouseMove;
            this.MouseDown += OnMouseDown;
            this.MouseUp += OnMouseUp;
            this.MouseRightButtonDown += OnMouseRightDown;
            this.MouseLeftButtonDown += OnMouseLeftDown;
            this.DragOver += OnDragOver;
        }
        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
        }
        protected  void OnDragOver(object sender, DragEventArgs e)
        {
            e.Data.SetData("Position", e.GetPosition(this.grid));
            ViewModelNodesCanvas.CommandDropOver.Execute(e.Data);
        }
        private Point GetDeltaMove()
        {
            Point CurrentPosition = Mouse.GetPosition(this.grid);
            Point result = new Point();

            if (positionMove !=null)
            {
                result = ForPoint.Subtraction(CurrentPosition, positionMove.Value);
            }
            positionMove = CurrentPosition;
            return result;
        }
        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();
            positionMove = null;
            Keyboard.Focus(this);
        }
        public void OnMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
            positionRightClick = e.GetPosition(this.grid);
        }
        public void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            Point t = this.PointToScreen(Mouse.GetPosition(this.grid));
            //Console.WriteLine(" PointToScreen "+t.ToString());
            //Console.WriteLine(" PointFromScreen " + this.PointFromScreen(t).ToString());
            if (Mouse.Captured == null)
            {
                Keyboard.ClearFocus();
                this.CaptureMouse();
                Keyboard.Focus(this);
            }
            positionLeftClick = e.GetPosition(this.grid);
            if (this.IsMouseCaptured)
                ViewModelNodesCanvas.CommandUnSelectAll.Execute(null);
        }
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.Captured == null)
                return;
            Point delta = GetDeltaMove();
            if (ForPoint.isEmpty(delta))
                return;
            if (this.IsMouseCaptured)
            {
                ViewModelNodesCanvas.CommandMoveAllNode.Execute(delta);

                //foreach (Node node in nodes)
                //{
                //    node.Selected = false;
                //    node.Manager.Move(delta);
                //}
            }
            else
            {
                ViewModelNodesCanvas.CommandMoveAllSelectedNode.Execute(delta);
                //foreach (Node node in nodes.Where(x => x.Selected == true).ToList())
                //{
                //    node.Manager.Move(delta);
                //}
            }
        }
       
       
        public void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModelNodesCanvas = e.NewValue as ViewModelNodesCanvas;
        }
        
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Ctrl +r");
        }
        private void Select(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandSelect.Execute(Mouse.GetPosition(this.grid));
        }
        private void SelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModelNodesCanvas.CommandSelectAll.Execute(e.Parameter);
        }
        private void New(object sender, ExecutedRoutedEventArgs e)
        {         
            ViewModelNodesCanvas.CommandNew.Execute(positionRightClick?? Mouse.GetPosition(this.grid));
            positionRightClick = null;
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
    }
}

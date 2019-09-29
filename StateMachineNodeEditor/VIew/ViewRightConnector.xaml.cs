﻿using System;
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
using ReactiveUI.Fody.Helpers;
using StateMachineNodeEditor.Helpers;
using ReactiveUI;
using ReactiveUI.Wpf;
using DynamicData;
using StateMachineNodeEditor.ViewModel;

namespace StateMachineNodeEditor.View
{
    /// <summary>
    /// Interaction logic for ViewConnector.xaml
    /// </summary>
    public partial class ViewRightConnector : UserControl, IViewFor<ViewModelConnector>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ViewModelConnector), typeof(ViewRightConnector), new PropertyMetadata(null));

        public ViewModelConnector ViewModel
        {
            get { return (ViewModelConnector)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelConnector)value; }
        }
        #endregion ViewModel

        public ViewRightConnector()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                // Имя перехода ( вводится в узле)
                this.Bind(this.ViewModel, x => x.Name, x => x.Text.Text);

                // Доступно ли имя перехода для редактирования
                this.Bind(this.ViewModel, x => x.TextEnable, x => x.Text.IsEnabled);

                // Доступен ли переход для создания соединия
                this.Bind(this.ViewModel, x => x.FormEnable, x => x.Form.IsEnabled);

                // Цвет рамки, вокруг перехода
                this.Bind(this.ViewModel, x => x.FormStroke, x => x.Form.Stroke);

                // Цвет перехода
                this.Bind(this.ViewModel, x => x.FormFill, x => x.Form.Fill);

                // Отображается ли переход
                this.OneWayBind(this.ViewModel, x => x.Visible, x => x.RightConnector.Visibility);

                // При изменении рамера или позиции узла
                this.WhenAnyValue(x => x.ViewModel.Node.Size,x=>x.ViewModel.Node.Translate.Value.Value).Subscribe(_=> UpdatePosition());
            });
        }
        void UpdatePosition()
        {
            Point Position;

            //Если узел не свернут
            if (this.ViewModel.Visible == true)
            {
                // Координата центра
                Point InputCenter = Form.TranslatePoint(new Point(Form.Width / 2, Form.Height / 2), this);

                //Ищем Canvas
                ViewNodesCanvas NodesCanvas = Visuals.FindParent<ViewNodesCanvas>(this);

                //Получаем позицию центру на канвасе
                Position = this.TransformToAncestor(NodesCanvas).Transform(InputCenter);
            }
            else
            {
                //Позиция выхода
                Position = this.ViewModel.Node.Output.Position.Value;
            }

            this.ViewModel.Position.Set(Position);
        }
    }
}

using System;
using System.Windows.Input;

namespace StateMachineNodeEditor.Helpers
{
    /// <summary>
    /// Команда без Undo/Redo
    /// </summary>
    /// <typeparam name="TypeParameter"></typeparam>
    public class SimpleCommand<TypeParameter>:ICommand
    {
        /// <summary>
        /// Функция, которая будет вызвана при выполнении команды
        /// </summary>
        private Action<TypeParameter> _execute;

        /// <summary>
        /// Объкт, которому принадлежит команда
        /// </summary>
        public object Owner { get; protected set; }

        /// <summary>
        /// Требуется  интерфейсом ICloneable, не используется
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Требуется  интерфейсом ICloneable, не используется
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Всегда возвращает true</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполнение команды
        /// </summary>
        /// <param name="parameter"> Параметр команды </param>
        public void Execute(object parameter)
        {
            this._execute((TypeParameter)parameter);
        }

        /// <summary>
        /// Создать команду
        /// </summary>
        /// <param name="owner">Объкт, которому принадлежит команда</param>
        /// <param name="execute">Функция, которая будет вызвана при выполнении команды</param>
        public SimpleCommand(object owner, Action<TypeParameter> execute=null)
        {
            Owner = owner;
            _execute = execute;
        }
    }
}

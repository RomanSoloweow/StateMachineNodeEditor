using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace StateMachineNodeEditor.Helpers
{
    public class Command : ICommand, ICloneable
    {
        /// <summary>
        /// Стек отмененных команд, которые можно выполнить повторно
        /// </summary>
        public static Stack<Command> Redo { get; set; } = new Stack<Command>();

        /// <summary>
        /// Стек выполненных команд, которые можно отменить 
        /// </summary>
        public static Stack<Command> Undo { get; set; } = new Stack<Command>();

        /// <summary>
        /// Функция, которая будет вызвана при выполнении команды
        /// </summary>
        private Func<object, object, object> _execute;

        /// <summary>
        /// Функция отмены команды
        /// </summary>
        private Func<object, object, object> _unExecute;

        /// <summary>
        /// Добавить копию команды в стек команд, которые можно выполнить повторно
        /// </summary>
        private void AddInRedo()
        {
            Redo.Push(this.Clone() as Command);
        }

        /// <summary>
        /// Добавить копию команды в стек команд, которые можно отменить
        /// </summary>
        private void AddInUndo()
        {
            Undo.Push(this.Clone() as Command);
        }
     
        /// <summary>
        /// Параметр, который был передан в команду при выполнении
        /// </summary>
        public object Parameters { get; protected set; }

        /// <summary>
        /// Результат выполнения команды
        /// </summary>
        /// Например здесь может храниться список объектов, которые были изменены
        public object Result { get; protected set; }

        /// <summary>
        /// Объкт, которому принадлежит команда
        /// </summary>
        public object Owner { get; protected set; }

        /// <summary>
        /// Флаг того, является ли команда отменяемой 
        /// </summary>
        private bool CanUnExecute
        {
            get { return _unExecute != null; }
        }    
        
        /// <summary>
        /// Клонирование текущей команды, для записи в стек выполненных или отмененных команд
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Command command = new Command(Owner, _execute, _unExecute);
            command.Parameters = this.Parameters;
            command.Result = this.Result;

            return command;
        }

        /// <summary>
        /// Требуется  интерфейсом ICloneable, не используется
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

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
            //Запоминаем параметр ( чтобы можно было егоже передать в отмену)
            Parameters = parameter;

            //Выполняем команду и запоминаем результат ( чтобы можно было выполнить отмену именно для этого результата)
            Result = this._execute(Parameters, Result);

            //Если команду можно отменить
            if (CanUnExecute)
            {
                //Добавляем копию команды в стек команд, которые можно отменить
                AddInUndo();

                //Очищаем список отмененнных команд ( началась новая ветка изменений)
                Redo.Clear();
            }

            //Очищаем результат ( чтобы не передавать его при повторном выполнении)
            Result = null;

            //Очищаем параметр ( чтобы не передавать его при повторном выполнении)
            Parameters = null;
        }

        /// <summary>
        /// Отмена команды
        /// </summary>
        public void UnExecute()
        {
            //Отменяем команду
            this._unExecute(Parameters, Result);

            //Добавляем копию команды в стек команд, которые можно выполнить повторно
            AddInRedo();
        }

        /// <summary>
        /// Установить функцию, которая будет вызвана при выполнении команды
        /// </summary>
        /// <param name="action">Функция, которая будет вызвана при выполнении команды</param>
        public void SetExecute(Func<object, object, object> action)
        {
            _execute = action;
        }

        /// <summary>
        /// Установить функцию, которая будет вызвана при отмене команды
        /// </summary>
        /// <param name="action">Функция, которая будет вызвана при отмене команды</param>
        public void SetUnExecute(Func<object, object, object> action)
        {
            _unExecute = action;
        }

        /// <summary>
        /// Очистить функцию, которая будет вызвана при выполнении команды
        /// </summary>
        public void RemoveExecute()
        {
            _execute = null;
        }

        /// <summary>
        /// Очистить функцию, которая будет вызвана при отмене команды
        /// </summary>
        public void RemoveUnExecute()
        {
            _unExecute = null;
        }

        /// <summary>
        /// Создать неотменяемую команду ( Для создания отменяемой команды, добавьте функцию, которая будет вызвана при отмене)
        /// </summary>
        /// <param name="owner">Объкт, которому принадлежит команда</param>
        /// <param name="action">Функция, которая будет вызвана при выполнении команды</param>
        public Command(object owner, Func<object, object, object> action)
        {
            Owner = owner;
            SetExecute(action);
        }

        /// <summary>
        /// Создать отменяемую команду
        /// </summary>
        /// <param name="owner">Объкт, которому принадлежит команда</param>
        /// <param name="action">Функция, которая будет вызвана при выполнении команды</param>
        /// <param name="unAction">Функция, которая будет вызвана при отмене команды</param>
        public Command(object owner, Func<object, object, object> action, Func<object, object, object> unAction) : this(owner, action)
        {
            SetUnExecute(unAction);
        }
    }
}

using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace StateMachineNodeEditor.Helpers
{
    /// <summary>
    /// Команда с Undo/Redo
    /// </summary>
    /// <typeparam name="TypeParameter">Тип параметра, передаваемого для выполнения</typeparam>
    /// <typeparam name="TypeResult">Тип результата выполнения</typeparam>
    public class Command<TypeParameter, TypeResult> : BaseCommand,ICommand, ICloneable where TypeParameter: class where  TypeResult : class
    {
        /// <summary>
        /// Стек отмененных команд, которые можно выполнить повторно
        /// </summary>
        public static Stack<BaseCommand> StackRedo { get; set; } = new Stack<BaseCommand>();

        /// <summary>
        /// Стек выполненных команд, которые можно отменить 
        /// </summary>
        public static Stack<BaseCommand> StackUndo { get; set; } = new Stack<BaseCommand>();

        /// <summary>
        /// Функция, которая будет вызвана при выполнении команды
        /// </summary>
        private readonly Func<TypeParameter, TypeResult, TypeResult> _execute;

        /// <summary>
        /// Функция отмены команды
        /// </summary>
        private readonly Func<TypeParameter, TypeResult, TypeResult> _unExecute;

        /// <summary>
        /// Добавить копию команды в стек команд, которые можно выполнить повторно
        /// </summary>
        private void AddInRedo()
        {
            StackRedo.Push(this.Clone() as Command<TypeParameter, TypeResult>);
        }

        /// <summary>
        /// Добавить копию команды в стек команд, которые можно отменить
        /// </summary>
        private void AddInUndo()
        {
           StackUndo.Push(this.Clone() as Command<TypeParameter, TypeResult>);
        }

        /// <summary>
        /// Параметр, который был передан в команду при выполнении
        /// </summary>
        public  TypeParameter Parameters { get;  set; }

        /// <summary>
        /// Результат выполнения команды
        /// </summary>
        /// Например здесь может храниться список объектов, которые были изменены
        public TypeResult Result { get;  set; }

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
            return new Command<TypeParameter, TypeResult>(Owner, _execute, _unExecute) 
            {
                Parameters = this.Parameters,
                Result = this.Result
            };
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
            Parameters = parameter as TypeParameter;

            //Выполняем команду и запоминаем результат ( чтобы можно было выполнить отмену именно для этого результата)
            Result = this._execute(Parameters, Result) as TypeResult;

            //Если команду можно отменить
            if (CanUnExecute)
            {
                //Добавляем копию команды в стек команд, которые можно отменить
                AddInUndo();

                //Очищаем список отмененнных команд ( началась новая ветка изменений)
                StackRedo.Clear();
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
            //Выполняем отмену команду
            this._unExecute(Parameters, Result);

            //Добавляем копию команды в стек команд, которые можно выполнить повторно
            AddInRedo();
        }

        /// <summary>
        /// Повторное выполнения команды
        /// </summary>
        public void Execute()
        {
            //Выполянем команду
            this.Result = this._execute(this.Parameters, this.Result);

            //Добавляем копию команды в стек команд, которые можно отменить
            AddInUndo();
        }

        /// <summary>
        /// Функция для команды повторного выполнения
        /// </summary>
        /// <param name="obj">Не используются</param>
        public static void Redo(object obj = null)
        {
            if (Command<TypeParameter, TypeResult>.StackRedo.Count > 0)
            {
                BaseCommand last = Command<TypeParameter, TypeResult>.StackRedo.Pop();
                last.Execute();
            }
        }

        /// <summary>
        /// Функция для команды отмены 
        /// </summary>
        /// <param name="obj">Не используются<</param>
        public static void Undo(object obj = null)
        {
            if (Command<TypeParameter, TypeResult>.StackUndo.Count > 0)
            {
                BaseCommand last = Command<TypeParameter, TypeResult>.StackUndo.Pop();
                last.UnExecute();
            }
        }

        /// <summary>
        /// Создать неотменяемую команду ( Для создания отменяемой команды, добавьте функцию, которая будет вызвана при отмене)
        /// </summary>
        /// <param name="owner">Объкт, которому принадлежит команда</param>
        /// <param name="action">Функция, которая будет вызвана при выполнении команды</param>
        public Command(object owner, Func<TypeParameter, TypeResult, TypeResult> execute)
        {
            Owner = owner;
            _execute = execute;
        }

        /// <summary>
        /// Создать отменяемую команду
        /// </summary>
        /// <param name="owner">Объкт, которому принадлежит команда</param>
        /// <param name="execute">Функция, которая будет вызвана при выполнении команды</param>
        /// <param name="unExecute">Функция, которая будет вызвана при отмене команды</param>
        public Command(object owner, Func<TypeParameter, TypeResult, TypeResult> execute, Func<TypeParameter, TypeResult, TypeResult> unExecute) : this(owner, execute)
        {
            _unExecute = unExecute;
        }
    }
}

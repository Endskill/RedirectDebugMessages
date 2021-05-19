using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RedirectDebugMessages.MVVM
{
	/// <summary>
	/// Just an extension of the ICommand Interface
	/// </summary>
	public class ActionCommand : ICommand
	{
		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _execute;

		public ActionCommand(Action<object> execute)
		{

			_execute = execute;
			_canExecute = (par => { return true; });
		}

		public ActionCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_canExecute = canExecute;
			_execute = execute;
		}

		/// <summary>
		/// CanExecute Variable which defines if you are able to progress
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public bool CanExecute(object parameter)
		{
			return _canExecute(parameter);
		}

		/// <summary>
		/// Execute Method which will fire when the CanExecute is positiv
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;
	}
}

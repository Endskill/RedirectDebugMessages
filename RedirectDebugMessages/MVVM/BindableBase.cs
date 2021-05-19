using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirectDebugMessages.MVVM
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Can be called to symbolise the Control to update
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Changes <paramref name="field"/> to <paramref name="value"/> if they are not the same
        /// calls <see cref="OnPropertyChanged"/> with <paramref name="nameOfField"/> when <paramref name="field"/> gets changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field that should get Changed</param>
        /// <param name="value">The new Value that <paramref name="field"/> should have</param>
        /// <param name="nameOfField">nameof <paramref name="field"/></param>
        public void Set<T>(ref T field, T value, string nameOfField)
        {
            if (!value.Equals(field))
            {
                field = value;
                OnPropertyChanged(nameOfField);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ERSA.Wpf
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Event informujący o zmianie w jednej z właściwości ViewModelu
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Ustawia wartość pola ViewModelu<br/>
        /// Jeśli wartość parametru <paramref name="value"/> różni się od obecnej wartości pola, zostanie ono zaktualizowane <br/>
        /// oraz zostanie wywołany event informujący o zmianie wartości
        /// </summary>
        /// <typeparam name="T">Typ pola które ma być zaktualizowane</typeparam>
        /// <param name="field">Pole które ma zostać zaktualizowane</param>
        /// <param name="value">Nowa wartość dla pola</param>
        /// <param name="propertyName">Nazwa właściwości o której zmianie ma zostać poinformowany WPF<br/>(Domyślnie wypełniona nazwą metody/właściwości która wywołała tę metodę)</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            PropertyHasChanged(propertyName ?? throw new ArgumentNullException(nameof(propertyName)));

            return true;
        }

        /// <summary>
        /// Metoda wywołująca event informujący WPF o zmianie w ViewModelu
        /// </summary>
        /// <param name="property"></param>
        protected void PropertyHasChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

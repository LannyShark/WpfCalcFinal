using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCalcFinal.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private double Sum { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string txtBox1;
        public string TxtBox1
        {
            get => txtBox1;
            set
            {
                txtBox1 = value;
                OnPropertyChanged();
            }
        }

        private string txtBox2;
        public string TxtBox2
        {
            get => txtBox2;
            set
            {
                txtBox2 = value;
                OnPropertyChanged();
            }
        }

        public ICommand ClickOnButton { get; }
        private void OnAddCommandExecute(object p)
        {
            switch (/*текст на кнопке*/)
            {
                case "=":
                    TxtBox1 += TxtBox2;
                    TxtBox2 = new DataTable().Compute(TxtBox1,null).ToString();
                    break;
                case "С":
                    TxtBox1 = "";
                    TxtBox2 = "";
                    break;
                case "CE":
                    TxtBox2 = "";
                    break;
                case "←":
                    TxtBox2.Remove(TxtBox2.Length, 1);
                    break;
                case "+/-":
                    TxtBox1 += "(-"+TxtBox2+")";
                    TxtBox2 = "";
                    break;
                case "%":
                    TxtBox1 = /*высчитываем значение доя последнего симовола*/ + /*последний символ*/ + /*значение доя последнего симовола * TxtBox2/100 */;
                    TxtBox2 = "";
                    break;
                case "x²":
                    TxtBox2 = Math.Pow(Convert.ToDouble(TxtBox2),2).ToString();
                    break;
                case "√x":
                    TxtBox2 = Math.Sqrt(Convert.ToDouble(TxtBox2)).ToString();
                    break;
                default:
                    if (/*текст на кнопке == цифра или точка*/)
                    {
                        TxtBox2 += /*цифра или точка*/;
                    }
                    else if (/*текст на кнопке == +,-,/,* */)
                    {
                        TxtBox1 += TxtBox2 /*+ текст на кнопке*/;
                        TxtBox2 = "";
                    }
                    break;
            }
        }
        private bool CanAddCommandExecute(object p)
        {
            if (/*текст на кнопке == цифра||*/ TxtBox2 != "")
            {
                return true;
            }
            return false;
        }
        public MainWindowViewModel()
        {
            ClickOnButton = new RelayCommand(OnAddCommandExecute, CanAddCommandExecute);
        }
    }
}

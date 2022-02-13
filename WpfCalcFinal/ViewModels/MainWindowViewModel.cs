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
        private void OnClickOnButtonExecute(object p)
        {
            switch (p as string)
            {
                case "=":
                    TxtBox1 += TxtBox2;
                    TxtBox2 = new DataTable().Compute(TxtBox1, null).ToString().Replace(',','.');
                    TxtBox1 = "";
                    break;
                case "C":
                    TxtBox1 = "";
                    TxtBox2 = "";
                    break;
                case "CE":
                    TxtBox2 = "";
                    break;
                case "←":
                    TxtBox2 = TxtBox2.Remove(TxtBox2.Length - 1, 1);
                    break;
                case "+/-":
                    TxtBox2 = "-" + TxtBox2;
                    break;
                case "%":
                    double pool = Convert.ToDouble(new DataTable().Compute(TxtBox1.Remove(TxtBox1.Length-1, 1), null));
                    TxtBox1 = pool.ToString() + TxtBox1.Remove(0, (TxtBox1.Length - 1)) + (pool * Convert.ToDouble(TxtBox2) / 100).ToString();
                    TxtBox2 = "";
                    break;
                case "x²":
                    TxtBox2 = Math.Pow(Convert.ToDouble(TxtBox2), 2).ToString();
                    break;
                case "√x":
                    TxtBox2 = Math.Sqrt(Convert.ToDouble(TxtBox2)).ToString();
                    break;
                default:
                    if (p as string == "1" || p as string == "2" || p as string == "3" || p as string == "4" || p as string == "5" || p as string == "6" || p as string == "7" || p as string == "8" || p as string == "9" || p as string == "0" || p as string == ".")
                    {
                        TxtBox2 += p as string;
                    }
                    else if (p as string == "+" || p as string == "-" || p as string == "*" || p as string == "/")
                    {
                        TxtBox1 += TxtBox2 + p as string;
                        TxtBox2 = "";
                    }
                    break;
            }
        }
        private bool CanClickOnButtonExecute(object p)
        {
            if (p as string == "1" || p as string == "2" || p as string == "3" || p as string == "4" || p as string == "5" || p as string == "6" || p as string == "7" || p as string == "8" || p as string == "9" || p as string == "0" || p as string == "." || TxtBox2 != "")
            {
                return true;
            }
            return false;
        }
        public MainWindowViewModel()
        {
            TxtBox1 = "";
            TxtBox2 = "";
            ClickOnButton = new RelayCommand(OnClickOnButtonExecute, CanClickOnButtonExecute);
        }
    }
}

using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace VirtualKeyboard
{
    public enum KeyboardType { FULL, KEYBOARD, NUMPAD }

    public partial class UCKeyboard : UserControl, INotifyPropertyChanged
    {
        public KeyboardType KeyboardType { get; set; }

        #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string characters = "qwertyuiop^asdfghjklñzxcvbnm";
        public string CharactersRow1 { get { return characters.Substring(0, 10); } }
        public string CharactersRow2 { get { return characters.Substring(10, 11); } }
        public string CharactersRow3 { get { return characters.Substring(21, 7); } }

        public string Numbers { get { return "1234567890<"; } }
        public string NumbersRow1 { get { return Numbers.Substring(0, 3); } }
        public string NumbersRow2 { get { return Numbers.Substring(3, 3); } }
        public string NumbersRow3 { get { return Numbers.Substring(6, 3); } }
        public string NumbersRow4 { get { return Numbers.Substring(9, 2); } }

        // String var where the keyboard will write
        public static readonly DependencyProperty StringVarProperty = DependencyProperty.Register("StringVar", typeof(string), typeof(UCKeyboard));
        public string StringVar
        {
            get { return (string)GetValue(StringVarProperty); }
            set { SetValue(StringVarProperty, value); }
        }

        public RelayCommand<string> CommandClick { get; set; }

        public UCKeyboard()
        {
            InitializeComponent();

            CommandClick = new RelayCommand<string>(CommandClick_Execute);
        }

        private void CommandClick_Execute(string character)
        {
            switch(character)
            {
                case "^":
                    // Mayus
                    if (characters[0] == 'q')
                        characters = characters.ToUpper();
                    else
                        characters = characters.ToLower();

                    RaisePropertyChanged("CharactersRow1");
                    RaisePropertyChanged("CharactersRow2");
                    RaisePropertyChanged("CharactersRow3");
                    break;

                case "<":
                    // Delete
                    StringVar = StringVar.Remove(StringVar.Length - 1);
                    return;

                default:
                    StringVar += character;
                    break;
            }
        }
    }
}

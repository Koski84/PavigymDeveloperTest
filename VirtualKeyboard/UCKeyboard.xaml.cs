using System.Windows.Controls;

namespace VirtualKeyboard
{
    public partial class UCKeyboard : UserControl
    {
        private string characters = "qwertyuiopasdfghjklñzxcvbnm";

        public char[] CharactersRow1 { get { return characters.Substring(0, 10).ToCharArray(); } }
        public char[] CharactersRow2 { get { return characters.Substring(10, 10).ToCharArray(); } }
        public char[] CharactersRow3 { get { return characters.Substring(20, 7).ToCharArray(); } }

        public string Numbers { get { return "1234567890"; } }

        

        public UCKeyboard()
        {
            InitializeComponent();
        }
    }
}

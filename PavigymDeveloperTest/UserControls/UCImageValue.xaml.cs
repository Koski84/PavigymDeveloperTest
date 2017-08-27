using System.Windows;
using System.Windows.Controls;

namespace PavigymDeveloperTest.UserControls
{
    public partial class UCImageValue : UserControl
    {
        public static readonly DependencyProperty ImgSourceProperty = DependencyProperty.Register("ImgSource", typeof(string), typeof(UCImageValue));
        public string ImgSource
        {
            get { return (string)GetValue(ImgSourceProperty); }
            set { SetValue(ImgSourceProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(UCImageValue));
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty UnitProperty = DependencyProperty.Register("Unit", typeof(string), typeof(UCImageValue));
        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        public UCImageValue()
        {
            InitializeComponent();
        }
    }
}

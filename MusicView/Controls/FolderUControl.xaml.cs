using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicView.Controls
{
    /// <summary>
    /// Логика взаимодействия для FolderUControl.xaml
    /// </summary>

    public partial class FolderUControl : UserControl
    {
        public List<SoundUControl> listSound;
        private static Geometry UpChevron, DownChevron;
        private bool IsHide = true;
        private string FName;

        public string Text
        {
            get => FName;
            set {
                FName = value;
                FolderName.Text = "(" + listSound.Count + ") " + FName;
            }
        }

        public void UpdateName() => FolderName.Text = "(" + listSound.Count + ") " + FName;

        static FolderUControl()
        {
            UpChevron = Geometry.Parse("M1,10 L9,2 L17,10");
            DownChevron = Geometry.Parse("M1,2 L9,10 L17,2");
        }

        public FolderUControl()
        {
            InitializeComponent();
            listSound = new List<SoundUControl>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsHide)
            {
                Chevron.Data = DownChevron;
                foreach (SoundUControl item in listSound)
                    item.Visibility = Visibility.Visible;
                IsHide = false;
            } else {
                Chevron.Data = UpChevron;
                foreach (SoundUControl item in listSound)
                    item.Visibility = Visibility.Collapsed;
                IsHide = true;
            }
            
        }
    }
}

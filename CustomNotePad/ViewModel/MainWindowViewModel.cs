using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomNotePad
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public static long AllComponents { get; set; }
        //Models
        #region
        private TextComponent selectedModel;
        public TextComponent SelectedModel
        {
            get => this.selectedModel;
            set
            {
                this.selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }
        public ObservableCollection<TextComponent> CollectionItems { get; set; }
        #endregion
        public MainWindowViewModel()
        {
            AllComponents = 0;
            CollectionItems = new ObservableCollection<TextComponent>();
            ReadFromFile();
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

        }





        //Commands
        #region
        //AddCommand
        #region
        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(() =>
                {
                    TextComponent text = new TextComponent();
                    CollectionItems.Add(text);
                    SelectedModel = text;
                }));
            }
            set { addCommand = value; }
        }
        #endregion

        //DeleteCommand
        #region 
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(() =>
                {
                    CollectionItems.Remove(SelectedModel);
                }));
            }
            set { addCommand = value; }
        }
        #endregion
        #endregion


        //Save & Read
        #region

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            for (int i = 0; i < this.CollectionItems.Count; i++)
            {
                File.WriteAllText($"Data/{i}.txt", String.Format("{0}|{1}|{2}", this.CollectionItems[i].Name, this.CollectionItems[i].Description, this.CollectionItems[i].Text));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void ReadFromFile()
        {
            string[] files = Directory.GetFiles("Data");
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    string TEMP = File.ReadAllText(files[i]);
                    string[] TEMP2 = TEMP.Split("|");
                    this.CollectionItems.Add(new TextComponent(TEMP2[0], TEMP2[1], TEMP2[2]));
                    AllComponents++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        #endregion
    }
}

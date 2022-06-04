using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomNotePad
{
    public class TextComponent : ViewModelBase
    {

        private string name;
        private string descr;
        private string text;
        private char code;



        public event PropertyChangedEventHandler PropertyChanged;

        public TextComponent()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
            this.Text = String.Empty;
            this.Code = '#';
        }
        public TextComponent(string name, string description,string text)
        {
            this.Name = name;
            this.Description = description;
            this.Code = this.name[0];
            this.Text = text;
        }

        public string Name
        {
            get => this.name;
            set
            {
                SetProperty<string>(ref this.name, value);
                if (name != String.Empty)
                {
                    this.Code = this.name[0];
                }
                else
                {
                    this.Code = '#';
                }
                OnPropertyChanged("Name");
            }
        }
        public char Code
        {
            get => this.code;
            set => SetProperty<char>(ref this.code, value);
        }
        public string Description
        {
            get => this.descr;
            set => SetProperty<string>(ref this.descr, value);
        }
        public string Text
        {
            get => this.text;
            set => SetProperty<string>(ref this.text, value);
        }
    }
}

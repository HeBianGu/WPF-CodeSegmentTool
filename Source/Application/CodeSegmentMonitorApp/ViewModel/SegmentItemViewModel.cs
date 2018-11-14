#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/5/2 10:18:51 
 * 文件名：SegmentItemViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeSegmentMonitorApp.ViewModel
{
    /// <summary> 说明 </summary>
    partial class SegmentItemViewModel
    {
        private string _fileName;
        /// <summary> 说明 </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        private string _title;
        /// <summary> 说明 </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }


        private string _filePath;
        /// <summary> 说明 </summary>
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged();
            }
        }

        private string _snippet;
        /// <summary> 说明 </summary>
        public string Snippet
        {
            get { return _snippet; }
            set
            {
                _snippet = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        /// <summary> 说明 </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        private string _shortcut;
        /// <summary> 说明 </summary>
        public string Shortcut
        {
            get { return _shortcut; }
            set
            {
                _shortcut = value;
                RaisePropertyChanged();
            }
        }

        private string _snippetTypes;
        /// <summary> 说明 </summary>
        public string SnippetTypes
        {
            get { return _snippetTypes; }
            set
            {
                _snippetTypes = value;
                RaisePropertyChanged();
            }
        }

        private string _author = "Create By HeBianGu";
        /// <summary> 说明 </summary>
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                RaisePropertyChanged();
            }
        }


   

        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj as string;

            switch (buttonName)
            {
                case "Case1":
                    {

                    }
                    break;
                case "Case2":
                    {

                    }
                    break;
                case "Case3":
                    {

                    }
                    break;
                case "Case4":
                    {

                    }
                    break;
                case "Case5":
                    {

                    }
                    break;
                case "Case6":
                    {

                    }
                    break;
                case "Case7":
                    {

                    }
                    break;
                case "Case8":
                    {

                    }
                    break;
                case "Case9":
                    {

                    }
                    break;
                case "Case10":
                    {

                    }
                    break;
                case "Case11":
                    {

                    }
                    break;
                case "Case12":
                    {

                    }
                    break;
                default:
                    break;
            }

           
        }
    }

    partial class SegmentItemViewModel : NotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public SegmentItemViewModel()
        {
            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));
        }

        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

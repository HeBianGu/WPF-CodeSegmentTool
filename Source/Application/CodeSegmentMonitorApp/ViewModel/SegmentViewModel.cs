#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/5/2 10:16:12 
 * 文件名：MainViewModel 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using CodeSegmentMonitorApp.Provider;
using HeBianGu.Base.Util;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeSegmentMonitorApp.ViewModel
{
    /// <summary> 说明 </summary>
    partial class SegmentViewModel
    {
        private ObservableCollection<SegmentItemViewModel> _collection = new ObservableCollection<SegmentItemViewModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<SegmentItemViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        private string _path;


        /// <summary> 说明 </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;

                Debug.WriteLine("Path:" + _path);

                RaisePropertyChanged();
            }
        }

        private SegmentItemViewModel _selection;
        /// <summary> 说明 </summary>
        public SegmentItemViewModel Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                RaisePropertyChanged();
            }
        }



        private void ButtonClickFunc(object obj)
        {
            string buttonName = obj as string;


            Debug.WriteLine("ButtonClickFunc:" + this.Path);
            switch (buttonName)
            {
                case "TextChanged":
                    {
                        if (string.IsNullOrEmpty(this.Path)) return;

                        if (!Directory.Exists(Path)) return;

                        var collection = Path.GetAllFile(l => l.Extension.EndsWith("snippet"));

                        this.Collection.Clear();

                        foreach (var item in collection)
                        {
                            SegmentItemViewModel c = SegmentProvider.Instance.Create(item);
                            this.Collection.Add(c);
                        }

                        string path = SegmentProvider.Instance.GetDefaultPath();

                        if (path.Trim() != this.Path.Trim())
                        {
                            SegmentProvider.Instance.SetDefaultPath(this.Path.Trim());
                        }

                        this.ButtonClickFunc("Refresh");
                    }
                    break;
                case "Refresh":
                    {
                        //var collection = this.Collection.OrderBy(l => l.Author).ThenBy(l => l.FileName).ToList();

                        //ObservableCollection<SegmentItemViewModel> newCollection = new ObservableCollection<SegmentItemViewModel>();

                        //foreach (var item in collection)
                        //{
                        //    newCollection.Add(item);
                        //}

                        //this.Collection = newCollection;

                        //this.Collection = new ObservableCollection<SegmentItemViewModel>(this.Collection.OrderBy(item => item.Author));
                    }
                    break;
                case "Loaded":
                    {
                        this.ButtonClickFunc("TextChanged");
                    }
                    break;
                case "SaveOther":
                    {
                        if (this.Selection == null) return;

                        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                        dlg.DefaultExt = ".snippet";
                        dlg.Filter = "snippet documents (.snippet)|*.snippet";
                        Nullable<bool> result = dlg.ShowDialog();

                        if (result == true)
                        {
                            string filename = dlg.FileName;

                            SegmentProvider.Instance.Save(this.Selection, filename);
                        }
                    }
                    break;
                case "Save":
                    {
                        if (this.Selection == null) return;

                        SegmentProvider.Instance.Save(this.Selection, this.Selection.FilePath);

                        this.ButtonClickFunc("Refresh");
                    }
                    break;
                case "Remove":
                    {
                        if (this.Selection == null) return;

                        var result = MessageWindow.ShowDialog("确定要移除[" + this.Selection.FileName + "]?");

                        if (!result) return;

                        File.Delete(this.Selection.FilePath);

                        this.Collection.Remove(this.Selection);
                    }
                    break;
                case "Add":
                    {
                        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                        dlg.DefaultExt = ".snippet";
                        dlg.Filter = "snippet documents (.snippet)|*.snippet";
                        dlg.InitialDirectory = this.Path;
                        Nullable<bool> result = dlg.ShowDialog();

                        if (result == true)
                        {
                            string filename = dlg.FileName;

                            SegmentItemViewModel model = new SegmentItemViewModel();
                            model.FileName = System.IO.Path.GetFileNameWithoutExtension(filename);
                            model.FilePath = filename;
                            model.Title = System.IO.Path.GetFileNameWithoutExtension(filename);
                            model.Shortcut = model.Title;
                            model.Description = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            SegmentProvider.Instance.Save(model, filename);

                            model = SegmentProvider.Instance.Create(filename);

                            this.Collection.Add(model);

                            this.Selection = model;

                        }
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

    partial class SegmentViewModel : NotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public SegmentViewModel()
        {
            RelayCommand = new RelayCommand(new Action<object>(ButtonClickFunc));

            this.Path = SegmentProvider.Instance.GetDefaultPath();
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

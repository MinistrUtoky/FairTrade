﻿using Fair_Trade.GameClasses;
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
using System.Windows.Shapes;

namespace Fair_Trade
{
    /// <summary>
    /// Логика взаимодействия для Download_Page.xaml
    /// </summary>
    public partial class Download_Page : Window
    {
        internal Download_Page()
        {
            InitializeComponent();
            GameMode.FormatWindow(this);
        }
    }
}

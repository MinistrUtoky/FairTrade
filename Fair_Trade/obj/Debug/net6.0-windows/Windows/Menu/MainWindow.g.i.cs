﻿#pragma checksum "..\..\..\..\..\Windows\Menu\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DD809F1CF13D0E5E6D806E0216BECE6CA9002B13"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Fair_Trade;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Fair_Trade {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas DancingShrek;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Main_Menu_Background;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Shop;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Start_Game;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Game_Settings;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Game_Exit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Fair_Trade;component/windows/menu/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DancingShrek = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.Main_Menu_Background = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.Shop = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
            this.Shop.Click += new System.Windows.RoutedEventHandler(this.Shop_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Start_Game = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
            this.Start_Game.Click += new System.Windows.RoutedEventHandler(this.Start_Game_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Game_Settings = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
            this.Game_Settings.Click += new System.Windows.RoutedEventHandler(this.Game_Settings_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Game_Exit = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\..\Windows\Menu\MainWindow.xaml"
            this.Game_Exit.Click += new System.Windows.RoutedEventHandler(this.Game_Exit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


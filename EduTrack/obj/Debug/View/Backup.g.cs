﻿#pragma checksum "..\..\..\View\Backup.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B691383B43301CD23E7E8FF9618913ABB534DC589F4FFDAC1017541FAD1E4E80"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EduTrack.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace EduTrack.View {
    
    
    /// <summary>
    /// Backup
    /// </summary>
    public partial class Backup : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\View\Backup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBox1;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\View\Backup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button downloadButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\View\Backup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button uploadButton;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\View\Backup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button importButton;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\View\Backup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock fileNameTextBlock;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EduTrack;component/view/backup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Backup.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.comboBox1 = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.downloadButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\View\Backup.xaml"
            this.downloadButton.Click += new System.Windows.RoutedEventHandler(this.downloadButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.uploadButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\View\Backup.xaml"
            this.uploadButton.Click += new System.Windows.RoutedEventHandler(this.uploadButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.importButton = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\View\Backup.xaml"
            this.importButton.Click += new System.Windows.RoutedEventHandler(this.importButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.fileNameTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


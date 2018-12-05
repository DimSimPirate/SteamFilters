﻿#pragma checksum "..\..\ViewFilters.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DF12F0B37B8393965D69384729CC64A04D17CA56"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SteamFilters;
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


namespace SteamFilters {
    
    
    /// <summary>
    /// ViewFilters
    /// </summary>
    public partial class ViewFilters : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\ViewFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem UserDetails;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ViewFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AvatarImage;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ViewFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UserNameText;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ViewFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AccountCreationDateText;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\ViewFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SteamFilters.ucSpinnerDotCircle LoadingSpinner;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\ViewFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView gamesListView;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\ViewFilters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label RLStatsLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/SteamFilters;component/viewfilters.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ViewFilters.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.UserDetails = ((System.Windows.Controls.TabItem)(target));
            return;
            case 2:
            
            #line 28 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UpdateUser);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 29 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ViewUserProfile);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AvatarImage = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.UserNameText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.AccountCreationDateText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 47 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UpdateGames);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 48 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SteamGameLink);
            
            #line default
            #line hidden
            return;
            case 9:
            this.LoadingSpinner = ((SteamFilters.ucSpinnerDotCircle)(target));
            return;
            case 10:
            this.gamesListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 11:
            
            #line 62 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.HeaderSort);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 67 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.HeaderSort);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 72 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.HeaderSort);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 77 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.HeaderSort);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 82 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.HeaderSort);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 95 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.Image)(target)).AddHandler(System.Windows.Documents.Hyperlink.ClickEvent, new System.Windows.RoutedEventHandler(this.RLStats));
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 96 "..\..\ViewFilters.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GetRocketLeagueStats);
            
            #line default
            #line hidden
            return;
            case 18:
            this.RLStatsLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

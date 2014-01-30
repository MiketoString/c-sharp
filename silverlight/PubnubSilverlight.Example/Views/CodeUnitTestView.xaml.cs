﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Silverlight.Testing.Controls;
using Microsoft.Silverlight.Testing.Client;

namespace PubnubSilverlight
{
    public partial class CodeUnitTestView : Page
    {
        public CodeUnitTestView()
        {
            InitializeComponent();

            var page = Instance.GetPage;

            ContainerForTest.Children.Add(page);

            TestPage unitTestPage = page as TestPage;
            Microsoft.Silverlight.Testing.Controls.TreeView resultsView = unitTestPage.TreeView;
            resultsView.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(TreeView_SelectedItemChanged);
        }

        void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
           var tree = sender as Microsoft.Silverlight.Testing.Controls.TreeView;

           tree.ExpandToDepth(1);
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}

﻿using LibDnaSerial;
using Microsoft.Win32;
using ModMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.DataVisualization.Charting.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (DataContext is IDisposable)
            {
                (DataContext as IDisposable).Dispose();
            }
            Application.Current.Shutdown();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainViewModel_SaveFileRequested(object sender, Events.SaveFileRequestedEventArgs args)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = args.Title;
            dialog.InitialDirectory = args.InitialDirectory;
            dialog.Filter = args.Filters;
            if (dialog.ShowDialog() == true)
            {
                args.Callback(dialog.FileName);
            }
        }

        private void MainViewModel_DevicePickerRequested(object sender, Events.DevicePickerRequestedEventArgs args)
        {
            new DevicePicker(args.Callback).ShowDialog();
        }

        private void MainViewModel_EditSettingsRequested(object sender, EventArgs args)
        {
            new SettingsWindow().ShowDialog();
        }

        private void MainViewModel_ShowAboutRequested(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        private void CrosshairContainer_MouseMove(object sender, MouseEventArgs e)
        {
            var crosshairGrid = sender as Grid;
            var p = e.GetPosition(crosshairGrid);
            var ctxt = DataContext as MainViewModel;
            ctxt.SetHoveredSample(p.X / crosshairGrid.ActualWidth);
        }

        private void CrosshairContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            var ctxt = DataContext as MainViewModel;
            ctxt.SetHoveredSample(-1);
        }

        private void MainViewModel_FirePromptRequested(object sender, Events.FirePromptRequestedEventArgs args)
        {
            new FireWindow(args.Callback, args.LastUsedDuration).ShowDialog();
        }

        private void MainViewModel_SetTemperaturePromptRequested(object sender, Events.SetTemperaturePromptRequestedEventArgs args)
        {
            new SetTemperatureWindow(args.Callback, args.CurrentTemperature).ShowDialog();
        }

        private void MainViewModel_SetPowerPromptRequested(object sender, Events.SetPowerPromptRequestedEventArgs args)
        {
            new SetPowerWindow(args.Callback, args.CurrentPower).ShowDialog();
        }

        private void MainViewModel_SetProfilePromptRequested(object sender, Events.SetProfilePromptRequestedEventArgs args)
        {
            new SetProfileWindow(args.Callback, args.CurrentProfile).ShowDialog();
        }

        private void MainViewModel_ConsoleRequested(object sender, Events.ConsoleRequestedEventArgs args)
        {
            new ConsoleWindow(args.Callback).Show();
        }

        private void MainViewModel_SaveStatisticsRequested(object sender, Events.SaveStatisticsRequestedEventArgs args)
        {
            var dlg = new SaveStatistics();
            dlg.NoteText = args.InitialValue;
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                args.Callback(dlg.NoteText);
            }
        }

        private void viewSavedStatisticsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ViewStatisticsWindow().Show();
        }
    }
}

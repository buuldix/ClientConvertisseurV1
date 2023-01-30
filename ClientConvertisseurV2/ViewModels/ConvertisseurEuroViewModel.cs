﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace ClientConvertisseurV2.ViewModels
{
    internal class ConvertisseurEuroViewModel : ObservableObject, INotifyPropertyChanged
    {
        public IRelayCommand BtnSetConversion { get; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Devise> devises;

        public ConvertisseurEuroViewModel()
        {
            GetDataOnLoadAsync();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }

       

        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set { devises = value; OnPropertyChanged("Devises"); }
        }

        private Devise? selectedDevise;

        public Devise? SelectedDevise
        {
            get { return selectedDevise; }
            set { selectedDevise = value; OnPropertyChanged("SelectedDevise"); }
        }


        private int montant;

        public int Montant
        {
            get { return montant; }
            set { montant = value; OnPropertyChanged("Montant"); }
        }
        private double? resultat;

        public double? Resultat
        {
            get { return resultat; }
            set { resultat = value; OnPropertyChanged("Resultat"); }
        }


        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:44340/api/");
            List<Devise> result = await service.GetDevisesAsync("Devises");
            /*if (result is null)
                ShowError("L'api n'est actuellement pas disponible.");
            else*/
            Devises = new ObservableCollection<Devise>(result);
        }

        private void ActionSetConversion()
        {
            Resultat = Montant * SelectedDevise.Taux;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
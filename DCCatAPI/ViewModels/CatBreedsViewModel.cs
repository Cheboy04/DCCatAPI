using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DCCatAPI.Models;

namespace DCCatAPI.ViewModels
{
    public class CatBreedsViewModel : INotifyPropertyChanged
    {
        private readonly ServicioApi _catApiService;
        public ObservableCollection<Cat> Breeds { get; set; } = new();
        public ObservableCollection<string> BreedNames { get; set; } = new(); // Lista de nombres de razas

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private string _selectedBreed;
        public string SelectedBreed
        {
            get => _selectedBreed;
            set
            {
                _selectedBreed = value;
                OnPropertyChanged();
                SearchBySelectedBreed(); // Buscar automáticamente cuando se seleccione una raza
            }
        }

        public Command SearchCommand { get; }

        public CatBreedsViewModel()
        {
            _catApiService = new ServicioApi();
            SearchCommand = new Command(async () => await SearchBreeds());
            LoadAllBreeds(); // Cargar todas las razas al inicializar el ViewModel
        }

        private async void LoadAllBreeds()
        {
            IsLoading = true;
            var breeds = await _catApiService.GetBreedsAsync();
            foreach (var breed in breeds)
            {
                BreedNames.Add(breed.Name); // Solo nombres para el Picker
            }
            IsLoading = false;
        }

        private async Task SearchBreeds()
        {
            IsLoading = true;
            var breeds = await _catApiService.GetBreedsAsync();
            Breeds.Clear();

            var filteredBreeds = string.IsNullOrEmpty(SearchText)
                ? breeds
                : breeds.Where(b => b.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var breed in filteredBreeds)
            {
                Breeds.Add(breed);
            }
            IsLoading = false;
        }

        private async void SearchBySelectedBreed()
        {
            if (string.IsNullOrEmpty(SelectedBreed)) return;

            IsLoading = true;
            var breeds = await _catApiService.GetBreedsAsync();
            Breeds.Clear();

            var selectedBreed = breeds.FirstOrDefault(b => b.Name.Equals(SelectedBreed, StringComparison.OrdinalIgnoreCase));
            if (selectedBreed != null)
            {
                Breeds.Add(selectedBreed); // Solo agrega la raza seleccionada
            }

            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using ReservaCanchas_Maui.AdminViews;
using ReservaCanchas_Maui.Models;
using ReservaCanchas_Maui.Repositories;
using System.Text.Json;

namespace ReservaCanchas_Maui.Views;

public partial class ComplejosPage : ContentPage
{
    private ComplejoRepository _repository;
    private List<Complejo> _complejos;
    private Usuario _usuario;

    public ComplejosPage(Usuario usuario)
    {
        InitializeComponent();
        _repository = new ComplejoRepository();
        _usuario = usuario;
        CargarComplejos();
        GenerarBotonesSuperUsuario();
    }

    private void CargarComplejos()
    {
        ComplejosCollection.ItemsSource = _repository.ObtenerTodosLosComplejos();
    }

    private async void OnComplejoSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Complejo seleccionado)
        {
            // Navega a la p�gina de listado de canchas asociadas
            await Navigation.PushAsync(new CanchasPage(seleccionado, _usuario));
        }
    }

    private void GenerarBotonesSuperUsuario()
    {
        // Generar botones solo si el usuario es un superusuario
        if (_usuario.Tipo == TipoDeUsuario.Superusuario)
        {
            // Bot�n para gestionar complejos
            var botonGestionarComplejos = new Button
            {
                Text = "Gestionar Complejos",
                BackgroundColor = Color.FromArgb("#007BFF"),
                TextColor = Colors.White,
                Margin = new Thickness(0, 10),
            };
            botonGestionarComplejos.Clicked += OnAdministracionComplejoSuperUser;

            // Bot�n para gestionar usuarios
            var botonGestionarUsuarios = new Button
            {
                Text = "Gestionar Usuarios",
                BackgroundColor = Color.FromArgb("#007BFF"),
                TextColor = Colors.White,
                Margin = new Thickness(0, 10),
            };
            botonGestionarUsuarios.Clicked += GestionUsuarios;

            // A�adir los botones al contenedor
            SuperUserButtonContainer.Children.Add(botonGestionarComplejos);
            SuperUserButtonContainer.Children.Add(botonGestionarUsuarios);

            // Hacer visible el contenedor
            SuperUserButtonContainer.IsVisible = true;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarComplejos();
    }

    private async void OnAdministracionComplejoSuperUser(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddComplejo());
    }

    private async void GestionUsuarios(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GestionarUsers());
    }
}

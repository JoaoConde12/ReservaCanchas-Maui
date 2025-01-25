using ReservaCanchas_Maui.Models;
using ReservaCanchas_Maui.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ReservaCanchas_Maui.AdminViews
{
    public partial class GestionarUsers : ContentPage
    {
        public ObservableCollection<Usuario> Usuarios { get; set; }

        public ICommand EliminarUsuarioCommand { get; private set; }

        public GestionarUsers()
        {
            InitializeComponent();

            Usuarios = new ObservableCollection<Usuario>(App._usuarioRepository.ObtenerTodosLosUsuarios());
            EliminarUsuarioCommand = new Command<int>(EliminarUsuario);

            BindingContext = this;
        }

        private async void EliminarUsuario(int idUsuario)
        {
            try
            {
                // Mostrar alerta de confirmaci�n
                bool confirmacion = await DisplayAlert(
                    "Confirmaci�n",
                    "�Est�s seguro de que deseas eliminar este usuario?",
                    "S�",
                    "No"
                );

                // Si el usuario confirma, procede con la eliminaci�n
                if (confirmacion)
                {
                    // Llama al repositorio para eliminar el usuario
                    App._usuarioRepository.EliminarUsuario(idUsuario);

                    // Elimina de la colecci�n observable
                    var usuario = Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
                    if (usuario != null)
                    {
                        Usuarios.Remove(usuario);
                    }

                    // Opcional: Mostrar mensaje de �xito
                    await DisplayAlert("�xito", "Usuario eliminado correctamente.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                // Mostrar mensaje de error
                await DisplayAlert("Error", "Ocurri� un error al intentar eliminar el usuario.", "OK");
            }
        }


    }
}

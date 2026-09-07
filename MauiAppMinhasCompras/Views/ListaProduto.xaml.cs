using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        // ObservableCollection notifica automaticamente a UI quando itens são adicionados/removidos
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public ListaProduto()
        {
            InitializeComponent();

            // Vincula a lista à ListView
            lst_produtos.ItemsSource = lista;
        }

        // OnAppearing é chamado sempre que a página aparece na tela
        // Ideal para recarregar os dados após inserção ou edição
        protected async override void OnAppearing()
        {
            try
            {
                lista.Clear();

                // Busca todos os produtos no banco de dados
                List<Produto> tmp = await App.Db.GetAll();

                // Adiciona cada produto na ObservableCollection (atualiza a UI automaticamente)
                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        // Evento do botão "+" — navega para a tela de cadastro
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }

        // Evento de busca em tempo real: filtrado a cada caractere digitado
        private async void search_bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string q = e.NewTextValue;

                lista.Clear();

                List<Produto> tmp = await App.Db.Search(q);
                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        // Evento de seleção de item — navega para a tela de edição (será implementada na Agenda 5)
        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Reservado para navegação para EditarProduto (Agenda 5)
        }
    }
}

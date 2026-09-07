using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        // Evento disparado ao clicar em "Salvar" na barra de ferramentas
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Cria um novo objeto Produto com os valores digitados pelo usuário
                Produto p = new Produto
                {
                    Descricao  = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_quantidade.Text),
                    Preco      = Convert.ToDouble(txt_preco.Text)
                };

                // Chama o método Insert do banco de dados via padrão Singleton
                await App.Db.Insert(p);

                // Exibe alerta de sucesso para o usuário
                await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

                // Retorna para a tela anterior (ListaProduto)
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Caso ocorra qualquer erro, exibe a mensagem sem travar o app
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}

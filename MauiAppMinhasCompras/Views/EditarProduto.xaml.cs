using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto()
        {
            InitializeComponent();
        }

        // Evento disparado ao clicar em "Salvar" na barra de ferramentas
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Recupera o produto que foi passado via BindingContext da tela anterior
                Produto produto_anexado = BindingContext as Produto;

                // Cria um novo objeto Produto com os valores editados, mantendo o mesmo Id
                Produto p = new Produto
                {
                    Id         = produto_anexado.Id,
                    Descricao  = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_quantidade.Text),
                    Preco      = Convert.ToDouble(txt_preco.Text)
                };

                // Chama o método Update do banco de dados
                await App.Db.Update(p);

                // Exibe alerta de sucesso
                await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

                // Retorna para a tela anterior (ListaProduto)
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}

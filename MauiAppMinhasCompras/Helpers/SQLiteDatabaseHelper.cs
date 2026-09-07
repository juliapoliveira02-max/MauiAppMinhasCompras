using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        // Conexão assíncrona com o banco de dados SQLite
        // readonly garante que a conexão não seja substituída acidentalmente
        readonly SQLiteAsyncConnection _conn;

        // Construtor: inicializa a conexão e cria a tabela Produto se não existir
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        // =====================
        // MÉTODO INSERT (Create)
        // =====================
        // Insere um novo registro do tipo Produto no banco de dados SQLite.
        // Retorna Task<int>: número de linhas afetadas (1 se bem-sucedido).
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        // =====================
        // MÉTODO UPDATE (Update)
        // =====================
        // Atualiza um registro existente na tabela Produto com base no Id.
        // A consulta SQL atualiza Descricao, Quantidade e Preco do registro correspondente.
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        // =====================
        // MÉTODO DELETE (Delete)
        // =====================
        // Remove um registro da tabela Produto com base no Id fornecido.
        // Utiliza a funcionalidade assíncrona do SQLite para não bloquear a thread principal.
        // Retorna Task<int>: 1 se excluiu com sucesso, 0 se não encontrou o registro.
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // =====================
        // MÉTODO GETALL (Read - todos)
        // =====================
        // Retorna todos os registros da tabela Produto.
        // Equivalente a "SELECT * FROM Produto" em SQL.
        // A operação é assíncrona e não bloqueia a thread principal.
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        // =====================
        // MÉTODO SEARCH (Read - busca)
        // =====================
        // Filtra registros da tabela Produto pela descrição usando o operador LIKE.
        // Exemplo: Search("cadeira") retorna todos os produtos que contêm "cadeira" na descrição.
        // O % é um coringa: qualquer coisa pode aparecer antes ou depois da string buscada.
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }
    }
}

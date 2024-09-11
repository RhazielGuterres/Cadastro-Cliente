using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using ProjetoX.Models;

namespace CadastroDeCliente.DAO
{
    public class ClienteDAO
    {
        private readonly string _connectionString;

        public ClienteDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void inserir(Cliente cliente)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Clientes (Nome, Email, Telefone, Endereco, Data) VALUES (@Nome, @Email, @Telefone, @Endereco, @Data);";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Adiciona os parâmetros para evitar SQL Injection
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    command.Parameters.AddWithValue("@Data", cliente.Data);

                    // Executa o comando de inserção
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> ListarTodos()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Clientes;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.IsDBNull(reader.GetOrdinal("Nome")) ? null : reader.GetString("Nome"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                Data = reader.IsDBNull(reader.GetOrdinal("Data")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Data"))
                            };

                            listaClientes.Add(cliente);
                        }
                    }
                }
            }

            return listaClientes;
        }


        public Cliente buscarPorId(int id)
        {
            Cliente cliente = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Clientes WHERE Id = @Id;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.IsDBNull(reader.GetOrdinal("Nome")) ? null : reader.GetString("Nome"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                Data = reader.IsDBNull(reader.GetOrdinal("Data")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Data"))

                            };
                        }
                    }
                }
            }

            return cliente;
        }

        public void DeletarPorId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Clientes WHERE Id = @Id;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public void atualizar(Cliente cliente)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Clientes SET Nome = @Nome, Email = @Email, Telefone = @Telefone, Endereco = @Endereco, Data = @Data WHERE Id = @Id;";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Adiciona os parâmetros
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    command.Parameters.AddWithValue("@Data", cliente.Data);
                    command.Parameters.AddWithValue("@Id", cliente.Id);

                    // Executa o comando de atualização
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
    }
}

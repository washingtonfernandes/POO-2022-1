using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AULA14_Async_Model.Model;

namespace AULA14_Async_Model.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> BuscaUsuarios();
        Task<Usuario> BuscaUsuario(int Id);
        void AdicionaUsuario(Usuario usuario);
        void AtualizaUsuario(Usuario usuario);
        void DeletaUsuario(Usuario usuario);

        Task<bool> SaveChangesAsync();

    }
}
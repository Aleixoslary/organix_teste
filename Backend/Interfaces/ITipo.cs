using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;

namespace Backend.Interfaces
{
    public interface ITipo
    {
         Task<List<Tipo>> Listar();

         Task<Tipo> BuscarPorId(int id);
         
         Task<Tipo> Salvar(Tipo tipo);

         Task<Tipo> Alterar(Tipo tipo);

         Task<Tipo> Excluir(Tipo tipo);
    }
}
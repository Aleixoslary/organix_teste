using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;

namespace Backend.Interfaces
{
    public interface ICategoriaReceita
    {
         Task<List<CategoriaReceita>> Listar();

         Task<CategoriaReceita> BuscarPorId(int id);
         
         Task<CategoriaReceita> Salvar(CategoriaReceita categoriaReceita);

         Task<CategoriaReceita> Alterar(CategoriaReceita categoriaReceita);

         Task<CategoriaReceita> Excluir(CategoriaReceita categoriaReceita);
    }
}
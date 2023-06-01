﻿using eAgenda.Dominio;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Orm.ModuloDespesa
{
    public class RepositorioDespesaOrm : RepositorioBase<Despesa>, IRepositorioDespesa
    {
        public RepositorioDespesaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public List<Despesa> SelecionarDespesasUltimos30Dias(DateTime dataAtual, Guid usuarioId)
        {
            return registros
               .Where(x => x.Data >= dataAtual.AddDays(-30))
               .Where(x => x.UsuarioId.Equals(usuarioId))
               .ToList();
        }

        public List<Despesa> SelecionarDespesasAntigas(DateTime dataAtual, Guid usuarioId)
        {
            return registros
               .Where(x => x.Data <= dataAtual.AddDays(-30))
               .Where(x => x.UsuarioId.Equals(usuarioId))
               .ToList();
        }

        public override Despesa SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Categorias)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}
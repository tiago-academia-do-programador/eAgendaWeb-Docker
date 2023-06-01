﻿using eAgenda.Dominio;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Orm.ModuloCompromisso
{
    public class RepositorioCompromissoOrm : RepositorioBase<Compromisso>, IRepositorioCompromisso
    {        
        public RepositorioCompromissoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }
      
        public override Compromisso SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Contato)
                .SingleOrDefault(x => x.Id == id);
        }

        public override List<Compromisso> SelecionarTodos(Guid usuarioId = new Guid())
        {
            return registros
                .Include(x => x.Contato)
                .Where(x => x.UsuarioId.Equals(usuarioId))
                .ToList();
        }

        public List<Compromisso> SelecionarCompromissosFuturos(DateTime dataInicial, DateTime dataFinal, Guid usuarioId = new Guid())
        {
            return registros
                .Include(x => x.Contato)
                .Where(x => x.Data >= dataInicial)
                .Where(x => x.Data <= dataFinal)
                .Where(x => x.UsuarioId.Equals(usuarioId))
                .ToList();
        }

        public List<Compromisso> SelecionarCompromissosPassados(DateTime dataDeHoje, Guid usuarioId = new Guid())
        {
            return registros
                .Include(x => x.Contato)
                .Where(x => x.Data < dataDeHoje)
                .Where(x => x.UsuarioId.Equals(usuarioId))
                .ToList();
        }
    }
}
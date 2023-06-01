using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;

namespace eAgenda.Infra.Orm
{
    public static class MigradorBancoDadoseAgenda
    {
        public static bool AtualizarBancoDados(DbContext db)
        {
            try
            {
                var qtdMigracoesPendentes = db.Database.GetPendingMigrations().Count();

                if (qtdMigracoesPendentes == 0)
                    return false;

                db.Database.Migrate();

                return true;
            }
            catch (Exception)
            {
                Thread.Sleep(TimeSpan.FromSeconds(15));

                AtualizarBancoDados(db);
            }

            return false;
        }
    }
}

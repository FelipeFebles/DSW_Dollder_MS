using DSW_ApiNoConformidades_Dollder_MS.Core.Database;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Usuario;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;

public class ApiDbContext : DbContext , IApiDbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public DbSet<AccionesEntity> Acciones { get; set; }
    public DbSet<PreventivasEntity> Preventivas { get; set; }
    public DbSet<CorrectivasEntity> Correctivas { get; set; }
    public DbSet<AdministradorEntity> Administrador { get; set; }
    public DbSet<CalidadEntity> Calidad { get; set; }
    public DbSet<CalendarioEntity> Calendario { get; set; }
    public DbSet<CierreEntity> Cierre { get; set; }
    public DbSet<DepartamentoEntity> Departamento { get;set; }
    public DbSet<IndicadoresEntity> Indicadores { get; set; }
    public DbSet<NoConformidadEntity> NoConformidad { get; set; }
    public DbSet<NotificacionEntity> Notificacion { get; set; }
    public DbSet<OperarioEntity> Operario { get; set; }
    public DbSet<ReporteEntity> Reporte { get; set; }
    public DbSet<ImagenReporteEntity> ImagenReporte { get; set; }
    public DbSet<ResponsableEntity> Responsable { get;set; }
    public DbSet<R_Acciones_UsuarioEntity> R_Acciones_Usuario { get; set; }
    public DbSet<R_Calidad_NoConformidadEntity> R_Calidad_NoConformidad { get; set; }
    public DbSet<R_Indicadores_CausasEntity> R_Indicadores_Causas { get; set; }
    public DbSet<IndicadorOrigenEntity> IndicadorOrigen { get; set; }
    public DbSet<IndicadorCausaEntity> IndicadorCausa { get; set; }

    public DbSet<RevisionReporteEntity> RevisionReporte { get; set; }
    public DbSet<SeguimientoEntity> Seguimiento {  get; set; }
    public DbSet<UsuarioEntity> Usuario { get; set; }
    public DbSet<VerificacionEfectividadEntity> VerificacionEfectividad { get; set; }


    public DbContext DbContext
    {
        get
        {
            return this;
        }
    }

    
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///             RELACIONES FK
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre NoConformidadEntity y El ENUMERADO Estado
        modelBuilder.Entity<NoConformidadEntity>()
            .Property(n => n.estado)
            .HasConversion(
                v => (int)v,
                v => (Estado)v
            );



        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre ImagenReporteEntity y ReporteEntity
        modelBuilder.Entity<ImagenReporteEntity>()
            .HasOne(r => r.reporte)
            .WithMany()
            .HasForeignKey(r => r.reporte_Id);

        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre AccionEntity y ResponsableEntity
        modelBuilder.Entity<AccionesEntity>()
            .HasOne(r => r.responsable)
            .WithMany()
            .HasForeignKey(r => r.responsable_Id);

        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre PreventivasEntity y CorrectivasEntity
        modelBuilder.Entity<PreventivasEntity>()
            .HasOne(r => r.correctiva)
            .WithMany()
            .HasForeignKey(r => r.correctiva_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre CierreEntity y NoConformidadEntity
        modelBuilder.Entity<CierreEntity>()
            .HasOne(r => r.noConformidad)
            .WithMany()
            .HasForeignKey(r => r.noConformidad_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre UsuarioEntity y DepartamentoEntity 
        modelBuilder.Entity<UsuarioEntity>()
            .HasOne(r => r.departamento)
            .WithMany()
            .HasForeignKey(r => r.departamento_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre IndicadoresEntity y CierreEntity
        modelBuilder.Entity<IndicadoresEntity>()
            .HasOne(r => r.cierre)
            .WithMany()
            .HasForeignKey(r => r.cierre_Id);

        modelBuilder.Entity<IndicadoresEntity>()
            .HasOne(r => r.origen)
            .WithMany()
            .HasForeignKey(r => r.origen_Id);

        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre IndicadoresEntity y CierreEntity
        modelBuilder.Entity<R_Indicadores_CausasEntity>()
            .HasOne(r => r.causa)
            .WithMany()
            .HasForeignKey(r => r.causa_Id);

        modelBuilder.Entity<R_Indicadores_CausasEntity>()
            .HasOne(r => r.indicador)
            .WithMany()
            .HasForeignKey(r => r.indicador_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre R_calidad_NoConformidadEntity y CalidadEntity
        modelBuilder.Entity<R_Calidad_NoConformidadEntity>()
            .HasOne(r => r.calidad)
            .WithMany()
            .HasForeignKey(r => r.calidad_Id);

        // Configurar la relación entre R_calidad_NoConformidadEntity y no Condormidad
        modelBuilder.Entity<R_Calidad_NoConformidadEntity>()
            .HasOne(r => r.noConformidad)
            .WithMany()
            .HasForeignKey(r => r.noConformidad_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre NoConformidadEntity y ReporteEntity
        modelBuilder.Entity<NoConformidadEntity>()
            .HasOne(r => r.reporte)
            .WithMany()
            .HasForeignKey(r => r.reporte_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre ReporteEntity y UsuariosEntity
        modelBuilder.Entity<ReporteEntity>()
            .HasOne(r => r.usuario)
            .WithMany()
            .HasForeignKey(r => r.usuario_Id);



        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre ResponsableEntity y NoConformidadEntity
        modelBuilder.Entity<ResponsableEntity>()
            .HasOne(r => r.noConformidad)
            .WithMany()
            .HasForeignKey(r => r.noConformidad_Id);

        modelBuilder.Entity<ResponsableEntity>()
            .HasOne(r => r.usuario)
            .WithMany()
            .HasForeignKey(r => r.usuario_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre R_AccionesCorrectivasEntity y AccionCorrectivaEntity
        modelBuilder.Entity<R_Acciones_UsuarioEntity>()
            .HasOne(r => r.acciones)
            .WithMany()
            .HasForeignKey(r => r.acciones_Id);

        // Configurar la relación entre R_AccionesCorrectivasEntity y UsuarioEntity
        modelBuilder.Entity<R_Acciones_UsuarioEntity>()
            .HasOne(r => r.usuario)
            .WithMany()
            .HasForeignKey(r => r.usuario_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre RevisionReporteEntity y ReporteEntity
        modelBuilder.Entity<RevisionReporteEntity>()
            .HasOne(r => r.reporte)
            .WithMany()
            .HasForeignKey(r => r.reporte_Id);

        // Configurar la relación entre RevisionReporteEntity y UsuarioEntity
        modelBuilder.Entity<RevisionReporteEntity>()
            .HasOne(r => r.usuario)
            .WithMany()
            .HasForeignKey(r => r.usuario_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre SeguimientoEntity y NoConformidadEntity
        modelBuilder.Entity<SeguimientoEntity>()
            .HasOne(r => r.noConformidad)
            .WithMany()
            .HasForeignKey(r => r.noConformidad_Id);


        ///////////////////////////////////////////////////////////////////////////////
        // Configurar la relación entre VerificacionEfectividadEntity y CierreEntity
        modelBuilder.Entity<VerificacionEfectividadEntity>()
            .HasOne(r => r.cierre)
            .WithMany()
            .HasForeignKey(r => r.cierre_Id);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///   Inicia una micro transaccion contra la BD
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public IDbContextTransactionProxy BeginTransaction()
    {
        return new DbContextTransactionProxy(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///   No cambiar el estado en la BD, ejemplo: no quiero que el nombre se pueda actualizar una vez enlazado
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    virtual public void SetPropertyIsModifiedToFalse<TEntity, TProperty>(TEntity entity,
        Expression<Func<TEntity, TProperty>> propertyExpression) where TEntity : class
    {
        Entry(entity).Property(propertyExpression).IsModified = false;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///   Cambia el estado de una entidad (added, Deleted, Detached, Modified, Unchanged)
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    virtual public void ChangeEntityState<TEntity>(TEntity entity, EntityState state)
    {
        if (entity != null)
        {
            Entry(entity).State = state;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///   Genera el ID, y los create y update de BaseEntity
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                Entry((BaseEntity)entityEntry.Entity).Property(x => x.CreatedAt).IsModified = false;
                Entry((BaseEntity)entityEntry.Entity).Property(x => x.CreatedBy).IsModified = false;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(string user, CancellationToken cancellationToken = default)
    {
        var state = new List<EntityState> { EntityState.Added, EntityState.Modified };

        var entries = ChangeTracker.Entries().Where(e =>
            e.Entity is BaseEntity && state.Any(s => e.State == s)
        );

        var dt = DateTime.UtcNow;

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedAt = dt;
                entity.CreatedBy = user;
                Entry(entity).Property(x => x.UpdatedAt).IsModified = false;
                Entry(entity).Property(x => x.UpdatedBy).IsModified = false;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entity.UpdatedAt = dt;
                entity.UpdatedBy = user;
                Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                Entry(entity).Property(x => x.CreatedBy).IsModified = false;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveEfContextChanges(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken) >= 0;
    }

    public async Task<bool> SaveEfContextChanges(string user, CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(user, cancellationToken) >= 0;
    }




}

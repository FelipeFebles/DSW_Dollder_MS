using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    dia = table.Column<int>(type: "integer", nullable: true),
                    mes = table.Column<int>(type: "integer", nullable: true),
                    anio = table.Column<int>(type: "integer", nullable: true),
                    titulo = table.Column<string>(type: "text", nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    cargo = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicadorCausa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    causa = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadorCausa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndicadorOrigen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    origen = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicadorOrigen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: true),
                    envia = table.Column<string>(type: "text", nullable: true),
                    dirigido = table.Column<string>(type: "text", nullable: true),
                    mensaje = table.Column<string>(type: "text", nullable: true),
                    revisado = table.Column<bool>(type: "boolean", nullable: true),
                    tipo = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario = table.Column<string>(type: "text", nullable: true),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    apellido = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    correo = table.Column<string>(type: "text", nullable: true),
                    preguntas_de_seguridad = table.Column<string>(type: "text", nullable: true),
                    preguntas_de_seguridad2 = table.Column<string>(type: "text", nullable: true),
                    respuesta_de_seguridad = table.Column<string>(type: "text", nullable: true),
                    respuesta_de_seguridad2 = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    departamento_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Departamento_departamento_Id",
                        column: x => x.departamento_Id,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reporte",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    detectado_por = table.Column<string>(type: "text", nullable: true),
                    area = table.Column<string>(type: "text", nullable: true),
                    titulo = table.Column<string>(type: "text", nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    departamento_emisor = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    usuario_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reporte_Usuario_usuario_Id",
                        column: x => x.usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagenReporte",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    imagen = table.Column<byte[]>(type: "bytea", nullable: true),
                    nombre_archivo = table.Column<string>(type: "text", nullable: true),
                    reporte_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenReporte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagenReporte_Reporte_reporte_Id",
                        column: x => x.reporte_Id,
                        principalTable: "Reporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoConformidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    numero_expedicion = table.Column<string>(type: "text", nullable: true),
                    revisado_por = table.Column<string>(type: "text", nullable: true),
                    consecuencias = table.Column<string>(type: "text", nullable: true),
                    responsables_cargo = table.Column<List<string>>(type: "text[]", nullable: true),
                    areas_involucradas = table.Column<List<string>>(type: "text[]", nullable: true),
                    prioridad = table.Column<int>(type: "integer", nullable: true),
                    estado = table.Column<int>(type: "integer", nullable: false),
                    reporte_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoConformidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoConformidad_Reporte_reporte_Id",
                        column: x => x.reporte_Id,
                        principalTable: "Reporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RevisionReporte",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    reporte_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevisionReporte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RevisionReporte_Reporte_reporte_Id",
                        column: x => x.reporte_Id,
                        principalTable: "Reporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RevisionReporte_Usuario_usuario_Id",
                        column: x => x.usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cierre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    conforme = table.Column<bool>(type: "boolean", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    responsable = table.Column<string>(type: "text", nullable: true),
                    fecha_verificacion = table.Column<string>(type: "text", nullable: true),
                    noConformidad_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cierre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cierre_NoConformidad_noConformidad_Id",
                        column: x => x.noConformidad_Id,
                        principalTable: "NoConformidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "R_Calidad_NoConformidad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    calidad_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    noConformidad_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_Calidad_NoConformidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_R_Calidad_NoConformidad_NoConformidad_noConformidad_Id",
                        column: x => x.noConformidad_Id,
                        principalTable: "NoConformidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_R_Calidad_NoConformidad_Usuario_calidad_Id",
                        column: x => x.calidad_Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responsable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    investigacion = table.Column<string>(type: "text", nullable: true),
                    analisis_causa = table.Column<List<string>>(type: "text[]", nullable: true),
                    correccion = table.Column<string>(type: "text", nullable: true),
                    analisis_riesgo = table.Column<string>(type: "text", nullable: true),
                    cargo_responsable = table.Column<string>(type: "text", nullable: true),
                    fecha_compromiso = table.Column<string>(type: "text", nullable: true),
                    usuario_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    noConformidad_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsable_NoConformidad_noConformidad_Id",
                        column: x => x.noConformidad_Id,
                        principalTable: "NoConformidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Responsable_Usuario_usuario_Id",
                        column: x => x.usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seguimiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    fecha_seguimiento = table.Column<string>(type: "text", nullable: true),
                    estatus = table.Column<string>(type: "text", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    realizado_por = table.Column<string>(type: "text", nullable: true),
                    noConformidad_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seguimiento_NoConformidad_noConformidad_Id",
                        column: x => x.noConformidad_Id,
                        principalTable: "NoConformidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Indicadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    cierre_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    origen_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indicadores_Cierre_cierre_Id",
                        column: x => x.cierre_Id,
                        principalTable: "Cierre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Indicadores_IndicadorOrigen_origen_Id",
                        column: x => x.origen_Id,
                        principalTable: "IndicadorOrigen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerificacionEfectividad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    efectiva = table.Column<bool>(type: "boolean", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    realizado_por = table.Column<string>(type: "text", nullable: true),
                    cierre_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificacionEfectividad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificacionEfectividad_Cierre_cierre_Id",
                        column: x => x.cierre_Id,
                        principalTable: "Cierre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    investigacion = table.Column<string>(type: "text", nullable: true),
                    area = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    visto_bueno = table.Column<bool>(type: "boolean", nullable: true),
                    cargo_usuario = table.Column<string>(type: "text", nullable: true),
                    fecha_compromiso = table.Column<string>(type: "text", nullable: true),
                    responsable_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    correctiva_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acciones_Acciones_correctiva_Id",
                        column: x => x.correctiva_Id,
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Acciones_Responsable_responsable_Id",
                        column: x => x.responsable_Id,
                        principalTable: "Responsable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "R_Indicadores_Causas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    causa_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    indicador_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_Indicadores_Causas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_R_Indicadores_Causas_IndicadorCausa_causa_Id",
                        column: x => x.causa_Id,
                        principalTable: "IndicadorCausa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_R_Indicadores_Causas_Indicadores_indicador_Id",
                        column: x => x.indicador_Id,
                        principalTable: "Indicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "R_Acciones_Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    acciones_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_Acciones_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_R_Acciones_Usuario_Acciones_acciones_Id",
                        column: x => x.acciones_Id,
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_R_Acciones_Usuario_Usuario_usuario_Id",
                        column: x => x.usuario_Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_correctiva_Id",
                table: "Acciones",
                column: "correctiva_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Acciones_responsable_Id",
                table: "Acciones",
                column: "responsable_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cierre_noConformidad_Id",
                table: "Cierre",
                column: "noConformidad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenReporte_reporte_Id",
                table: "ImagenReporte",
                column: "reporte_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_cierre_Id",
                table: "Indicadores",
                column: "cierre_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Indicadores_origen_Id",
                table: "Indicadores",
                column: "origen_Id");

            migrationBuilder.CreateIndex(
                name: "IX_NoConformidad_reporte_Id",
                table: "NoConformidad",
                column: "reporte_Id");

            migrationBuilder.CreateIndex(
                name: "IX_R_Acciones_Usuario_acciones_Id",
                table: "R_Acciones_Usuario",
                column: "acciones_Id");

            migrationBuilder.CreateIndex(
                name: "IX_R_Acciones_Usuario_usuario_Id",
                table: "R_Acciones_Usuario",
                column: "usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_R_Calidad_NoConformidad_calidad_Id",
                table: "R_Calidad_NoConformidad",
                column: "calidad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_R_Calidad_NoConformidad_noConformidad_Id",
                table: "R_Calidad_NoConformidad",
                column: "noConformidad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_R_Indicadores_Causas_causa_Id",
                table: "R_Indicadores_Causas",
                column: "causa_Id");

            migrationBuilder.CreateIndex(
                name: "IX_R_Indicadores_Causas_indicador_Id",
                table: "R_Indicadores_Causas",
                column: "indicador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reporte_usuario_Id",
                table: "Reporte",
                column: "usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Responsable_noConformidad_Id",
                table: "Responsable",
                column: "noConformidad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Responsable_usuario_Id",
                table: "Responsable",
                column: "usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RevisionReporte_reporte_Id",
                table: "RevisionReporte",
                column: "reporte_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RevisionReporte_usuario_Id",
                table: "RevisionReporte",
                column: "usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimiento_noConformidad_Id",
                table: "Seguimiento",
                column: "noConformidad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_departamento_Id",
                table: "Usuario",
                column: "departamento_Id");

            migrationBuilder.CreateIndex(
                name: "IX_VerificacionEfectividad_cierre_Id",
                table: "VerificacionEfectividad",
                column: "cierre_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendario");

            migrationBuilder.DropTable(
                name: "ImagenReporte");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "R_Acciones_Usuario");

            migrationBuilder.DropTable(
                name: "R_Calidad_NoConformidad");

            migrationBuilder.DropTable(
                name: "R_Indicadores_Causas");

            migrationBuilder.DropTable(
                name: "RevisionReporte");

            migrationBuilder.DropTable(
                name: "Seguimiento");

            migrationBuilder.DropTable(
                name: "VerificacionEfectividad");

            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropTable(
                name: "IndicadorCausa");

            migrationBuilder.DropTable(
                name: "Indicadores");

            migrationBuilder.DropTable(
                name: "Responsable");

            migrationBuilder.DropTable(
                name: "Cierre");

            migrationBuilder.DropTable(
                name: "IndicadorOrigen");

            migrationBuilder.DropTable(
                name: "NoConformidad");

            migrationBuilder.DropTable(
                name: "Reporte");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Departamento");
        }
    }
}

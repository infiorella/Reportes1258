namespace Informes
{
    using Models;
    using Models.Reports;
    using QuestPDF.Drawing;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class TemplateExtraordinaria : IDocument
    {
        public LNActaSesionExtraordinaria sesion { get; set; }
        public TemplateExtraordinaria(LNActaSesionExtraordinaria reunion)
        {
            this.sesion = reunion;
        }


        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        //Página
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);


                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        //Cabecera estática
        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
                var rutaImagen = Path.Combine("wwwroot/img/logo.jpg");
                byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);

                //Primer cuadro
                row.ConstantItem(90).PaddingLeft(35).Image(imageData);

                //Segundo Cuadro
                row.RelativeItem().Column(col =>
                {
                    col.Item().PaddingBottom(5).Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(13).Bold());
                        text.AlignCenter();

                        text.Span("Acta de Sesión Extraordinaria N° " + sesion.IdInforme);
                    });

                    col.Item().Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(12));
                        text.AlignCenter();

                        text.Span("C.M./I.E: 1258 Sebastián Lorente");
                    });
                    col.Item().Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(12));
                        text.AlignCenter();

                        text.Span("DRE/UGEL: Lima Metropolitana");
                    });
                });

                row.ConstantItem(80).Padding(0);
            });
        }

        //Contenido
        void ComposeContent(IContainer container)
        {
            container.PaddingRight(35).PaddingLeft(35).PaddingTop(20)
                .Column(col1 =>
                {

                    col1.Item().LineHorizontal(0.5f);

                    //Parrafo
                    col1.Item().PaddingTop(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12).LineHeight(2f));
                        txt.Span("En el aula del " + sesion.Grado + " '" + sesion.Seccion + "'" +
                            "siendo las " + sesion.FechaReunion.Value.ToString("HH:mm") + " del día " + sesion.FechaReunion.Value.ToString("dd MMMM yyyy") +
                            ", se reunieron los padres de familia a la convocatoria del docente " + sesion.DocenteNombre + "."
                            ); ;
                    });


                    col1.Item().PaddingLeft(15).Column(column =>
                    {
                        column.Item().Row(row =>
                            {
                                row.Spacing(5);
                                row.RelativeItem().Text("La agenda de la reunión fue la siguiente: "+ sesion.Agenda);
                            });

                    });

                    col1.Item().PaddingLeft(15).Column(column =>
                    {
                        foreach (var i in sesion.Padres)
                        {
                            column.Item().Row(row =>
                            {
                                row.Spacing(5);
                                row.AutoItem().Text("*"); // text or image
                                row.RelativeItem().Text(i);
                            });
                        }
                    });


                    //Condicion Entrega Libros
                    if (sesion.Entrega == 1)
                    {
                        col1.Item().Text("Luego del saludo correspondiente al profesor da lectura del acta de entrega de los texto para los estudiantes por parte de la comisión de gestión pedagógica en la cual indica que ha sido asignado los textos para la totalidad de estudiantes").LineHeight(2f);
                    }


                    //Conformidad de padres
                    if (sesion.ConformidadPadres == 1)
                    {
                        col1.Item().Text("Los padres de familia por unanimidad expresan su conformidad y apoyo al docente para que desarrolle actividades de aprendizaje significativas para los estudiantes y " +
                                                "estarán prestos en la adquisición de nuevos materiales educativos durante el año escolar.").LineHeight(2f);
                        col1.Item().Text("El docente agradece a los padres de familia por su apoyo.").LineHeight(2f);

                    }

                    //Si todos los padres firmaron
                    col1.Item().Text("Finalmente todos los padres firmaron el padrón de entrega de textos de textos en señal de conformidad ").LineHeight(2f);
                    


                    col1.Item().Text("Sin otro particular me suscribo a usted.").LineHeight(2f);

                    col1.Item().AlignCenter().PaddingTop(40).Text("Atentamente,").Bold();
                    col1.Item().AlignCenter().PaddingTop(10).Text("Docente " + sesion.DocenteNombre).SemiBold(); /******Nombre docente*****/

                    if (sesion.Comentarios != null)
                    {
                        col1.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                        {
                            col.Item().Text("Comentarios").FontSize(12).Bold();
                            col.Item().Text(sesion.Comentarios);
                            col.Spacing(4);
                        });
                    }
                });
        }
    }
}

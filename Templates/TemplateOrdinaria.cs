namespace Informes
{
    using Models;
    using Models.Reports;
    using Models.Views;
    using QuestPDF.Drawing;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class TemplateOrdinaria : IDocument
    {
        public LNActaSesionOrdinaria sesion { get; set; }
        public TemplateOrdinaria(LNActaSesionOrdinaria sesion)
        {
            this.sesion = sesion;
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
                var rutaImagen = Path.Combine("wwwroot/images/logo.jpg");
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

                        text.Span("Acta de Sesión Ordinaria N° "+ sesion.IdInforme);
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

                    col1.Item().PaddingTop(5).LineHorizontal(0.5f);

                    //Parrafo
                    col1.Item().PaddingTop(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12).LineHeight(2f));
                        txt.Span("En el aula del " + sesion.Seccion +
                            "siendo las " + sesion.Fecha.Value.ToString("HH:mm") + " del día " + sesion.Fecha.Value.ToString("dd MMMM yyyy") +
                            ", se reunieron los padres de faamilia a la convocatoria del profesor "+ sesion.DocenteNombre+ ", "+
                            "para realizar la entrega del Informe de Progreso (libreta de notas), correspondiente al "+sesion.Trimestre+" trimestre del presente año."); ;
                    });

                    col1.Item().Text("Luego del saludo del profesor, se informa del resultado de las evaluaciones, indicando que los padres de familia cuyos hijos tengan el nivel de logro: Inicio y Proceso, tienen que brindar mayor apoyo a sus hijos para superar esta dificultad.").LineHeight(2f);

                    if (sesion.Agenda != null)
                    {
                        col1.Item().Text(sesion.Agenda).LineHeight(2f);
                    }

                    col1.Item().Text("Seguidamente el docente hace entrega de las boletas a cada uno de los padres, seguidamente se expresa a los padres su preocupación por los estudiantes que están en Inicio y Proceso los cuales son "+ sesion.CantidadBajoNivel+" estudiantes").LineHeight(2f);                    
                    col1.Item().Text("El docente agradece a los padres de familia por su apoyo.").LineHeight(2f);

                  
                    col1.Item().Text("Asímismo, el docente se compromete a brindar refuerzo escolar fuera del horario que los padres lo soliciten.").LineHeight(2f);
                    col1.Item().Text("Los padres de familia expresan su aceptación para mejorar el aprendizaje de sus hijos, comprometiéndose a asumir el costo económico y enviar puntualmente a los niños.").LineHeight(2f);


                    col1.Item().Text("Sin otro particular me suscribo a usted.").LineHeight(2f);

                    col1.Item().AlignCenter().PaddingTop(40).Text("Atentamente,").Bold();
                    col1.Item().AlignCenter().PaddingTop(10).Text("Docente " + sesion.DocenteNombre).SemiBold(); /******Nombre docente*****/

                    col1.Item().PaddingTop(15).LineHorizontal(0.5f);

                    col1.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                    {
                        col.Item().Text(sesion.Comentarios).FontSize(12).Bold();
                        col.Item().Text(Placeholders.LoremIpsum());
                        col.Spacing(4);
                    });
                });
        }
    }
}

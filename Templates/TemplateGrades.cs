namespace Informes
{
    using Models;
    using Models.Reports;
    using Models.Views;
    using QuestPDF.Drawing;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class TemplateGrades : IDocument
    {
        public LNInformeNotas notas { get; set; }
        public TemplateGrades(LNInformeNotas notas)
        {
            this.notas = notas;
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
                row.ConstantItem(60).Padding(0).Image(imageData);

                //Segundo Cuadro
                row.RelativeItem().Column(col =>
                {
                    col.Item().PaddingBottom(5).Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(13).Bold());
                        text.AlignCenter();

                        text.Span("INFORME DE RENDIMIENTO ACADÉMICO N° "+notas.IdInforme);
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

                row.ConstantItem(60).Padding(0);
            });
        }

        //Contenido
        void ComposeContent(IContainer container)
        {           
            container.PaddingRight(35).PaddingLeft(35).PaddingTop(20)
                .Column(col1 =>
                {

                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.AlignRight();
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("Prof: " + notas.DocenteNombre + "/" + "Grado:" + notas.Seccion).Bold(); /****************/
                    });

                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("INFORME N°" + notas.IdInforme + " - " + DateTime.Now.ToString("yyyy") + "").Underline().Bold(); /*********************/
                    });

                    col1.Item().Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("A:       ").SemiBold();
                        txt.Span(" Fernando Vásquez Diaz");
                    });

                    col1.Item().PaddingBottom(10).PaddingLeft(20).Text("     Director I.E N° 1258 'SLT' ").Italic();

                    col1.Item().Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("De:     ").SemiBold();
                        txt.Span(notas.DocenteNombre); /********NOMBRE DOCENTE*************/
                    });
                    col1.Item().PaddingBottom(10).PaddingLeft(20).Text("    Profesor del grado " + notas.Seccion).Italic(); /****GRADO SECCION******/


                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("Asunto: ").SemiBold();
                        txt.Span("Informe sobre rendimiento académico"); /********FECHA *************/
                    });

                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("Fecha:  ").SemiBold();
                        txt.Span("" + DateTime.Now.ToString("dd/MM/yyyy")); /********FECHA *************/
                    });


                    col1.Item().PaddingTop(10).LineHorizontal(0.5f);

                    //Parrafo
                    col1.Item().PaddingTop(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12).LineHeight(2f));
                        txt.Span("Por medio del presente me dirijo a ud. para saludarle y al mismo tiempo hacer de su conocimiento sobre los resultados de las evaluaciones correspondientes al " + notas.Trimestre + " del presente año, como se detalla a continuación:");
                    });


                    //DATOS
                    col1.Item().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.ExtendLastCellsToTableBottom();

                        //Primera fila
                        table.Cell().Background(Colors.Grey.Lighten3).AlignMiddle().AlignCenter().Text("Escala").FontSize(10).LineHeight(2f);
                        table.Cell().Background(Colors.Grey.Lighten3).AlignMiddle().AlignCenter().Text("Cantidad").FontSize(10).LineHeight(2f);
                        table.Cell().Background(Colors.Grey.Lighten3).AlignMiddle().AlignCenter().Text("Nivel de Logro").FontSize(10).LineHeight(2f);

                        //Segunda fila
                        table.Cell().AlignMiddle().AlignCenter().Text("AD").FontSize(10).LineHeight(2f);
                        table.Cell().AlignMiddle().AlignCenter().Text("" + notas.CantidadAD).FontSize(10).LineHeight(2f); /*******Cantidad inicial*****/
                        table.Cell().AlignMiddle().AlignCenter().Text("Destacado").FontSize(10).LineHeight(2f);

                        table.Cell().AlignMiddle().AlignCenter().Text("A").FontSize(10).LineHeight(2f);
                        table.Cell().AlignMiddle().AlignCenter().Text("" + notas.CantidadA).FontSize(10).LineHeight(2f); /*******Cantidad inicial*****/
                        table.Cell().AlignMiddle().AlignCenter().Text("Aprobado").FontSize(10).LineHeight(2f);

                        table.Cell().AlignMiddle().AlignCenter().Text("B").FontSize(10).LineHeight(2f);
                        table.Cell().AlignMiddle().AlignCenter().Text("" + notas.CantidadB).FontSize(10).LineHeight(2f); /*******Cantidad inicial*****/
                        table.Cell().AlignMiddle().AlignCenter().Text("En proceso").FontSize(10).LineHeight(2f);

                        table.Cell().AlignMiddle().AlignCenter().Text("C").FontSize(10).LineHeight(2f);
                        table.Cell().AlignMiddle().AlignCenter().Text("" + notas.CantidadC).FontSize(10).LineHeight(2f); /*******Cantidad inicial*****/
                        table.Cell().AlignMiddle().AlignCenter().Text("En Inicio").FontSize(10).LineHeight(2f);

                    });


                    //Lista
                    col1.Item().Text("Así mismo, se brindará refuerzo a los estudiantes que están en incio y proceso").LineHeight(2f);
                    col1.Item().Text("Sin otro particular me suscribo a usted.").LineHeight(2f);

                    col1.Item().AlignCenter().PaddingTop(40).Text("Atentamente,").Bold();
                    col1.Item().AlignCenter().PaddingTop(10).Text("Docente " + notas.DocenteNombre).SemiBold(); /******Nombre docente*****/
                });
        }
    }
}

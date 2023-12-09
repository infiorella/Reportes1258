namespace Informes
{
    using Models;
    using Models.Reports;
    using QuestPDF.Drawing;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class TemplateAttendance : IDocument
    {
        public LNInformeAsistencia asistencia { get; set; }
        public TemplateAttendance(LNInformeAsistencia informe)
        {
            this.asistencia = informe;
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
                        x.CurrentPageNumber().FontSize(9);
                        x.Span(" / ").FontSize(9);
                        x.TotalPages().FontSize(9);
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
                //C: \Users\USER\Documents\Bitbucket\wwwroot\img\logo.jpg
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

                        text.Span("Informe de asistencia del 1 'A ' para el mes de 3");
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
                        txt.Span("Prof: " + asistencia.DocenteNombre + "/" + "Grado:" + asistencia.Seccion).Bold(); /****************/
                    });

                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("INFORME N°" + asistencia.IdReporte + " - " + DateTime.Now.ToString("yyyy") + "").Underline().Bold(); /*********************/
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
                        txt.Span(asistencia.DocenteNombre); /********NOMBRE DOCENTE*************/
                    });
                    col1.Item().PaddingBottom(10).PaddingLeft(20).Text("    Profesor del grado " + asistencia.Grado + " " + asistencia.Seccion).Italic(); /****GRADO SECCION******/


                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("Asunto: ").SemiBold();
                        txt.Span("Informe de Inasistencia de estudiantes correspondientes al mes de " + asistencia.Mes + " del " + DateTime.Now.ToString("yyyy")); /********FECHA *************/
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
                        txt.Span("Por medio del presente me dirijo a ud. a fin de saludarlo y al mismo tiempo " +
                            "informarle respecto a la asistencia de los estudiantes a mi cargo durante el mes" + asistencia.Mes +/***********/
                            " del presente año, como se detalla a continuación: ");
                    });


                    //DATOS
                    col1.Item().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.ExtendLastCellsToTableBottom();

                        //Primera fila
                        table.Cell().ColumnSpan(2).Background(Colors.Grey.Lighten3).AlignMiddle().Padding(8).Text("Intersección").FontSize(10);
                        table.Cell().Background(Colors.Grey.Lighten3).AlignMiddle().Text("Justificada").FontSize(10);
                        table.Cell().Background(Colors.Grey.Lighten3).AlignMiddle().Text("No Justificada").FontSize(10);

                        //Segunda fila
                        table.Cell().ColumnSpan(2).AlignMiddle().Padding(8).Text("Tardanza").FontSize(10);
                        table.Cell().AlignMiddle().AlignCenter().Text("" + asistencia.tardanzasJustificadas).FontSize(10); /*******Cantidad inicial*****/
                        table.Cell().AlignMiddle().AlignCenter().Text("" + asistencia.tardanzasNoJustificadas).FontSize(10);

                        //Segunda fila
                        table.Cell().ColumnSpan(2).AlignMiddle().Padding(8).Text("Falta").FontSize(10);
                        table.Cell().AlignMiddle().AlignCenter().Text("" + asistencia.faltasJustificadas).FontSize(10); /*******Cantidad inicial*****/
                        table.Cell().AlignMiddle().AlignCenter().Text("" + asistencia.faltasNoJustificadas).FontSize(10);
                    });


                    //Lista
                    col1.Item().Text("Es cuenta informe a usted para su conocimiento y fines pertinentes").LineHeight(2f);
                    col1.Item().Text("Sin otro particular me suscribo a usted.").LineHeight(2f);

                    col1.Item().AlignCenter().PaddingTop(40).Text("Atentamente,").Bold();
                    col1.Item().AlignCenter().PaddingTop(10).Text("Docente " + asistencia.DocenteNombre).SemiBold(); /******Nombre docente*****/
                });
        }
    }
}

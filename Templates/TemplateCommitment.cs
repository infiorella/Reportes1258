namespace Informes
{
    using Models.Reports;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class TemplateCommitment : IDocument
    {
        public LNActaCompromiso report { get; set; }
        public TemplateCommitment(LNActaCompromiso report)
        {
            this.report = report;
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

                        text.Span("INFORME DE COMPROMISO N° "+report.IdInforme);
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
                        txt.Span("Prof: " +  report.DocenteNombre + "/" + "Grado:" + report.Seccion).Bold(); /****************/
                    });

                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("INFORME N°" + report.IdInforme + " - " + report.FechaCompromiso.ToString("yyyy") + "").Underline().Bold(); /*********************/
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
                        txt.Span(report.DocenteNombre); /********NOMBRE DOCENTE*************/
                    });
                    col1.Item().PaddingBottom(10).PaddingLeft(20).Text("    Profesor del grado " + report.Seccion).Italic(); /****GRADO SECCION******/


                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("Asunto: ").SemiBold();
                        txt.Span("Informe sobre conductas inadecuadas de los estudiantes");
                    });

                    col1.Item().PaddingBottom(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12));
                        txt.Span("Fecha:  ").SemiBold();
                        txt.Span("" + DateTime.Now.ToString("dd de MMMM del yyyy")); /********FECHA *************/
                    });


                    col1.Item().PaddingTop(10).LineHorizontal(0.5f);

                    //Parrafo
                    col1.Item().PaddingTop(10).Text(txt =>
                    {
                        txt.DefaultTextStyle(x => x.FontSize(12).LineHeight(1.5f));
                        txt.Span("Por medio del presente me dirijo a usted para informarle sobre los compromisos adquiridos por los padres de fmilia para el presente año escolar:  ");
                    });

                    col1.Item().PaddingLeft(15).Column(column =>
                    {
                        foreach (var i in report.Compromisos/*LNInformeComportamiento.Alumnos()*/)
                        {
                            column.Item().Row(row =>
                            {
                                row.Spacing(5);
                                row.AutoItem().Text($"{i}."); // text or image
                                row.RelativeItem().Text(Placeholders.Sentence());
                            });
                        }
                    });


                    col1.Item().PaddingTop(10).Text("Sin otro particular, me suscribo a usted").LineHeight(1.5f);

                    col1.Item().AlignCenter().PaddingTop(40).Text("Atentamente,").Bold();
                    col1.Item().AlignCenter().PaddingTop(10).Text("Docente " + report.DocenteNombre).SemiBold(); /******Nombre docente*****/


                    col1.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                    {
                        col.Item().Text(report.Comentarios).FontSize(12).Bold();
                        col.Item().Text(Placeholders.LoremIpsum());
                        col.Spacing(4);
                    });
                });
        }
    }
}

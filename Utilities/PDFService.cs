using InventoryIT.Data;
using InventoryIT.Model;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace InventoryIT.Utilities
{
    public class PDFService
    {
        private readonly InventoryDbContext inventoryDbContext;

        public PDFService(InventoryDbContext inventoryDbContext)
        {
            this.inventoryDbContext = inventoryDbContext;
        }

        public async Task<byte[]> GenerateListExtensions(List<PhoneExtension> extensions)
        {
            // Agrupar las extensiones por PBX
            var groupedExtensions = extensions.GroupBy(e => e.PhoneNumberPBX);

            using (var memoryStream = new MemoryStream())
            {
                // Crear un nuevo documento PDF
                var document = new PdfDocument();

                // Mostrar una sola página
                document.PageLayout = PdfPageLayout.SinglePage;

                // Permitir que la aplicación del visor ajuste la página en la ventana
                document.ViewerPreferences.FitWindow = true;

                // Establecer el título del documento
                document.Info.Title = "Lista de Extensiones";

                // Definir las fuentes a usar
                var titleFont = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
                var paragraphFont = new XFont("Times New Roman", 12, XFontStyleEx.Italic);
                var font = new XFont("Times New Roman", 12, XFontStyleEx.Regular);

                // Crear una página para cada grupo de PBX
                foreach (var group in groupedExtensions)
                {
                    // Crear la primera página para el grupo actual
                    var page = document.AddPage();
                    var gfx = XGraphics.FromPdfPage(page);
                    var tf = new XTextFormatter(gfx);

                    // Dibujar el título del documento en la página actual
                    gfx.DrawString($"Lista de Extensiones - PBX {group.Key}", titleFont, XBrushes.Black,
                        new XRect(0, 40, page.Width.Point, 40), XStringFormats.Center);

                    // Dibujar un párrafo antes de la tabla
                    var beforeTableParagraph = $"Lista de extensiones para PBX {group.Key}";
                    tf.DrawString(beforeTableParagraph, paragraphFont, XBrushes.Black,
                        new XRect(50, 80, page.Width.Point - 100, 40));

                    // Definir el tamaño y posición de las celdas de la tabla
                    double cellWidth = 100;
                    double cellHeight = 15;
                    double x = 30; // Coordenada X inicial para la tabla
                    double y = 120; // Coordenada Y inicial para la tabla

                    // Calcular el número máximo de filas por página
                    int maxRowsPerPage = (int)((page.Height.Point - y - 40) / cellHeight);

                    // Encabezados de la tabla
                    var headers = new string[] { "Extension", "Central", "Tipo", "Empleado", "Apellido" };
                    int currentRow = 0;

                    // Función para dibujar el encabezado de la tabla
                    void DrawTableHeader(XGraphics g, double startX, double startY)
                    {
                        for (int col = 0; col < headers.Length; col++)
                        {
                            double cellX = startX + col * cellWidth;
                            g.DrawRectangle(XPens.Black, cellX, startY, cellWidth, cellHeight);
                            g.DrawString(headers[col], font, XBrushes.Black,
                                new XRect(cellX, startY, cellWidth, cellHeight),
                                XStringFormats.Center);
                        }
                    }

                    // Función para dibujar una fila de datos en la tabla
                    void DrawTableRow(XGraphics g, double startX, double startY, PhoneExtension ext)
                    {
                        var data = new string[]
                        {
                    ext.Extension.ToString(),
                    ext.PhoneNumberPBX.ToString(),
                    ext.Employee ==null? "": ext.Type,
                    ext.Employee?.Name ?? "",
                    ext.Employee?.LastName ?? ""
                        };

                        for (int col = 0; col < data.Length; col++)
                        {
                            double cellX = startX + col * cellWidth;
                            g.DrawRectangle(XPens.Black, cellX, startY, cellWidth, cellHeight);
                            g.DrawString(data[col], font, XBrushes.Black,
                                new XRect(cellX, startY, cellWidth, cellHeight),
                                XStringFormats.Center);
                        }
                    }

                    // Dibujar el encabezado de la tabla en la página actual
                    DrawTableHeader(gfx, x, y);
                    y += cellHeight;
                    currentRow++;

                    // Dibujar cada extensión en la tabla
                    foreach (var extension in group)
                    {
                        // Si se alcanza el límite de filas por página, agregar una nueva página
                        if (currentRow >= maxRowsPerPage)
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            y = 40; // Restablecer la coordenada Y para la nueva página
                            DrawTableHeader(gfx, x, y); // Dibujar el encabezado en la nueva página
                            y += cellHeight;
                            currentRow = 1; // Restablecer el contador de filas para la nueva página
                        }

                        // Dibujar la fila actual en la tabla
                        DrawTableRow(gfx, x, y, extension);
                        y += cellHeight;
                        currentRow++;

                    }
                    // Agregar pie de página
                    var footerText = $"Documento generado el {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Reporte Generado por el Sistema InventoryIT";
                    gfx.DrawString(footerText, font, XBrushes.Black,
                        new XRect(50, page.Height.Point - 50, page.Width.Point - 100, 20),
                        XStringFormats.Center);

                }



                // Guardar el documento en el memoryStream en lugar de en un archivo
                document.Save(memoryStream, false);

                // Retornar los bytes del PDF generado
                return memoryStream.ToArray();
            }
        }



        public async Task<byte[]> GeneratePDFPeripheral(PeripheralModel i)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Crear un nuevo documento PDF.
                var document = new PdfDocument();

                // Mostrar una sola página.
                document.PageLayout = PdfPageLayout.SinglePage;

                // Permitir que la aplicación del visor ajuste la página en la ventana.
                document.ViewerPreferences.FitWindow = true;

                // Establecer el título del documento.
                document.Info.Title = "Reporte de Entrega de Activos";

                // Crear una página vacía.
                var page = document.AddPage();

                // Obtener un objeto XGraphics para dibujar.
                var gfx = XGraphics.FromPdfPage(page);

                // Crear una fuente.
                var font = new XFont("Times New Roman", 12, XFontStyleEx.Regular);
                var titleFont = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
                var paragraphFont = new XFont("Times New Roman", 12, XFontStyleEx.Italic);

                // Crear un objeto XTextFormatter
                var tf = new XTextFormatter(gfx);

                // Título del documento
                gfx.DrawString("Reporte de Entrega de Activos", titleFont, XBrushes.Black,
                    new XRect(0, 40, page.Width.Point, 40), XStringFormats.Center);

                // Párrafo antes de la tabla
                var beforeTableParagraph = "A continuación se presenta la información correspondiente al activo entregado";
                tf.DrawString(beforeTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, 80, page.Width.Point - 100, 40));

                // Definir el tamaño y posición de la tabla.
                double cellHeight = 15;
                double y = 150; // Ajustar la posición Y para que haya espacio para el párrafo

                // Datos de ejemplo para la tabla.
                string[,] data = new string[,]
                {
            { "Información", "Datos" },
            { "Marca", i.Brand.Name},
            { "Número de Serie", i.SerialNumber.ToString() },
            { "Modelo", i.Model },
            { "Tipo", i.Type },
            { "Adquisición", i.AdquisitionDate.ToShortDateString() },
            { "Costo", "$ " + i.Cost.ToString() },
            { "Cédula", i.Employee.DNI.ToString()},
            { "Nombre", i.Employee.Name },
            { "Apellido", i.Employee.LastName.ToString() },
            { "Teléfono", i.Employee.PhoneNumber.ToString() },
                };

                // Calcular el ancho de cada columna basándose en el tamaño del texto.
                double[] columnWidths = new double[data.GetLength(1)];
                for (int col = 0; col < data.GetLength(1); col++)
                {
                    double maxWidth = 0;
                    for (int row = 0; row < data.GetLength(0); row++)
                    {
                        var size = gfx.MeasureString(data[row, col], font);
                        if (size.Width > maxWidth)
                        {
                            maxWidth = size.Width;
                        }
                    }
                    columnWidths[col] = maxWidth + 10; // Agregar un pequeño margen
                }

                // Calcular el ancho total de la tabla.
                double tableWidth = 0;
                foreach (double width in columnWidths)
                {
                    tableWidth += width;
                }

                // Calcular la posición X inicial para centrar la tabla.
                double x = (page.Width.Point - tableWidth) / 2;

                // Dibujar la tabla.
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    double cellX = x;
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        double cellWidth = columnWidths[col];

                        // Dibujar el borde de la celda.
                        gfx.DrawRectangle(XPens.Black, cellX, y, cellWidth, cellHeight);

                        // Dibujar el texto dentro de la celda.
                        gfx.DrawString(data[row, col], font, XBrushes.Black,
                            new XRect(cellX, y, cellWidth, cellHeight),
                            XStringFormats.Center);

                        cellX += cellWidth;
                    }
                    y += cellHeight;
                }

                // Párrafo después de la tabla
                var afterTableParagraph = $"Se informa a {i.Employee.Name} {i.Employee.LastName}, cédula {i.Employee.DNI}, que se llevará a cabo la entrega de un activo asignada para el uso corporativo, con la serie {i.SerialNumber}. El empleado que reciba este activo será responsable de su correcto uso y mantenimiento. Es imperativo utilizar el dispositivo exclusivamente para actividades relacionadas con el trabajo, asegurando que se cumplan las políticas de seguridad y confidencialidad de la empresa. Cualquier daño o pérdida debe ser reportado de inmediato al departamento de TI. Asimismo, se recuerda que es responsabilidad del empleado devolver el equipo en buen estado al finalizar su relación laboral con la empresa. Su cooperación y cumplimiento con estas directrices son esenciales para garantizar una comunicación eficiente y segura dentro de nuestra organización";
                tf.DrawString(afterTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, y + 20, page.Width.Point - 100, 200));

                // Espacio para las firmas
                short _y = 200;
                gfx.DrawLine(XPens.Black, 50, y + _y, page.Width.Point - 80, y + _y);
                gfx.DrawString("Firma de quien entrega", font, XBrushes.Black,
                    new XRect(50, y + _y, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Incrementar la coordenada y para crear separación
                gfx.DrawLine(XPens.Black, 50, y + 270, page.Width.Point - 80, y + 270);
                gfx.DrawString($"Firma de quien recibe: {i.Employee.Name} {i.Employee.LastName} {i.Employee.SecondLastName}", font, XBrushes.Black,
                    new XRect(50, y + 280, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Agregar pie de página
                var footerText = $"Documento generado el {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Reporte Generado por el Sistema InventoryIT";
                gfx.DrawString(footerText, font, XBrushes.Black,
                    new XRect(50, page.Height.Point - 50, page.Width.Point - 100, 20),
                    XStringFormats.Center);

                // Guardar el documento en el memoryStream en lugar de en un archivo
                document.Save(memoryStream, false);

                // Retornar los bytes del PDF generado
                return memoryStream.ToArray();
            }
        }

        public async Task<byte[]> GeneratePDFComputer(ComputerModel i)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Crear un nuevo documento PDF.
                var document = new PdfDocument();

                // Mostrar una sola página.
                document.PageLayout = PdfPageLayout.SinglePage;

                // Permitir que la aplicación del visor ajuste la página en la ventana.
                document.ViewerPreferences.FitWindow = true;

                // Establecer el título del documento.
                document.Info.Title = "Reporte de Entrega de Activos";

                // Crear una página vacía.
                var page = document.AddPage();

                // Obtener un objeto XGraphics para dibujar.
                var gfx = XGraphics.FromPdfPage(page);

                // Crear una fuente.
                var font = new XFont("Times New Roman", 12, XFontStyleEx.Regular);
                var titleFont = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
                var paragraphFont = new XFont("Times New Roman", 12, XFontStyleEx.Italic);

                // Crear un objeto XTextFormatter
                var tf = new XTextFormatter(gfx);

                // Título del documento
                gfx.DrawString("Reporte de Entrega de Activos", titleFont, XBrushes.Black,
                    new XRect(0, 40, page.Width.Point, 40), XStringFormats.Center);

                // Párrafo antes de la tabla
                var beforeTableParagraph = "A continuación se presenta la información correspondiente al activo entregado";
                tf.DrawString(beforeTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, 80, page.Width.Point - 100, 40));

                // Definir el tamaño y posición de la tabla.
                double cellHeight = 15;
                double y = 150; // Ajustar la posición Y para que haya espacio para el párrafo

                // Datos de ejemplo para la tabla.
                string[,] data = new string[,]
                {
            { "Información", "Datos" },
            { "Número de serie", i.SerialNumber.ToString() },
            { "Modelo", i.ModelName.ToString() },
            { "Adquisición", i.AdquisitionDate.ToShortDateString() },
            { "Costo", i.Cost.ToString() },
            { "Tipo Disco", (i.HaveSSD)? "Tiene SSD":"Tiene HHD" },
            { "Tamaño Disco", i.SizeDisk.ToString() + " GB"},
            { "Tipo RAM", i.RAMType.ToString() },
            { "Tamaño RAM", i.SizeRAM.ToString() + " GB" },
            { "Procesador", i.Processor.ToString() },
            { "Distribución", i.KeyboardLayout },
            { "Marca", i.Brand.Name.ToString() },
            { "Cédula", i.Employee.DNI.ToString() },
            { "Nombre", i.Employee.Name.ToString() },
            { "Apellido", i.Employee.LastName.ToString() },
            { "Telefono", i.Employee.PhoneNumber.ToString() },
                    // { nameof(i.Employee.Departament.Name), i.Employee.Departament.Name }
                };

                // Calcular el ancho de cada columna basándose en el tamaño del texto.
                double[] columnWidths = new double[data.GetLength(1)];
                for (int col = 0; col < data.GetLength(1); col++)
                {
                    double maxWidth = 0;
                    for (int row = 0; row < data.GetLength(0); row++)
                    {
                        var size = gfx.MeasureString(data[row, col], font);
                        if (size.Width > maxWidth)
                        {
                            maxWidth = size.Width;
                        }
                    }
                    columnWidths[col] = maxWidth + 10; // Agregar un pequeño margen
                }

                // Calcular el ancho total de la tabla.
                double tableWidth = 0;
                foreach (double width in columnWidths)
                {
                    tableWidth += width;
                }

                // Calcular la posición X inicial para centrar la tabla.
                double x = (page.Width.Point - tableWidth) / 2;

                // Dibujar la tabla.
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    double cellX = x;
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        double cellWidth = columnWidths[col];

                        // Dibujar el borde de la celda.
                        gfx.DrawRectangle(XPens.Black, cellX, y, cellWidth, cellHeight);

                        // Dibujar el texto dentro de la celda.
                        gfx.DrawString(data[row, col], font, XBrushes.Black,
                            new XRect(cellX, y, cellWidth, cellHeight),
                            XStringFormats.Center);

                        cellX += cellWidth;
                    }
                    y += cellHeight;
                }

                // Párrafo después de la tabla
                var afterTableParagraph = $"Se informa a {i.Employee.Name} {i.Employee.LastName}, cédula {i.Employee.DNI} que se llevará a cabo la entrega de una computadora, con la serie {i.SerialNumber} asignada para el uso corporativo. El empleado que reciba este activo será responsable de su correcto uso y mantenimiento. Es imperativo utilizar el dispositivo exclusivamente para actividades relacionadas con el trabajo, asegurando que se cumplan las políticas de seguridad y confidencialidad de la empresa. Cualquier daño o pérdida debe ser reportado de inmediato al departamento de TI. Asimismo, se recuerda que es responsabilidad del empleado devolver el equipo en buen estado al finalizar su relación laboral con la empresa. Su cooperación y cumplimiento con estas directrices son esenciales para garantizar una comunicación eficiente y segura dentro de nuestra organización";
                tf.DrawString(afterTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, y + 20, page.Width.Point - 100, 200));

                // Espacio para las firmas
                short _y = 200;
                gfx.DrawLine(XPens.Black, 50, y + _y, page.Width.Point - 80, y + _y);
                gfx.DrawString("Firma de quien entrega", font, XBrushes.Black,
                    new XRect(50, y + _y, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Incrementar la coordenada y para crear separación
                gfx.DrawLine(XPens.Black, 50, y + 270, page.Width.Point - 80, y + 270);
                gfx.DrawString($"Firma de quien recibe: {i.Employee.Name} {i.Employee.LastName} {i.Employee.SecondLastName}", font, XBrushes.Black,
                    new XRect(50, y + 280, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Agregar pie de página
                var footerText = $"Documento generado el {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Reporte Generado por el Sistema InventoryIT";
                gfx.DrawString(footerText, font, XBrushes.Black,
                    new XRect(50, page.Height.Point - 50, page.Width.Point - 100, 20),
                    XStringFormats.Center);

                // Guardar el documento en el memoryStream en lugar de en un archivo
                document.Save(memoryStream, false);

                // Retornar los bytes del PDF generado
                return memoryStream.ToArray();
            }
        }


        public async Task<byte[]> GeneratePDFPhone(Phone_Number_User_Model i)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Crear un nuevo documento PDF.
                var document = new PdfDocument();

                // Mostrar una sola página.
                document.PageLayout = PdfPageLayout.SinglePage;

                // Permitir que la aplicación del visor ajuste la página en la ventana.
                document.ViewerPreferences.FitWindow = true;

                // Establecer el título del documento.
                document.Info.Title = "Reporte de Entrega de Activos";

                // Crear una página vacía.
                var page = document.AddPage();

                // Obtener un objeto XGraphics para dibujar.
                var gfx = XGraphics.FromPdfPage(page);

                // Crear una fuente.
                var font = new XFont("Times New Roman", 12, XFontStyleEx.Regular);
                var titleFont = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
                var paragraphFont = new XFont("Times New Roman", 12, XFontStyleEx.Italic);

                // Crear un objeto XTextFormatter
                var tf = new XTextFormatter(gfx);

                // Título del documento
                gfx.DrawString("Reporte de Entrega de Activos", titleFont, XBrushes.Black,
                    new XRect(0, 40, page.Width.Point, 40), XStringFormats.Center);

                // Párrafo antes de la tabla
                var beforeTableParagraph = "A continuación se presenta la información correspondiente al activo entregado";
                tf.DrawString(beforeTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, 80, page.Width.Point - 100, 40));

                // Datos de ejemplo para la tabla.
                string[,] data = new string[,]
                {
            { "Información", "Datos" },
            { "Número Teléfono", i.PhoneNumber.Number.ToString() },
            { "Operador", i.PhoneNumber.Operator.ToString() },
            { "Tipo", i.PhoneNumber.Type.ToString() },
            { "Serie", i.PhoneNumberModel.PhoneSerial },
            { "Modelo", i.PhoneNumberModel.PhoneModel },
            { "Fecha Asignación", i.CreationDate.ToShortDateString() },
            { "Cédula", i.Employee.DNI.ToString() },
            { "Nombre", i.Employee.Name.ToString() },
            { "Apellido", i.Employee.LastName.ToString() },
            { "Teléfono", i.Employee.PhoneNumber.ToString() },
            { "Departamento", i.Employee.Departament.Name }
                };

                // Calcular el ancho de cada columna basándose en el tamaño del texto.
                double[] columnWidths = new double[data.GetLength(1)];
                for (int col = 0; col < data.GetLength(1); col++)
                {
                    double maxWidth = 0;
                    for (int row = 0; row < data.GetLength(0); row++)
                    {
                        var size = gfx.MeasureString(data[row, col], font);
                        if (size.Width > maxWidth)
                        {
                            maxWidth = size.Width;
                        }
                    }
                    columnWidths[col] = maxWidth + 10; // Agregar un pequeño margen
                }

                // Calcular el ancho total de la tabla.
                double tableWidth = 0;
                foreach (double width in columnWidths)
                {
                    tableWidth += width;
                }

                // Calcular la posición X inicial para centrar la tabla.
                double x = (page.Width.Point - tableWidth) / 2;
                double y = 150; // Ajustar la posición Y para que haya espacio para el párrafo
                double cellHeight = 20;

                // Dibujar la tabla.
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    double cellX = x;
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        double cellWidth = columnWidths[col];

                        // Dibujar el borde de la celda.
                        gfx.DrawRectangle(XPens.Black, cellX, y, cellWidth, cellHeight);

                        // Dibujar el texto dentro de la celda.
                        gfx.DrawString(data[row, col], font, XBrushes.Black,
                            new XRect(cellX, y, cellWidth, cellHeight),
                            XStringFormats.Center);

                        cellX += cellWidth;
                    }
                    y += cellHeight;
                }

                // Párrafo después de la tabla
                var afterTableParagraph = $"Se informa a {i.Employee.Name} {i.Employee.LastName}, cédula {i.Employee.DNI}, que se llevará a cabo la entrega de un teléfono y una línea telefónica asignados para el uso corporativo. El empleado que reciba estos activos será responsable de su correcto uso y mantenimiento. Es imperativo utilizar los dispositivos exclusivamente para actividades relacionadas con el trabajo, asegurando que se cumplan las políticas de seguridad y confidencialidad de la empresa. Cualquier daño o pérdida debe ser reportado de inmediato al departamento de TI. Asimismo, se recuerda que es responsabilidad del empleado devolver el equipo en buen estado al finalizar su relación laboral con la empresa. Su cooperación y cumplimiento con estas directrices son esenciales para garantizar una comunicación eficiente y segura dentro de nuestra organización.";
                tf.DrawString(afterTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, y + 20, page.Width.Point - 100, 200));

                // Posicionar imeitext
                var imeitext = $"El IMEI del teléfono corresponde a: {i.PhoneNumberModel.IMEIs}, con la serie {i.PhoneNumberModel.PhoneSerial}.";
                tf.DrawString(imeitext, paragraphFont, XBrushes.Red, new XRect(50, y + 180, page.Width.Point - 100, 40));

                // Espacio para las firmas
                short _y = 280;
                gfx.DrawLine(XPens.Black, 50, y + _y, page.Width.Point - 80, y + _y);
                gfx.DrawString("Firma de quien entrega", font, XBrushes.Black,
                    new XRect(50, y + _y, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Incrementar la coordenada y para crear separación
                gfx.DrawLine(XPens.Black, 50, y + 350, page.Width.Point - 80, y + 350);
                gfx.DrawString($"Firma y nombre: {i.Employee.Name} {i.Employee.LastName} {i.Employee.SecondLastName} - {i.Employee.DNI}", font, XBrushes.Black,
                    new XRect(50, y + 360, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Agregar pie de página
                var footerText = $"Documento generado el {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Reporte Generado por el Sistema InventoryIT";
                gfx.DrawString(footerText, font, XBrushes.Black,
                    new XRect(50, page.Height.Point - 50, page.Width.Point - 100, 20),
                    XStringFormats.Center);

                // Guardar el documento en el memoryStream en lugar de en un archivo
                document.Save(memoryStream, false);

                // Retornar los bytes del PDF generado
                return memoryStream.ToArray();
            }
        }

    }
}

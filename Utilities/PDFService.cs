using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Drawing.Printing;

namespace InventoryIT.Utilities
{
    public class PDFService
    {

        #region Properties
        private readonly InventoryDbContext inventoryDbContext;

        // Crear fuentes
        XFont fuenteTitulo = new XFont("Verdana", 12);
        XFont fuentePequena = new XFont("Verdana", 8);
        XFont fuenteBold = new XFont("Verdana", 10);
        XFont fuenteGeneral = new XFont("Verdana", 10);
        // Definir el margen de 1 cm (en puntos)
        double margen = 28.35; // 1 cm en puntos
        int yoffetHeader = 0;
        double margenInferior = 0; // 0.8 cm en puntos
        double margenSuperior = 0; // 1 cm en puntos (margen superior)

        int totalPages = 4;
       

        #endregion

        public PDFService(InventoryDbContext inventoryDbContext)
        {
            this.inventoryDbContext = inventoryDbContext;
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
                tf.DrawString(imeitext, paragraphFont, XBrushes.Black, new XRect(50, y + 180, page.Width.Point - 100, 40));

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

                // Establecer el título del documento.
                document.Info.Title = "Reporte de Entrega de Activos";

                // Diccionario para almacenar las páginas y sus gráficos.
                var pages = new Dictionary<string, (PdfPage Page, XGraphics Gfx)>();


                #region Portada

                // Crear la primera página para la portada.
                var portada = CrearNuevaPagina(pages, "Portada", document, true);
                GenerarPortada(portada.Gfx, portada.Page);

                #endregion
                #region Pagina 2

                // Crear la segunda página.
                CrearNuevaPagina(pages, "Page2", document, true);

                string parrafo = "El colaborador que reciba este activo será responsable de su correcto uso y mantenimiento. " +
                                 "Es imperativo utilizar el dispositivo exclusivamente para actividades relacionadas con el trabajo, " +
                                 "asegurando que se cumplan las políticas de seguridad y confidencialidad de la empresa. " +
                                 "Cualquier daño o pérdida debe ser reportado de inmediato al departamento de TI. " +
                                 "Asimismo, se recuerda que es responsabilidad del colaborador devolver el equipo en buen estado " +
                                 "al finalizar su relación laboral con la empresa. Su cooperación y cumplimiento con estas directrices " +
                                 "son esenciales para garantizar una comunicación eficiente y segura dentro de nuestra organización.";

                AddParrafo(pages["Page2"].Gfx, parrafo, fuenteGeneral, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page2"].Gfx, "Por lo tanto, la presente acta, hace constar la entrega del siguiente equipo y accesorios:", fuenteGeneral, ref yoffetHeader); // Ajusta la fuente y el offset según necesites

                // Definir altura de celda
                int cellHeight = 20;

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


                // Llamar al método para agregar la tabla
                AgregarTabla(pages["Page2"].Gfx, data, fuenteGeneral, ref yoffetHeader, cellHeight);

                #endregion

                #region page 3
                CrearNuevaPagina(pages, "Page3", document, true);

                string parrafo2 = "El Colaborador manifiesta que el equipo que aquí se entrega es y será de la empresa en todo momento, y procederá en caso de";

                AddParrafo(pages["Page3"].Gfx, parrafo2, fuenteGeneral, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page3"].Gfx, "Pérdida del Equipo:", fuenteTitulo, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page3"].Gfx, "Él colaborador deberá notificar de forma inmediata a su jefe, coordinador, para que se tomen las medidas de seguridad necesarias para el respectivo control administrativo.", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page3"].Gfx, "Robo de Equipo:", fuenteTitulo, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page3"].Gfx, "Él colaborador deberá acudir de manera inmediata al ente Público para su denuncia y notificar (presentar recibo de la denuncia) de forma inmediata a su jefe, coordinador o supervisor, para que se tomen las medidas de seguridad necesaria. A su vez el supervisor) deberá de notificar vía correo a Área de Tecnología y Recursos Humanos, para el respectivo control administrativo.", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page3"].Gfx, "En caso de robo o extravió de equipos y accesorios usados:  ", fuenteTitulo, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page3"].Gfx, "Se calculará el costo según la condición del equipo o depreciación desde el momento de la entrega (detalle escrito en el formato de entrega de equipo Grupo Mecsa S.A.). ", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page3"].Gfx, "Pérdida o robo del Equipo o de Teléfono Celular será descontado el costo del nuevo equipo al colaborador ya sea:", fuenteBold, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                List<string> lista = new List<string>(){
                "Vía nomina: el Ejecutivo (a) y la Unidad de Recursos determinara la forma y plazo para la cancelación del equipo celular",
                "Deposito: el colaborador podrá ejecutar un depósito correspondiente al costo del equipo celular en las cuentas de la empresa, (la Unidad de recursos Humanos suministrará la información al colaborador. ",
                "Reponer físicamente el equipo, el cual deberá de tener las especificaciones y características iguales o superiores al equipo anterior, aclarando que este equipo y sus accesorios se consideraran como reemplazo del anterior para uso de la empresa."};
                // Llamar al método para agregar la lista numerada
                AgregarLista(pages["Page3"].Gfx, lista, fuenteGeneral, ref yoffetHeader, numerada: true);
                AddParrafo(pages["Page3"].Gfx, "En caso de cualquier reposición física del equipo esta deberá hacerse y coordinarse con el Área de Tecnología en un tiempo no mayor a 15 días hábiles a partir del momento de la notificación por robo o extravió. ", fuenteBold, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                #endregion

                #region page 4
                CrearNuevaPagina(pages, "Page4", document, true);
                AddParrafo(pages["Page4"].Gfx, "Cambios y Devoluciones", fuenteTitulo, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page4"].Gfx, "Los equipos y accesorios de la empresa pasan por un proceso minucioso de revisión, en los casos que el equipo haya presentado fallas mínimas o notables, el usuario podrá solicitar el cambio del equipo por otro en perfecto funcionamiento, para poder tramitar la devolución del producto, se debe cumplir con los siguientes requerimientos", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page4"].Gfx, "Deberá notificar a su jefatura inmediata, para que este notifique vía correo al Área de Tecnología correspondiente; quien se encargara de gestionar su reparación, a través de los diferentes proveedores de servicios de reparación telefónica o según el caso de la garantía del equipo.\r\nEn caso de que el colaborador por mal uso dañe cualquier parte del equipo o accesorio, asumirá el costo de la reparación y repuestos necesarios para el buen uso del teléfono o equipos.\r\n", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page4"].Gfx, "El teléfono celular o equipo debe ser devuelto en las mismas condiciones que fue entregado, debe venir con la caja y los accesorios (en caso que se hayan entregado), así mismo debe ser formateado con los datos de fábrica y desligado de cualquier cuenta en la nube este proceso deberá realizar con el encargado del área de Tecnología completando el respectivo formulario de Entrega de Equipos.\r\n\r\nEl colaborador autoriza expresamente a la empresa Grupo Mecsa S.A. mediante este documento a descontar del salario los valores de la dotación cuando en cualquiera de los casos anteriores no los devuelve al empleador.\r\n", fuenteGeneral, ref yoffetHeader);

                yoffetHeader = yoffetHeader + 15;
                List<(string nombre, string titulo)> firmantes = new List<(string, string)>
                {
                    (i.Employee.Name + " " + i.Employee.LastName, i.Employee.DNI.ToString() ),
                     ("Nombre y Firma Jefatura", "Jefe Inmediato"),
                    ("Nombre y Firma", "Encargado de Tecnologia"),
                };

                // Llamar al método para agregar las líneas de firmas
                AgregarLineaDeFirmas(pages["Page4"].Gfx, firmantes, ref yoffetHeader);

                #endregion

                #region Numbers

                int totalPages = pages.Count;
                int pageIndex = 1;
                foreach (var page in pages)
                {
                    HeaderPageNumber(page.Value.Gfx, page.Value.Page, pageIndex, totalPages);
                    pageIndex++;
                }
                #endregion






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
                var document = new PdfDocument
                {
                    PageLayout = PdfPageLayout.SinglePage,
                    Info = { Title = "Reporte de Entrega de Activos" }
                };

                // Diccionario para almacenar las páginas y sus gráficos.
                var pages = new Dictionary<string, (PdfPage Page, XGraphics Gfx)>();

                #region Portada

                // Crear la primera página para la portada.
                var portada = CrearNuevaPagina(pages, "Portada", document, true);
                GenerarPortada(portada.Gfx, portada.Page);

                #endregion
                #region Pagina 2

                // Crear la segunda página.
                CrearNuevaPagina(pages, "Page2", document, true);

                string parrafo = "El colaborador que reciba este activo será responsable de su correcto uso y mantenimiento. " +
                                 "Es imperativo utilizar el dispositivo exclusivamente para actividades relacionadas con el trabajo, " +
                                 "asegurando que se cumplan las políticas de seguridad y confidencialidad de la empresa. " +
                                 "Cualquier daño o pérdida debe ser reportado de inmediato al departamento de TI. " +
                                 "Asimismo, se recuerda que es responsabilidad del colaborador devolver el equipo en buen estado " +
                                 "al finalizar su relación laboral con la empresa. Su cooperación y cumplimiento con estas directrices " +
                                 "son esenciales para garantizar una comunicación eficiente y segura dentro de nuestra organización.";

                AddParrafo(pages["Page2"].Gfx, parrafo, fuenteGeneral, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page2"].Gfx, "Por lo tanto, la presente acta, hace constar la entrega del siguiente equipo y accesorios:", fuenteGeneral, ref yoffetHeader); // Ajusta la fuente y el offset según necesites

                // Datos de ejemplo para la tabla.
                string[,] data = new string[,]
                {
                    { "Información", "Datos" },
                    { "Número de serie", i.SerialNumber.ToString() },
                    { "Modelo", i.ModelName.ToString() },
                    { "Adquisición", i.AdquisitionDate.ToShortDateString() },
                    { "Costo", i.Cost.ToString() },
                    { "Tipo Disco", (i.HaveSSD) ? "Tiene SSD" : "Tiene HHD" },
                    { "Tamaño Disco", i.SizeDisk.ToString() + " GB" },
                    { "Tipo RAM", i.RAMType.ToString() },
                    { "Tamaño RAM", i.SizeRAM.ToString() + " GB" },
                    { "Procesador", i.Processor.ToString() },
                    { "Distribución", i.KeyboardLayout },
                    { "Marca", i.Brand.Name.ToString() },
                    { "Cédula", i.Employee.DNI.ToString() },
                    { "Nombre", i.Employee.Name.ToString() },
                    { "Apellido", i.Employee.LastName.ToString() },
                    { "Teléfono", i.Employee.PhoneNumber.ToString() },
                };
                // Definir altura de celda
                int cellHeight = 20;

                // Llamar al método para agregar la tabla
                AgregarTabla(pages["Page2"].Gfx, data, fuenteGeneral, ref yoffetHeader, cellHeight);

                #endregion

                #region page 3
                CrearNuevaPagina(pages, "Page3", document, true);

                string parrafo2 = "El Colaborador manifiesta que el equipo que aquí se entrega es y será de la empresa en todo momento, y procederá en caso de";

                AddParrafo(pages["Page3"].Gfx, parrafo2, fuenteGeneral, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page3"].Gfx, "Pérdida del Equipo:", fuenteTitulo, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page3"].Gfx, "Él colaborador deberá notificar de forma inmediata a su jefe, coordinador, para que se tomen las medidas de seguridad necesarias para el respectivo control administrativo.", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page3"].Gfx, "Robo de Equipo:", fuenteTitulo, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page3"].Gfx, "Él colaborador deberá acudir de manera inmediata al ente Público para su denuncia y notificar (presentar recibo de la denuncia) de forma inmediata a su jefe, coordinador o supervisor, para que se tomen las medidas de seguridad necesaria. A su vez el supervisor) deberá de notificar vía correo a Área de Tecnología y Recursos Humanos, para el respectivo control administrativo.", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page3"].Gfx, "En caso de robo o extravió de equipos y accesorios usados:  ", fuenteTitulo, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page3"].Gfx, "Se calculará el costo según la condición del equipo o depreciación desde el momento de la entrega (detalle escrito en el formato de entrega de equipo Grupo Mecsa S.A.). ", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page3"].Gfx, "Pérdida o robo del Equipo o de Teléfono Celular será descontado el costo del nuevo equipo al colaborador ya sea:", fuenteBold, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                List<string> lista = new List<string>(){
                "Vía nomina: el Ejecutivo (a) y la Unidad de Recursos determinara la forma y plazo para la cancelación del equipo celular",
                "Deposito: el colaborador podrá ejecutar un depósito correspondiente al costo del equipo celular en las cuentas de la empresa, (la Unidad de recursos Humanos suministrará la información al colaborador. ",
                "Reponer físicamente el equipo, el cual deberá de tener las especificaciones y características iguales o superiores al equipo anterior, aclarando que este equipo y sus accesorios se consideraran como reemplazo del anterior para uso de la empresa."};
                // Llamar al método para agregar la lista numerada
                AgregarLista(pages["Page3"].Gfx, lista, fuenteGeneral, ref yoffetHeader, numerada: true);
                AddParrafo(pages["Page3"].Gfx, "En caso de cualquier reposición física del equipo esta deberá hacerse y coordinarse con el Área de Tecnología en un tiempo no mayor a 15 días hábiles a partir del momento de la notificación por robo o extravió. ", fuenteBold, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                #endregion

                #region page 4
                CrearNuevaPagina(pages, "Page4", document, true);
                AddParrafo(pages["Page4"].Gfx, "Cambios y Devoluciones", fuenteTitulo, ref yoffetHeader); // Ajusta la fuente y el offset según necesites
                AddParrafo(pages["Page4"].Gfx, "Los equipos y accesorios de la empresa pasan por un proceso minucioso de revisión, en los casos que el equipo haya presentado fallas mínimas o notables, el usuario podrá solicitar el cambio del equipo por otro en perfecto funcionamiento, para poder tramitar la devolución del producto, se debe cumplir con los siguientes requerimientos", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page4"].Gfx, "Deberá notificar a su jefatura inmediata, para que este notifique vía correo al Área de Tecnología correspondiente; quien se encargara de gestionar su reparación, a través de los diferentes proveedores de servicios de reparación telefónica o según el caso de la garantía del equipo.\r\nEn caso de que el colaborador por mal uso dañe cualquier parte del equipo o accesorio, asumirá el costo de la reparación y repuestos necesarios para el buen uso del teléfono o equipos.\r\n", fuenteGeneral, ref yoffetHeader);
                AddParrafo(pages["Page4"].Gfx, "El teléfono celular o equipo debe ser devuelto en las mismas condiciones que fue entregado, debe venir con la caja y los accesorios (en caso que se hayan entregado), así mismo debe ser formateado con los datos de fábrica y desligado de cualquier cuenta en la nube este proceso deberá realizar con el encargado del área de Tecnología completando el respectivo formulario de Entrega de Equipos.\r\n\r\nEl colaborador autoriza expresamente a la empresa Grupo Mecsa S.A. mediante este documento a descontar del salario los valores de la dotación cuando en cualquiera de los casos anteriores no los devuelve al empleador.\r\n", fuenteGeneral, ref yoffetHeader);

                yoffetHeader = yoffetHeader + 15;
                List<(string nombre, string titulo)> firmantes = new List<(string, string)>
                {
                    (i.Employee.Name + " " + i.Employee.LastName, i.Employee.DNI.ToString() ),
                     ("Nombre y Firma Jefatura", "Jefe Inmediato"),
                    ("Nombre y Firma", "Encargado de Tecnologia"),
                };

                // Llamar al método para agregar las líneas de firmas
                AgregarLineaDeFirmas(pages["Page4"].Gfx, firmantes, ref yoffetHeader);

                #endregion
               
                #region Numbers

                int totalPages = pages.Count;
                int pageIndex = 1;
                foreach (var page in pages)
                {
                    HeaderPageNumber(page.Value.Gfx, page.Value.Page, pageIndex, totalPages);
                    pageIndex++;
                }
                #endregion
                // Guardar el documento en el memoryStream
                document.Save(memoryStream, false);

                // Retornar los bytes del PDF generado
                return memoryStream.ToArray();
            }
        } 
     
        #region General Elements
        private void AgregarTabla(XGraphics gfx, string[,] data, XFont font, ref int yOffset, int cellHeight)
        {
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
            double x = (gfx.PageSize.Width - tableWidth) / 2;

            // Dibujar la tabla.
            for (int row = 0; row < data.GetLength(0); row++)
            {
                double cellX = x;
                for (int col = 0; col < data.GetLength(1); col++)
                {
                    double cellWidth = columnWidths[col];

                    // Dibujar el borde de la celda.
                    gfx.DrawRectangle(XPens.Black, cellX, yOffset, cellWidth, cellHeight);

                    // Dibujar el texto dentro de la celda.
                    gfx.DrawString(data[row, col], font, XBrushes.Black,
                        new XRect(cellX, yOffset, cellWidth, cellHeight),
                        XStringFormats.Center);

                    // Actualizar la posición X para la siguiente celda.
                    cellX += cellWidth;
                }

                // Actualizar el offset Y para la siguiente fila.
                yOffset += cellHeight;
            }
        }
        private void AgregarLineaDeFirmas(XGraphics gfx, List<(string nombre, string titulo)> firmantes, ref int yOffset)
        {
            // Espacio entre cada firma
            int espacioEntreFirmas = 60;

            // Establecer el margen izquierdo
            double margenIzquierdo = 40;
            double anchoLinea = gfx.PageSize.Width - 80; // Ancho total menos márgenes

            foreach (var firmante in firmantes)
            {
                // Dibuja la línea para la firma
                gfx.DrawLine(XPens.Black, margenIzquierdo, yOffset, margenIzquierdo + anchoLinea, yOffset);

                // Dibuja el nombre del firmante
                gfx.DrawString(firmante.nombre, fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo, yOffset + 4, anchoLinea, 20), XStringFormats.TopLeft);

                // Dibuja el título del firmante
                gfx.DrawString(firmante.titulo, fuentePequena, XBrushes.Black, new XRect(margenIzquierdo, yOffset + 15, anchoLinea, 20), XStringFormats.TopLeft);

                // Actualiza el offset para la siguiente línea de firmas
                yOffset += espacioEntreFirmas;
            }
        }
        private void AgregarLista(XGraphics gfx, List<string> items, XFont fuente, ref int yOffset, bool numerada = false)
        {
            // Establecer el ancho máximo para el texto
            double anchoMaximo = gfx.PageSize.Width - 80; // Margenes izquierdo y derecho

            foreach (var item in items)
            {
                // Preparar el texto para el elemento de la lista
                string prefijo = numerada ? $"{items.IndexOf(item) + 1}. " : "• ";
                string textoCompleto = prefijo + item;

                // Dividir el contenido en líneas según el ancho máximo
                string lineaActual = string.Empty;
                var palabras = textoCompleto.Split(' ');

                foreach (var palabra in palabras)
                {
                    string nuevaLinea = string.IsNullOrEmpty(lineaActual) ? palabra : lineaActual + " " + palabra;
                    XSize size = gfx.MeasureString(nuevaLinea, fuente);

                    if (size.Width > anchoMaximo)
                    {
                        // Dibujar la línea actual y reiniciar
                        gfx.DrawString(lineaActual, fuente, XBrushes.Black, new XRect(50, yOffset, anchoMaximo, 20), XStringFormats.TopLeft);
                        yOffset += 20; // Ajustar el offset para la siguiente línea
                        lineaActual = palabra; // Iniciar una nueva línea
                    }
                    else
                    {
                        lineaActual = nuevaLinea; // Continuar construyendo la línea
                    }
                }

                // Dibujar cualquier contenido restante en lineaActual
                if (!string.IsNullOrEmpty(lineaActual))
                {
                    gfx.DrawString(lineaActual, fuente, XBrushes.Black, new XRect(40, yOffset, anchoMaximo, 20), XStringFormats.TopLeft);
                    yOffset += 20; // Ajustar el offset para la siguiente línea
                }
            }
        }
        private void AddParrafo(XGraphics gfx, string contenido, XFont fuente, ref int yOffset)
        {
            // Establecer el ancho máximo para el texto
            double anchoMaximo = gfx.PageSize.Width - 80; // Margenes izquierdo y derecho

            // Dividir el contenido en líneas según el ancho máximo
            var palabras = contenido.Split(' ');

            string lineaActual = string.Empty;

            foreach (var palabra in palabras)
            {
                // Verificar si agregar la palabra excede el ancho
                string nuevaLinea = string.IsNullOrEmpty(lineaActual) ? palabra : lineaActual + " " + palabra;
                XSize size = gfx.MeasureString(nuevaLinea, fuente);

                if (size.Width > anchoMaximo)
                {
                    // Dibujar la línea actual y reiniciar
                    gfx.DrawString(lineaActual, fuente, XBrushes.Black, new XRect(40, yOffset, anchoMaximo, 20), XStringFormats.TopLeft);
                    yOffset += 25; // Ajustar el offset para la siguiente línea
                    lineaActual = palabra; // Iniciar una nueva línea
                }
                else
                {
                    lineaActual = nuevaLinea; // Continuar construyendo la línea
                }
            }

            // Dibujar cualquier contenido restante en líneaActual
            if (!string.IsNullOrEmpty(lineaActual))
            {
                gfx.DrawString(lineaActual, fuente, XBrushes.Black, new XRect(40, yOffset, anchoMaximo, 20), XStringFormats.TopLeft);
                yOffset += 25; // Ajustar el offset para la siguiente línea
            }
        }
        #endregion
        #region Layout
        private (PdfPage Page, XGraphics Gfx) CrearNuevaPagina(Dictionary<string, (PdfPage Page, XGraphics Gfx)> pages, string key, PdfDocument document, bool applyLayout)
        {
            var nuevaPagina = document.AddPage();
            nuevaPagina.Size = PdfSharp.PageSize.A4;
            var nuevoGfx = XGraphics.FromPdfPage(nuevaPagina);
            if (applyLayout)
            {
                LayoutPage(nuevoGfx, nuevaPagina);
            }
            pages[key] = (nuevaPagina, nuevoGfx);
            return pages[key];
        }
        private void GenerarPortada(XGraphics gfx, PdfPage pagina)
        {
            // Definir tamaños y posiciones
            int margenIzquierdo = 35 +10;
            int anchoPagina = (int)pagina.Width - 2 * margenIzquierdo;
            int yOffset = 190;  // Margen superior
            // Tabla de versión y justificación
            int heighttitle = 30;
            int heightbody = 40;
            gfx.DrawRectangle(XPens.Black, XBrushes.SkyBlue, margenIzquierdo, yOffset, 500, heighttitle);
            gfx.DrawString("VERSIÓN", fuenteBold, XBrushes.Black, new XRect(margenIzquierdo + 5, yOffset + 2, 80, heighttitle), XStringFormats.Center);
            gfx.DrawString("JUSTIFICACIÓN DE LA CREACIÓN DEL DOCUMENTO", fuenteBold, XBrushes.Black, new XRect(margenIzquierdo + 90, yOffset + 2, 400, heighttitle), XStringFormats.Center);
            yOffset += heighttitle  ;
            gfx.DrawRectangle(XPens.Black, margenIzquierdo, yOffset, 80, heightbody);
            gfx.DrawString("01", fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo + 30, yOffset + 2, 40, heightbody), XStringFormats.Center);
            gfx.DrawRectangle(XPens.Black, margenIzquierdo + 80, yOffset, 500-80, heightbody);
            gfx.DrawString("La creación de este documento se manifiesta por la necesidad ", fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo + 85, yOffset, 500-40, 30), XStringFormats.Center);
            gfx.DrawString("en el diseño de un Sistema de Gestión de Calidad.", fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo + 85, yOffset + 10, 500-40, 30), XStringFormats.Center);
            yOffset += heightbody ;
            // Autor
            gfx.DrawRectangle(XPens.Black, margenIzquierdo, yOffset, 80, heightbody);
            gfx.DrawString("AUTOR", fuenteBold, XBrushes.Black, new XRect(margenIzquierdo + 5, yOffset + 2, 80, heightbody), XStringFormats.Center);
            gfx.DrawRectangle(XPens.Black, margenIzquierdo + 80, yOffset, 500-80, heightbody);
            gfx.DrawString("Norwin Ortega – Encargado de Recursos Humanos", fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo + 85, yOffset + 2, 500-80, heightbody), XStringFormats.Center);
            yOffset += heightbody + 15;
            // Revisado y aprobado por
            gfx.DrawRectangle(XPens.Black, XBrushes.SkyBlue, margenIzquierdo, yOffset, 500, heighttitle);
            gfx.DrawString("REVISADO POR", fuenteBold, XBrushes.Black, new XRect(margenIzquierdo + 5, yOffset + 2, 500, heighttitle), XStringFormats.Center);
            yOffset += heighttitle;
            gfx.DrawRectangle(XPens.Black, margenIzquierdo, yOffset, 350, heightbody);
            gfx.DrawString("Norwin Ortega Lazo – Encargado de Recursos Humanos", fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo + 5, yOffset + 2, 350, heightbody), XStringFormats.Center);
            gfx.DrawRectangle(XPens.Black, margenIzquierdo + 350, yOffset, 500- 350, heightbody);
            gfx.DrawString("09/2024", fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo + 355, yOffset + 2, 500 - 350, heightbody), XStringFormats.Center);
            yOffset += heightbody +15;
            gfx.DrawRectangle(XPens.Black, XBrushes.SkyBlue, margenIzquierdo, yOffset, 500, heighttitle);
            gfx.DrawString("APROBADO POR", fuenteBold, XBrushes.Black, new XRect(margenIzquierdo + 5, yOffset + 2, 500, heighttitle), XStringFormats.Center);
            yOffset += heighttitle;
            gfx.DrawRectangle(XPens.Black, margenIzquierdo, yOffset, 350, heightbody);
            gfx.DrawString("Anthony Fallas Ureña – Gerente General", fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo + 5, yOffset + 2, 350, heightbody), XStringFormats.Center);
            gfx.DrawRectangle(XPens.Black, margenIzquierdo + 350, yOffset, 500-350,heightbody);
            gfx.DrawString("09/2024", fuenteGeneral, XBrushes.Black, new XRect(margenIzquierdo + 355, yOffset + 2, 500-350, heightbody), XStringFormats.Center);
        }
        private void LayoutPage(XGraphics gfx, PdfPage page)
        {
            // Definir el ancho y alto de la página descontando los márgenes
            double anchoPagina = page.Width - 2 * margen;
            double altoPagina = page.Height - 2 * margen;
            // Dibujar el área de contenido dentro del margen (opcional, para visualizar los márgenes)
            gfx.DrawRectangle(XPens.DarkGray, margen, margen, anchoPagina, altoPagina);
            // Llamar al método que dibuja el encabezado, pasando el gráfico y la página
            yoffetHeader = HeaderPage(gfx, page);
            Footer(gfx, page);
        }
        private int HeaderPage(XGraphics gfx, PdfPage page)
        {
            // Definir el margen izquierdo
            double margenIzquierdo = 35;

            // Definir el ancho disponible en la página para el encabezado
            double anchoPagina = page.Width - 2 * margenIzquierdo;

            // Espacio en la parte superior del encabezado
            int yOffset = 40;  // Margen superior

            // Fuentes (asegúrate de que estas fuentes estén definidas correctamente)
            XFont fuenteTitulo = this.fuenteTitulo;
            XFont fuenteBold = this.fuenteBold;
            XFont fuentePequena = this.fuentePequena;

            // Texto del encabezado
            // Ajustar el tamaño del área del texto según sea necesario
            XRect retangule = new XRect();
            retangule = new XRect(margenIzquierdo + 10, yOffset + 10, 133, 104);
            // Cargar la imagen
            XImage imagen = XImage.FromFile("./wwwroot/Assets/Grupo-Mecsa-LOGO.png");

            // Dibujar la imagen en el lugar del texto
            gfx.DrawImage(imagen, retangule);

            //            gfx.DrawString("GRUPO Mecsa", fuenteTitulo, XBrushes.Black, retangule, XStringFormats.Center);
            gfx.DrawRectangle(XPens.Black, retangule);    // Dibuja el borde del rectángulo con un color

            retangule = new XRect(margenIzquierdo + 133 + 10, yOffset + 10, 215, 104);
            gfx.DrawString("ACTA DE ENTREGA DE ACTIVO", fuenteTitulo, XBrushes.Black, retangule, XStringFormats.Center);
            gfx.DrawRectangle(XPens.Black, retangule);    // Dibuja el borde del rectángulo con un color


            retangule = new XRect(margenIzquierdo + 215 + 133 + 10, yOffset + 10, 80, 40);
            gfx.DrawString("FO-GRH-AE-01", fuentePequena, XBrushes.Black, retangule, XStringFormats.Center);
            gfx.DrawRectangle(XPens.Black, retangule);    // Dibuja el borde del rectángulo con un color

            retangule = new XRect(margenIzquierdo + +80 + 215 + 133 + 10, yOffset + 10, 70, 40);
            gfx.DrawString("Version: 1\n9-24", fuentePequena, XBrushes.Black, retangule, XStringFormats.Center);
            gfx.DrawRectangle(XPens.Black, retangule);    // Dibuja el borde del rectángulo con un color

            retangule = new XRect(margenIzquierdo + 215 + 133 + 10, yOffset + 10 + 40, (80 + 70), (133 - 70));
            
            //    gfx.DrawString($"Pagina n°", fuenteTitulo, XBrushes.Black, retangule, XStringFormats.Center);
            
            gfx.DrawRectangle(XPens.Black, retangule);    // Dibuja el borde del rectángulo con un color
            return yOffset + 10 + 10 + 10 + 10 + 10 + 40 + (133 - 70);
        }
        private int HeaderPageNumber(XGraphics gfx, PdfPage page, int Number= 1, int totalPages = 0)
        {
            // Definir el margen izquierdo
            double margenIzquierdo = 35;

            // Definir el ancho disponible en la página para el encabezado
            double anchoPagina = page.Width - 2 * margenIzquierdo;

            // Espacio en la parte superior del encabezado
            int yOffset = 40;  // Margen superior

            // Fuentes (asegúrate de que estas fuentes estén definidas correctamente)
            XFont fuenteTitulo = this.fuenteTitulo;
            XFont fuenteBold = this.fuenteBold;
            XFont fuentePequena = this.fuentePequena;

            // Texto del encabezado
            // Ajustar el tamaño del área del texto según sea necesario
            XRect retangule = new XRect();
            retangule = new XRect(margenIzquierdo + 10, yOffset + 10, 133, 104);
            
            retangule = new XRect(margenIzquierdo + 215 + 133 + 10, yOffset + 10 + 40, (80 + 70), (133 - 70));

                gfx.DrawString($"Pagina n°{Number}/{totalPages}", fuenteTitulo, XBrushes.Black, retangule, XStringFormats.Center);

            gfx.DrawRectangle(XPens.Black, retangule);    // Dibuja el borde del rectángulo con un color
            return yOffset + 10 + 10 + 10 + 10 + 10 + 40 + (133 - 70);
        }
        private void Footer(XGraphics g, PdfPage page)
        {
            // Definir el margen inferior de 0.8 cm (en puntos)
            double margenInferior = 0.8 * 28.35; // 0.8 cm en puntos
            double margenSuperior = 28.35; // 1 cm en puntos (margen superior)

            // Calcular la posición vertical del texto (a distancia del margen inferior)
            double yPos = page.Height - margenInferior;

            // Definir el ancho total disponible en la página
            double anchoPagina = page.Width;

            // Calcular el tamaño del texto para centrarlo
            string texto = "Copyright 2024 - Documento Confidencial para uso Interno";
            XSize textoSize = g.MeasureString(texto, fuenteBold);

            // Calcular la posición horizontal para centrar el texto
            double xPos = (anchoPagina - textoSize.Width) / 2;

            // Dibujar el texto centrado y a la distancia del margen inferior
            g.DrawString(texto, fuentePequena, XBrushes.Black, new XRect(xPos, yPos, textoSize.Width, textoSize.Height), XStringFormats.TopLeft);
        }

        #endregion
    }
}

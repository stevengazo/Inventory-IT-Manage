using InventoryIT.Model;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using System.IO;
using InventoryIT.Controllers;

namespace InventoryIT.Utilities
{
    public class PDFService
    {
        public async Task<byte[]> GenerateListExtensions(List<PhoneExtension> extensions)
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
                document.Info.Title = "Lista de Extensiones";

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
                gfx.DrawString("Lista de Extensiones", titleFont, XBrushes.Black,
                    new XRect(0, 40, page.Width.Point, 40), XStringFormats.Center);

                // Párrafo antes de la tabla
                var beforeTableParagraph = "Lista de extensiones existentes";
                tf.DrawString(beforeTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, 80, page.Width.Point - 100, 40));

                // Definir el tamaño y posición de la tabla.
                double tableWidth = 200; // Ancho total de la tabla
                double cellWidth = 100;
                double cellHeight = 15;
                double x = 30; // Centrar la tabla horizontalmente
                double y = 120; // Ajustar la posición Y para que haya espacio para el párrafo

                int sizeData = (extensions.Count > 0) ? extensions.Count : 1;

                // Datos de ejemplo para la tabla.
                string[,] data = new string[sizeData + 1, 5];
                data[0, 0] = "Extension";
                data[0, 1] = "Central";
                data[0, 2] = "Tipo";
                data[0, 3] = "Empleado";
                data[0, 4] = "Apellido" ;


                // Llenar la matriz con los datos de las extensiones
                for (int i = 0; i < extensions.Count; i++)
                {
                    data[i + 1, 0] = extensions[i].Extension.ToString();
                    data[i + 1, 1] = extensions[i].PhoneNumberPBX.ToString();
                    data[i + 1, 2] = extensions[i].Type;
                    data[i + 1, 3] = (extensions[i].Employee != null) ? extensions[i].Employee.Name : "";
                    data[i + 1, 4] = (extensions[i].Employee != null) ? extensions[i].Employee.LastName : ""; ;
                }

                // Dibujar la tabla.
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        // Calcular la posición de la celda.
                        double cellX = x + col * cellWidth;
                        double cellY = y + row * cellHeight;

                        // Dibujar el borde de la celda.
                        gfx.DrawRectangle(XPens.Black, cellX, cellY, cellWidth, cellHeight);


                        // Dibujar el texto dentro de la celda.
                        gfx.DrawString(data[row, col], font, XBrushes.Black,
                            new XRect(cellX, cellY, cellWidth, cellHeight),
                            XStringFormats.Center);
                    }
                }

                // Guardar el documento...
                // Guardar el documento en el memoryStream en lugar de en un archivo
                document.Save(memoryStream, false);

                // Retornar los bytes del PDF generado
                return memoryStream.ToArray();

            }   }

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
                double tableWidth = 200; // Ancho total de la tabla
                double cellWidth = 100;
                double cellHeight = 15;
                double x = (page.Width.Point - tableWidth) / 2; // Centrar la tabla horizontalmente
                double y = 150; // Ajustar la posición Y para que haya espacio para el párrafo

                // Datos de ejemplo para la tabla.
                string[,] data = new string[,]
                {
                    { "Información", "Datos" },
                    { nameof(i.Name), i.SerialNumber.ToString() },
                    { nameof(i.Type), i.Type },
                    { nameof(i.AdquisitionDate), i.AdquisitionDate.ToShortDateString() },
                    { nameof(i.SerialNumber), i.SerialNumber },
                    { nameof(i.Cost), "$ " + i.Cost.ToString() },
                    { nameof(i.Brand.Name), i.Brand.Name},
                    { nameof(i.Brand), i.Brand.Name.ToString() },
                    { nameof(i.Employee.LastName), i.Employee.LastName.ToString() },
                    { nameof(i.Employee.PhoneNumber), i.Employee.PhoneNumber.ToString() },
                   // { nameof(i.Employee.Departament.Name), i.Employee.Departament.Name }
                };

                // Dibujar la tabla.
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        // Calcular la posición de la celda.
                        double cellX = x + col * cellWidth;
                        double cellY = y + row * cellHeight;

                        // Dibujar el borde de la celda.
                        gfx.DrawRectangle(XPens.Black, cellX, cellY, cellWidth, cellHeight);

                        // Dibujar el texto dentro de la celda.
                        gfx.DrawString(data[row, col], font, XBrushes.Black,
                            new XRect(cellX, cellY, cellWidth, cellHeight),
                            XStringFormats.Center);
                    }
                }

                // Párrafo después de la tabla
                var afterTableParagraph = $"Se informa a {i.Employee.Name} {i.Employee.LastName} que se llevará a cabo la entrega de un activo asignada para el uso corporativo. El empleado que reciba este activo será responsable de su correcto uso y mantenimiento. Es imperativo utilizar el dispositivo exclusivamente para actividades relacionadas con el trabajo, asegurando que se cumplan las políticas de seguridad y confidencialidad de la empresa. Cualquier daño o pérdida debe ser reportado de inmediato al departamento de TI. Asimismo, se recuerda que es responsabilidad del empleado devolver el equipo en buen estado al finalizar su relación laboral con la empresa. Su cooperación y cumplimiento con estas directrices son esenciales para garantizar una comunicación eficiente y segura dentro de nuestra organización";
                tf.DrawString(afterTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + 20, page.Width.Point - 100, 200));

                // Espacio para las firmas
                short _y = 200;
                gfx.DrawLine(XPens.Black, 50, y + data.GetLength(0) * cellHeight + _y, page.Width.Point - 80, y + data.GetLength(0) * cellHeight + _y);
                gfx.DrawString("Firma de quien entrega", font, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + _y, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Incrementar la coordenada y para crear separación
                gfx.DrawLine(XPens.Black, 50, y + data.GetLength(0) * cellHeight + 270, page.Width.Point - 80, y + data.GetLength(0) * cellHeight + 270);
                gfx.DrawString($"Firma de quien recibe: {i.Employee.Name} {i.Employee.LastName} {i.Employee.SecondLastName}", font, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + 280, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);


                // Guardar el documento...
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
                double tableWidth = 200; // Ancho total de la tabla
                double cellWidth = 100;
                double cellHeight = 15;
                double x = (page.Width.Point - tableWidth) / 2; // Centrar la tabla horizontalmente
                double y = 150; // Ajustar la posición Y para que haya espacio para el párrafo

                // Datos de ejemplo para la tabla.
                string[,] data = new string[,]
                {
                    { "Información", "Datos" },
                    { nameof(i.SerialNumber), i.SerialNumber.ToString() },
                    { nameof(i.ModelName), i.ModelName.ToString() },
                    { nameof(i.AdquisitionDate), i.AdquisitionDate.ToShortDateString() },
                    { nameof(i.Cost), i.Cost.ToString() },
                    { nameof(i.HaveSSD), (i.HaveSSD)? "Tiene SSD":"Tiene HHD" },
                    { nameof(i.SizeDisk), i.SizeDisk.ToString() + " GB"},
                    { nameof(i.RAMType), i.RAMType.ToString() },
                    { nameof(i.SizeRAM), i.SizeRAM.ToString() + " GB" },
                    { nameof(i.Processor), i.Processor.ToString() },
                    { nameof(i.KeyboardLayout), (i.HaveSSD)? "Posee":"No posee" },
                    { nameof(i.Brand), i.Brand.Name.ToString() },
                    { nameof(i.Employee.LastName), i.Employee.LastName.ToString() },
                    { nameof(i.Employee.PhoneNumber), i.Employee.PhoneNumber.ToString() },
                   // { nameof(i.Employee.Departament.Name), i.Employee.Departament.Name }
                };

                // Dibujar la tabla.
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        // Calcular la posición de la celda.
                        double cellX = x + col * cellWidth;
                        double cellY = y + row * cellHeight;

                        // Dibujar el borde de la celda.
                        gfx.DrawRectangle(XPens.Black, cellX, cellY, cellWidth, cellHeight);

                        // Dibujar el texto dentro de la celda.
                        gfx.DrawString(data[row, col], font, XBrushes.Black,
                            new XRect(cellX, cellY, cellWidth, cellHeight),
                            XStringFormats.Center);
                    }
                }

                // Párrafo después de la tabla
                var afterTableParagraph = $"Se informa a {i.Employee.Name} {i.Employee.LastName} que se llevará a cabo la entrega de una computadora asignada para el uso corporativo. El empleado que reciba este activo será responsable de su correcto uso y mantenimiento. Es imperativo utilizar el dispositivo exclusivamente para actividades relacionadas con el trabajo, asegurando que se cumplan las políticas de seguridad y confidencialidad de la empresa. Cualquier daño o pérdida debe ser reportado de inmediato al departamento de TI. Asimismo, se recuerda que es responsabilidad del empleado devolver el equipo en buen estado al finalizar su relación laboral con la empresa. Su cooperación y cumplimiento con estas directrices son esenciales para garantizar una comunicación eficiente y segura dentro de nuestra organización";
                tf.DrawString(afterTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + 20, page.Width.Point - 100, 200));

                // Espacio para las firmas
                short _y = 200;
                gfx.DrawLine(XPens.Black, 50, y + data.GetLength(0) * cellHeight + _y, page.Width.Point - 80, y + data.GetLength(0) * cellHeight + _y);
                gfx.DrawString("Firma de quien entrega", font, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + _y, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Incrementar la coordenada y para crear separación
                gfx.DrawLine(XPens.Black, 50, y + data.GetLength(0) * cellHeight + 270, page.Width.Point - 80, y + data.GetLength(0) * cellHeight + 270);
                gfx.DrawString($"Firma de quien recibe: {i.Employee.Name} {i.Employee.LastName} {i.Employee.SecondLastName}", font, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + 280, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);


                // Guardar el documento...
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

                // Definir el tamaño y posición de la tabla.
                double tableWidth = 200; // Ancho total de la tabla
                double cellWidth = 100;
                double cellHeight = 20;
                double x = (page.Width.Point - tableWidth) / 2; // Centrar la tabla horizontalmente
                double y = 150; // Ajustar la posición Y para que haya espacio para el párrafo

                // Datos de ejemplo para la tabla.
                string[,] data = new string[,]
                {
                    { "Información", "Datos" },
                    { nameof(i.PhoneNumber.Number), i.PhoneNumber.Number.ToString() },
                    { nameof(i.PhoneNumber.Operator), i.PhoneNumber.Operator.ToString() },
                    { nameof(i.PhoneNumber.Type), i.PhoneNumber.Type.ToString() },
                    { nameof(i.CreationDate), i.CreationDate.ToShortDateString() },
                    { nameof(i.Employee.EmployeeId), i.Employee.EmployeeId.ToString() },
                    { nameof(i.Employee.Name), i.Employee.Name.ToString() },
                    { nameof(i.Employee.LastName), i.Employee.LastName.ToString() },
                    { nameof(i.Employee.PhoneNumber), i.Employee.PhoneNumber.ToString() },
                    { nameof(i.Employee.Departament.Name), i.Employee.Departament.Name }
                };

                // Dibujar la tabla.
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        // Calcular la posición de la celda.
                        double cellX = x + col * cellWidth;
                        double cellY = y + row * cellHeight;

                        // Dibujar el borde de la celda.
                        gfx.DrawRectangle(XPens.Black, cellX, cellY, cellWidth, cellHeight);

                        // Dibujar el texto dentro de la celda.
                        gfx.DrawString(data[row, col], font, XBrushes.Black,
                            new XRect(cellX, cellY, cellWidth, cellHeight),
                            XStringFormats.Center);
                    }
                }

                // Párrafo después de la tabla
                var afterTableParagraph = $"Se informa a {i.Employee.Name} {i.Employee.LastName} que se llevará a cabo la entrega de un teléfono y una línea telefónica asignados para el uso corporativo. El empleado que reciba estos activos será responsable de su correcto uso y mantenimiento. Es imperativo utilizar los dispositivos exclusivamente para actividades relacionadas con el trabajo, asegurando que se cumplan las políticas de seguridad y confidencialidad de la empresa. Cualquier daño o pérdida debe ser reportado de inmediato al departamento de TI. Asimismo, se recuerda que es responsabilidad del empleado devolver el equipo en buen estado al finalizar su relación laboral con la empresa. Su cooperación y cumplimiento con estas directrices son esenciales para garantizar una comunicación eficiente y segura dentro de nuestra organización.";
                tf.DrawString(afterTableParagraph, paragraphFont, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + 20, page.Width.Point - 100, 200));

                // Espacio para las firmas
                short _y = 200;
                gfx.DrawLine(XPens.Black, 50, y + data.GetLength(0) * cellHeight + _y, page.Width.Point - 80, y + data.GetLength(0) * cellHeight + _y);
                gfx.DrawString("Firma de quien entrega", font, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + _y, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);

                // Incrementar la coordenada y para crear separación
                gfx.DrawLine(XPens.Black, 50, y + data.GetLength(0) * cellHeight + 270, page.Width.Point - 80, y + data.GetLength(0) * cellHeight + 270);
                gfx.DrawString($"Firma de quien recibe: {i.Employee.Name} {i.Employee.LastName} {i.Employee.SecondLastName}", font, XBrushes.Black,
                    new XRect(50, y + data.GetLength(0) * cellHeight + 280, page.Width.Point - 50, 0),
                    XStringFormats.TopLeft);


                // Agregar pie de página
                var footerText = $"Documento generado el {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Reporte generado por el sistema InventoryIT";
                gfx.DrawString(footerText, font, XBrushes.Black,
                    new XRect(50, page.Height.Point - 50, page.Width.Point - 100, 20),
                    XStringFormats.Center);

                // Guardar el documento...
                // Guardar el documento en el memoryStream en lugar de en un archivo
                document.Save(memoryStream, false);

                // Retornar los bytes del PDF generado
                return memoryStream.ToArray();
            }
        }
    }
}

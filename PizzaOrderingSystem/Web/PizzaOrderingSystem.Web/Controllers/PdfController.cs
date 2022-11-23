using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderingSystem.Common;
using PizzaOrderingSystem.Services.Data;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.IO;
using System.Threading.Tasks;

namespace PizzaOrderingSystem.Web.Controllers
{
    public class PdfController : BaseController
    {
        private readonly IOrderService orderService;

        public PdfController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public async Task<IActionResult> CreateDocument()
        {
            var order = await this.orderService.GetLastOrderAsync();

            //Creates a new PDF document
            PdfDocument document = new PdfDocument();

            //Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Landscape;
            document.PageSettings.Margins.All = 50;

            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            //Loads the image as stream
            FileStream imageStream = new FileStream(GlobalConstants.ShopImageFullUrlOffice, FileMode.Open, FileAccess.Read);
            RectangleF bounds = new RectangleF(176, 0, 390, 130);
            PdfImage image = PdfImage.FromStream(imageStream);

            //Draws the image to the PDF page
            page.Graphics.DrawImage(image, bounds);
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, bounds.Bottom + 90, graphics.ClientSize.Width, 30);

            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);

            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);

            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement("INVOICE " + order.Id, subHeadingFont);
            element.Brush = PdfBrushes.White;

            //Draws the heading on the page
            PdfLayoutResult result = element.Draw(page, new PointF(10, bounds.Top + 8));
            string currentDate = order.TimeOfOrder.ToString("F");

            //Measures the width of the text to place it in the correct location
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y);

            //Draws the date by using DrawString method
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, textPosition);
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);

            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement("BILL TO : " + order.User.FirstName.ToUpper() + " " + order.User.LastName.ToUpper(), timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            element = new PdfTextElement("TOTAL PRICE WITH VAT : " + order.TotalPrice.ToString("C"), timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));
            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, result.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);

            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            //Saving the PDF to the MemoryStream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Set the position as '0'.
            stream.Position = 0;

            //Download the PDF document in the browser
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            fileStreamResult.FileDownloadName = $"Invoice {order.Id}.pdf";
            return fileStreamResult;
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using PrintIt.Core;

namespace PrintIt.ServiceHost.Controllers
{
    [ApiController]
    [Route("print")]
    public class PrintController : ControllerBase
    {
        private readonly IPdfPrintService _pdfPrintService;

        public PrintController(IPdfPrintService pdfPrintService)
        {
            _pdfPrintService = pdfPrintService;
        }

        [HttpPost]
        [Route("from-pdf")]
        public async Task<IActionResult> PrintFromPdf([FromBody] PrintFromTemplateRequest request)
        {
            //await using Stream pdfStream = request.PdfFile.OpenReadStream();
            var encodedPdfString = request.PdfFile;
            var pdfFile = Convert.FromBase64String(encodedPdfString);
            MemoryStream pdfStream = new MemoryStream(pdfFile);

            await _pdfPrintService.Print(pdfStream,
                printerName: request.PrinterPath,
                pageRange: request.PageRange,
                numberOfCopies: request.Copies ?? 1);
            return Ok();
        }

    }

    public sealed class PrintFromTemplateRequest
    {
         [Required]
        public string PdfFile { get; set; }

         [Required]
        public string PrinterPath { get; set; }

        public string PageRange { get; set; }

        public int? Copies { get; set; }
    }

    public sealed class PrintLabelPayload
    {
        public string TrackingNumbers { get; set; }

        public string[] GetTrackingNumbers()
        {
            if (string.IsNullOrEmpty(TrackingNumbers) == false)
            {
                return TrackingNumbers.Split(',');
            }
            return new string[] { };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace UniversityCourseManagementSystem.Controllers
{
    [Serializable]
    public class ViewResultController : Controller
    {
       
        RegisteredCourseManager aRegisteredCourseManager = new RegisteredCourseManager();
        ViewResultManager aViewResultManager = new ViewResultManager();
        public ActionResult ViewResult()
        {
            ViewBag.RegistrationNo = aRegisteredCourseManager.GetRegistrationNo();
            return View();
        }

        [HttpPost]
        public ActionResult ViewResult(PdfModel aModel,int studentId)
        {
            ViewBag.RegistrationNo = aRegisteredCourseManager.GetRegistrationNo();

            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            Paragraph header1 = new Paragraph("International Islamic University Chittagong", FontFactory.GetFont("Italic", 15, Font.BOLD, BaseColor.DARK_GRAY));
            header1.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(header1);
            Paragraph header = new Paragraph("Semester Grade System",FontFactory.GetFont("Italic",15,Font.BOLD,BaseColor.BLACK));
            header.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(header);

            //Horizontal Line
            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            pdfDoc.Add(line);


            PdfPTable table1 = new PdfPTable(2);
            table1.WidthPercentage = 100;
            //0=Left, 1=Centre, 2=Right
            table1.HorizontalAlignment = 0;
            table1.SpacingBefore = 30f;
            table1.SpacingAfter = 20f;

            PdfPCell cell = new PdfPCell();
            cell.Border = 0;

            Chunk aChunk = new Chunk("Name: "+ aModel.StudentName +".\nEmail: "+ aModel.Email +".\nDepartment: "+ aModel.DeptName +"", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK));
            cell = new PdfPCell();
            cell.Border = 0;
            cell.AddElement(aChunk);
            table1.AddCell(cell);

            Paragraph date = new Paragraph("Date: " + DateTime.Now.ToShortDateString() + "\nTime: " + DateTime.Now.ToShortTimeString() + "", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK));
            cell = new PdfPCell();
            cell.Border = 0;
            date.Alignment = Element.ALIGN_RIGHT;
            cell.AddElement(date);
            table1.AddCell(cell);

            pdfDoc.Add(table1);

            //Tableg
            PdfPTable table2 = new PdfPTable(2);
            table2.WidthPercentage = 100;
            //0=Left, 1=Centre, 2=Right
            table2.HorizontalAlignment = 0;
            table2.SpacingBefore = 20f;
            table2.SpacingAfter = 30f;

            Paragraph resultParagraph = new Paragraph("Result Sheet :", FontFactory.GetFont("Arial", 12, Font.ITALIC, BaseColor.DARK_GRAY));
            pdfDoc.Add(resultParagraph);

            //Table
            //int sl = 0;
            var studentResult = aViewResultManager.GetStudentResult(studentId);
            table2 = new PdfPTable(3);
            table2.WidthPercentage = 100;
            table2.HorizontalAlignment = 0;
            table2.SpacingBefore = 20f;
            table2.SpacingAfter = 30f;

            //table.AddCell("Sl.");
            table2.AddCell("Course Code");
            table2.AddCell("Course Tile");
            table2.AddCell("Garde");
            //Cell
            foreach (var result in studentResult)
            {
                //table.AddCell(sl++.ToString());
                table2.AddCell(result.CourseCode);
                table2.AddCell(result.CourseName);
                table2.AddCell(result.GradeName);
            }
            pdfDoc.Add(table2);

            //Garding Sysytem
            Paragraph score = new Paragraph("GRADING SYSTEM :", FontFactory.GetFont("Arial", 12, Font.ITALIC, BaseColor.DARK_GRAY));
            pdfDoc.Add(score);
            
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;
            //Cell 1
            table.AddCell("PERCENTAGE OF SCORE");
            table.AddCell("LETTER GRADE");
            table.AddCell("REMARKS");
            //Cell 2
            table.AddCell("80-100");
            table.AddCell("A+");
            table.AddCell("Excellent");
            //Cell 3
            table.AddCell("75-79");
            table.AddCell("A");
            table.AddCell("Very Good");
            //Cell 4
            table.AddCell("70-74");
            table.AddCell("A-");
            table.AddCell("Very Good");
            //Cell 5
            table.AddCell("65-69");
            table.AddCell("B+");
            table.AddCell("Good");
            //Cell 6
            table.AddCell("60-64");
            table.AddCell("B");
            table.AddCell("Good");
            //Cell 7
            table.AddCell("55-59");
            table.AddCell("B-");
            table.AddCell("Satisfactory");
            //Cell 8
            table.AddCell("50-54");
            table.AddCell("C+");
            table.AddCell("Satisfactory");
            //Cell 9
            table.AddCell("45-49");
            table.AddCell("C");
            table.AddCell("Pass");
            //Cell 10
            table.AddCell("40-44");
            table.AddCell("D");
            table.AddCell("Pass");
            //Cell 11
            table.AddCell("00-39");
            table.AddCell("F");
            table.AddCell("Fail");
            pdfDoc.Add(table);

            
            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Credit-Card-Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();

            return View();
        }

        
        public JsonResult GetStudentInfoWithDeptName(int studentId)
        {
            var studentInfo = aRegisteredCourseManager.GetStudentInfoWithDeptName(studentId);
            return Json(studentInfo);
        }
        public JsonResult GetStudentResult(int studentId)
        {
            var studentResult = aViewResultManager.GetStudentResult(studentId);
            return Json(studentResult);
        }
	}
}
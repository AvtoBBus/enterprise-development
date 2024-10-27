using AdmissionCommittee.Domain.Models;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace AdmissionCommittee.Server
{
    public class ReaderCSV
    {
        public static List<Applicant> GetApplicants(string fileName)
        {
            var applicants = new List<Applicant>();
            using (TextFieldParser textFieldParser = new TextFieldParser(fileName))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(";");
                while (!textFieldParser.EndOfData)
                {
                    string[] rows = textFieldParser.ReadFields();
                    Applicant applicant = new Applicant
                    {
                        Id = Int32.Parse(rows[0]),
                        BirthdayDate = DateTime.ParseExact(rows[1], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        City = rows[2],
                        Country = rows[3],
                        FullName = rows[4]
                    };
                    applicants.Add(applicant);
                }
            }

            return applicants;
        }

        public static List<Direction> GetDirections(string fileName)
        {
            var directions = new List<Direction>();
            using (TextFieldParser textFieldParser = new TextFieldParser(fileName))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(";");
                while (!textFieldParser.EndOfData)
                {
                    string[] rows = textFieldParser.ReadFields();
                    Direction direction = new Direction
                    {
                        Id = Int32.Parse(rows[0]),
                        ApplicantId = Int32.Parse(rows[1]),
                        SpecialityId = Int32.Parse(rows[2]),
                        Priority = Int32.Parse(rows[3])
                    };
                    directions.Add(direction);
                }
            }

            return directions;
        }

        public static List<ExamResult> GetExamResults(string fileName)
        {
            var examResults = new List<ExamResult>();
            using (TextFieldParser textFieldParser = new TextFieldParser(fileName))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(";");
                while (!textFieldParser.EndOfData)
                {
                    string[] rows = textFieldParser.ReadFields();
                    ExamResult eResult = new ExamResult
                    {
                        Id = Int32.Parse(rows[0]),
                        ApplicantId = Int32.Parse(rows[1]),
                        ExamName = rows[2],
                        Result = Int32.Parse(rows[3]),
                    };
                    examResults.Add(eResult);
                }
            }

            return examResults;
        }

        public static List<Speciality> GetSpecialities(string fileName)
        {
            var specialities = new List<Speciality>();
            using (TextFieldParser textFieldParser = new TextFieldParser(fileName))
            {
                textFieldParser.TextFieldType = FieldType.Delimited;
                textFieldParser.SetDelimiters(";");
                while (!textFieldParser.EndOfData)
                {
                    string[] rows = textFieldParser.ReadFields();
                    Speciality speciality = new Speciality
                    {
                        Id = Int32.Parse(rows[0]),
                        Number = rows[1],
                        Name = rows[2],
                        Faculity = rows[3],
                    };
                    specialities.Add(speciality);
                }
            }

            return specialities;
        }
    }
}

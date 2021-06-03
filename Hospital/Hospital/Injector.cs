using Service;
using Hospital.VMConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class Injector
    {
        //private StudentService studentService = new StudentService();

        //private TeacherService teacherService = new TeacherService();

        //private ClassesService subjectService = new ClassesService();

        //private TeacherConverter teacherConverter = new TeacherConverter();

        //private SubjectConverter subjectConverter = new SubjectConverter();

        private PatientConverter patientConverter = new PatientConverter();

        //public StudentService StudentService
        //{
        //    get { return studentService; }
        //}

        //public TeacherService TeacherService
        //{
        //    get { return teacherService; }
        //}

        //public ClassesService SubjectService
        //{
        //    get { return subjectService; }
        //}

        //public SubjectConverter SubjectConverter
        //{
        //    get { return subjectConverter; }
        //}

        //public TeacherConverter TeacherConverter
        //{
        //    get { return teacherConverter; }
        //}

        public PatientConverter PatientConverter
        {
            get { return patientConverter; }
        }
    }
}

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
        private PatientService patientService = new PatientService();

        //private TeacherService teacherService = new TeacherService();

        //private ClassesService subjectService = new ClassesService();

        //private TeacherConverter teacherConverter = new TeacherConverter();

        //private SubjectConverter subjectConverter = new SubjectConverter();

        private PatientConverter patientConverter = new PatientConverter();

        public PatientService PatientService
        {
            get { return patientService; }
        }

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

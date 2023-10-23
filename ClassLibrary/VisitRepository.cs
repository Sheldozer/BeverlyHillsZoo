using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VisitRepository
    {
        private readonly ZooContext _context = new ZooContext();

        public void AddVisit(Visit visit)
        {
            _context.Add(visit);
            _context.SaveChanges();
        }

        public void DeleteVisit()
        {
            // add logic
        }

        public void ViewVisits()
        {
           // add logic
        }
    }
}

﻿using ClassLibrary.Data;
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

        public void AddVisit(int animalId, List<int> visitorIds, DateTime visitDate, Visit.TimeSlot visitTimeSlot)
        {
            var visitorsInVisit = _context.Visitors.Where(v => visitorIds.Contains(v.Id)).ToList();

            var newVisit = new Visit
            {
                AnimalId = animalId,
                Visitors = visitorsInVisit,
                VisitDate = visitDate,
                VisitTimeSlot = visitTimeSlot
            };

            _context.Add(newVisit);
            _context.SaveChanges();
        }

        public void DeleteVisit()
        {
            // add logic
        }

        public void ViewVisits()
        {
           
        }
    }
}

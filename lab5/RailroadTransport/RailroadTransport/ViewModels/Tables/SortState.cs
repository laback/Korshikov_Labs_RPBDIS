using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailroadTransport.ViewModels
{
    public enum SortState
    {
        No,
        NameOfPostAcs,
        NameOfPostDesc,
        NameOfStopAcs,
        NameOfStopDecs,
        FIOAcs,
        FIODecs,
        AgeAcs,
        AgeDecs,
        WorkExpAcs,
        WorkExpDecs,
        NameOfTypeAcs,
        NameOfTypeDecs
    }
    public class SortViewModel
    {
        public SortState NameOfPostSort { get; set; }
        public SortState NameOfStopSort { get; set; }
        public SortState FIOSort { get; set; }
        public SortState AgeSort { get; set; }
        public SortState WorkExpSort { get; set; }
        public SortState NameOfTypeSort { get; set; }
        public SortState CurrentState { get; set; }
        public SortViewModel(SortState sortState)
        {
            NameOfPostSort = sortState == SortState.NameOfPostAcs ? SortState.NameOfPostDesc : SortState.NameOfPostAcs;
            NameOfStopSort = sortState == SortState.NameOfStopAcs ? SortState.NameOfStopDecs : SortState.NameOfStopAcs;
            FIOSort = sortState == SortState.FIOAcs ? SortState.FIODecs : SortState.FIOAcs;
            AgeSort = sortState == SortState.AgeAcs ? SortState.AgeDecs : SortState.AgeAcs;
            WorkExpSort = sortState == SortState.WorkExpAcs ? SortState.WorkExpDecs : SortState.WorkExpAcs;
            NameOfTypeSort = sortState == SortState.NameOfTypeAcs ? SortState.NameOfTypeDecs : SortState.NameOfTypeAcs;

            CurrentState = sortState;
        }
    }
}
using System.Collections.Generic;

namespace R3
{
    public class ErrorCode
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public string LogId { get; set; }
    }

    public class Paging
    {
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public int MaxRecords { get; set; }
        public int TotalPages { get; set; }
        public int RecordsShowing { get; set; }
        public int Pins { get; set; }
    }

    public class Building
    {
        public string BathroomTotal { get; set; }
        public string Bedrooms { get; set; }
        public string StoriesTotal { get; set; }
        public string Type { get; set; }
        public string SizeExterior { get; set; }
        public string SizeInterior { get; set; }
    }

    public class Address
    {
        public string AddressText { get; set; }
    }

    public class Phone
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string AreaCode { get; set; }
        public string PhoneTypeId { get; set; }
    }

    public class Email
    {
        public string ContactId { get; set; }
    }

    public class Website
    {
        public string Websites { get; set; }
        public string WebsiteTypeId { get; set; }
    }

    public class Organization
    {
        public int OrganizationID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }
        public List<Website> Websites { get; set; }
        public string Designation { get; set; }
        public bool HasEmail { get; set; }
        public bool PermitFreetextEmail { get; set; }
        public bool PermitShowListingLink { get; set; }
    }

    public class Phone2
    {
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string AreaCode { get; set; }
        public string PhoneTypeId { get; set; }
    }

    public class Email2
    {
        public string ContactId { get; set; }
    }

    public class Website2
    {
        public string Website { get; set; }
        public string WebsiteTypeId { get; set; }
    }

    public class Individual
    {
        public int IndividualID { get; set; }
        public string Name { get; set; }
        public Organization Organization { get; set; }
        public List<Phone2> Phones { get; set; }
        public List<Email2> Emails { get; set; }
        public string Photo { get; set; }
        public string Position { get; set; }
        public bool PermitFreetextEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CorporationDisplayTypeId { get; set; }
        public List<Website2> Websites { get; set; }
        public string EducationCredentials { get; set; }
        public string CorporationName { get; set; }
        public string CorporationType { get; set; }
        public bool? CccMember { get; set; }
    }

    public class Address2
    {
        public string AddressText { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }

    public class Photo
    {
        public string SequenceId { get; set; }
        public string HighResPath { get; set; }
        public string MedResPath { get; set; }
        public string LowResPath { get; set; }
        public string Description { get; set; }
        public string LastUpdated { get; set; }
    }

    public class Parking
    {
        public string Name { get; set; }
    }

    public class Property
    {
        public string Price { get; set; }
        public string Type { get; set; }
        public Address2 Address { get; set; }
        public List<Photo> Photo { get; set; }
        public List<Parking> Parking { get; set; }
        public string ParkingSpaceTotal { get; set; }
        public string TypeId { get; set; }
        public string FarmType { get; set; }
        public string ZoningType { get; set; }
        public string AmmenitiesNearBy { get; set; }
        public string OwnershipType { get; set; }
        public string LeaseRent { get; set; }
    }

    public class Business
    {
    }

    public class Land
    {
        public string SizeTotal { get; set; }
        public string SizeFrontage { get; set; }
    }

    public class AlternateURL
    {
        public string DetailsLink { get; set; }
        public string VideoLink { get; set; }
    }

    public class OpenHouse
    {
        public string StartTime { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string FormattedDateTime { get; set; }
        public string Comments { get; set; }
    }

    public class Result
    {
        public string Id { get; set; }
        public string MlsNumber { get; set; }
        public string PublicRemarks { get; set; }
        public Building Building { get; set; }
        public List<Individual> Individual { get; set; }
        public Property Property { get; set; }
        public Business Business { get; set; }
        public Land Land { get; set; }
        public AlternateURL AlternateURL { get; set; }
        public string PostalCode { get; set; }
        public string RelativeDetailsURL { get; set; }
        public List<OpenHouse> OpenHouse { get; set; }
    }

    public class Pin
    {
        public string key { get; set; }
        public string propertyId { get; set; }
        public int count { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class SearchResult
    {
        public ErrorCode ErrorCode { get; set; }
        public Paging Paging { get; set; }
        public List<Result> Results { get; set; }
        public List<Pin> Pins { get; set; }
    }
}
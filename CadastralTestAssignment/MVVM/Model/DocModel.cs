using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CadastralTestAssignment.MVVM.Model
{
    public class DocModel : BaseModel
    {
        public string? OrganRegistrRights { get; private set; }
        public string? DateFormation { get; private set; }
        public string? RegistrationNumber { get; private set; }
        public string? DateReceivedRequest { get; private set; }
        public string? DateReceiptRequestRegAuthorityRights { get; private set; }
        public string? Area { get; private set; }
        public string? Unit { get; private set; }

        public DocModel() { }
        public DocModel(XElement xElement) 
        {
            Deserialize(xElement);
        }
        public override XElement Serialize()
        {
            throw new NotImplementedException();
        }

        public override void SoloSerialize(string path)
        {
            Console.WriteLine("Nothing to serialize solo");
        }

        protected override void Deserialize(XElement doc)
        {
            XElement? detailsStatement = doc.Element("details_statement");
            XElement? groupTopRequisites = detailsStatement?.Element("group_top_requisites");

            XElement? organRegistrRights = groupTopRequisites?.Element("organ_registr_rights");
            XElement? dateFormation = groupTopRequisites?.Element("date_formation");
            XElement? registrationNumber = groupTopRequisites?.Element("registration_number");
            OrganRegistrRights = organRegistrRights?.Value ?? string.Empty;
            DateFormation = dateFormation?.Value ?? string.Empty;
            RegistrationNumber = registrationNumber?.Value ?? string.Empty;

            XElement? detailsRequest = doc?.Element("details_request");
            XElement? dateReceivedRequest = detailsRequest?.Element("date_received_request");
            XElement? dateReceiptRequestRegAuthorityRights = detailsRequest?.Element("date_receipt_request_reg_authority_rights");
            DateReceivedRequest = dateReceivedRequest?.Value ?? string.Empty;
            DateReceiptRequestRegAuthorityRights = dateReceiptRequestRegAuthorityRights?.Value ?? string.Empty;

            XElement? cadastralBlock = doc?.Element("cadastral_blocks")?.Element("cadastral_block");
            XElement? cadastralNumber = cadastralBlock?.Element("cadastral_number");
            XElement? areaQuarter = cadastralBlock?.Element("area_quarter");

            Indexer = cadastralNumber?.Value ?? string.Empty;
            Area = areaQuarter?.Element("area")?.Value ?? string.Empty;
            Unit = areaQuarter?.Element("unit")?.Value ?? string.Empty;
        }
    }
}
